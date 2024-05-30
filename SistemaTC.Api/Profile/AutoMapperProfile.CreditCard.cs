using Entity = SistemaTC.Data.Entities.CreditCard;
using TransactionEntity = SistemaTC.Data.Entities.CreditCardTransaction;
using EntityDTO = SistemaTC.DTO.CreditCard.CreditCard;
using ExistingDTOPin = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using ExistingDTOBloqueo = SistemaTC.DTO.CreditCard.ExistingCreditCardBloqueo;
using ExistingDTOAumento = SistemaTC.DTO.CreditCard.ExistingCreditCardAumento;
using NewDTO = SistemaTC.DTO.CreditCard.CreditCardNew;
using ResponseDTOSaldos = SistemaTC.DTO.CreditCard.CreditCardResponseSaldo;
using ResponseDTOFecha = SistemaTC.DTO.CreditCard.CreditCardResponseFecha;
using ResponseDTODetalle = SistemaTC.DTO.CreditCard.CreditCardResponseDetalle;
using ResponseDTOStatement = SistemaTC.DTO.CreditCard.CreditCardStatement;
using ResponseDTOStatementDetail = SistemaTC.DTO.CreditCard.CreditCardStatementDetail;
using static SistemaTC.Core.Enums;

namespace SistemaTC.Api.Profile;
public partial class AutoMapperProfile
{
    public void AddCreditCardMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOPin>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOBloqueo>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LockedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOAumento>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, EntityDTO>()
            .ReverseMap();
        CreateMap<Entity, ResponseDTOStatement>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.FirstName}{(string.IsNullOrEmpty(src.Owner.LastName) ? "" : $" { src.Owner.LastName}")}"));
        CreateMap<TransactionEntity, ResponseDTOStatementDetail>()
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Type == CreditCardTransactionType.Debit ? src.Amount : 0))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Type == CreditCardTransactionType.Credit ? src.Amount : 0))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created))
            .ReverseMap();
    }
}
