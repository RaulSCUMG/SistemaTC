using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaTC.Core;
using SistemaTC.Core.Extensions;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.Enums;

namespace SistemaTC.Services;
public class CutoffService(TCContext dbContext, ILogger<CutoffService> logger): ICutoffService
{
    public async Task<List<CreditCutOff>> GetCreditCutoffAsync()
    {
        return await dbContext.CreditCutOffs.ToListAsync();
    }

    public async Task<CreditCutOff?> GetCreditCutoffAsync(Guid creditCutoffId)
    {
        return await dbContext.CreditCutOffs.FirstOrDefaultAsync(x => x.UserId == creditCutoffId);
    }

    public async Task CreateCreditCardsCutOff(string user)
    {
        var success = true;
        var hashTransaction = GetHashCode();
        var currentDate = DateTime.Now;
        var creditCards = await dbContext.CreditCards
            .Where(x => x.NextBalanceCutOffDate.Day == currentDate.Day
                && x.Active)
            .ToListAsync();

        foreach (var creditCard in creditCards)
        {
            try
            {
                await dbContext.Database.BeginTransactionAsync();
                var lastCutoff = await dbContext.CreditCutOffs
                    .FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId
                                        && !x.Closed);

                if (lastCutoff != null && lastCutoff.PayedAmount < lastCutoff.TotalBalance)
                {
                    var chargeAmount = (lastCutoff.TotalBalance - lastCutoff.PayedAmount) * (creditCard.ChargeRate / 100);
                    await dbContext.CreditCardTransactions.AddAsync(new()
                    {
                        UserId = creditCard.UserId,
                        CreditCardId = creditCard.CreditCardId,
                        Type = CreditCardTransactionType.Credit,
                        Description = General.CreditCutOff.ChargeRateTemplate,
                        Amount = chargeAmount,
                        Created = DateTime.UtcNow,
                        CreatedBy = user
                    });

                    creditCard.CreditAvailable -= chargeAmount;
                    lastCutoff.Closed = true;
                    await dbContext.SaveChangesAsync();
                }

                var transactions = await dbContext.CreditCardTransactions
                    .Where(x => x.CreditCardId == creditCard.CreditCardId
                                && x.CreditCutOffId == null)
                    .ToListAsync();

                CreditCutOff newCreditCutOff = new()
                {
                    UserId = creditCard.UserId,
                    CreditCardId = creditCard.CreditCardId,
                    Name = General.CreditCutOff.NameTemplate.Replace("{date}", $"{currentDate:MMMM yyyy}"),
                    Year = (ushort)currentDate.Year,
                    Month = (ushort)currentDate.Month,
                    TotalCredit = transactions.Where(x => x.Type == CreditCardTransactionType.Credit).Sum(x => x.Amount),
                    TotalDebit = transactions.Where(x => x.Type == CreditCardTransactionType.Debit).Sum(x => x.Amount),
                    Created = DateTime.UtcNow,
                    CreatedBy = user
                };

                newCreditCutOff.TotalBalance = (lastCutoff?.TotalBalance ?? 0) + newCreditCutOff.TotalCredit - newCreditCutOff.TotalDebit;
                newCreditCutOff.TotalCreditAvailable = creditCard.CreditAvailable;
                await dbContext.CreditCutOffs.AddAsync(newCreditCutOff);

                transactions.ForEach(x => { x.CreditCutOffId = newCreditCutOff.CreditCutOffId; });

                var nextMonth = currentDate.AddMonths(1);
                var daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
                creditCard.NextBalanceCutOffDate = new DateTime(nextMonth.Year, nextMonth.Month, creditCard.BalanceCutOffDay > daysInNextMonth ? daysInNextMonth : creditCard.BalanceCutOffDay);
                creditCard.NextPaymentDate = new DateTime(nextMonth.Year, nextMonth.Month, creditCard.PaymentDay > daysInNextMonth ? daysInNextMonth : creditCard.PaymentDay);

                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync();
                var errorMessage = e.GetLastException();
                logger.LogError("Error processing credit cutoff for credit card {CreditCardId}. Transaction: {hashTransaction}. Error: {errorMessage}", creditCard.CreditCardId, hashTransaction, errorMessage);
                success = false;
            }
        }

        if (!success)
        {
            throw new Exception($"Some errors were produced during the process. Please contact your administrator. Transaction: {hashTransaction}");
        }
    }
}
