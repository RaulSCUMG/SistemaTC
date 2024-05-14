﻿using Microsoft.EntityFrameworkCore;
using SistemaTC.Core;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;

namespace SistemaTC.Services;

public class UserService(ITCContext dbContext) : IUserService
{
    public async Task<List<User>> GetUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(Guid userId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);

        if (user != null && !password.ValidHash(user.Password))
        {
            return null;
        }

        return user;
    }

    public async Task<(User? user, List<string> validationErrors)> AddAsync(User user)
    {
        var validationErrors = await ValidateUser(user).ToListAsync();

        if(validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        user.Password = user.Password.Hash();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return (user, []);
    }

    private async IAsyncEnumerable<string> ValidateUser(User user, bool newUser = true)
    {
        var existingUserCondition = dbContext.Users.Where(x => x.UserName == user.UserName);

        if (!newUser)
            existingUserCondition = existingUserCondition.Where(x => x.UserId != user.UserId);

        if (await existingUserCondition.AnyAsync(x => x.UserName == user.UserName))
        {
            yield return "Username already exists";
        }
    }
}
