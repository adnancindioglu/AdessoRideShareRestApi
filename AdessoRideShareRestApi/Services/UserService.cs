using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Services
{
    public class UserService : IUserService
    {

        private readonly IContextService _dbContext;

        public UserService(IContextService dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Create(UserModel user)
        {
            user.CreatedDate = DateTime.Now;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> Update(UserModel user)
        {
            try
            {
                user.UpdatedDate = DateTime.Now;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.Entry(user).Property(c => c.CreatedDate).IsModified = false;
                _dbContext.Entry(user.Travel).State = EntityState.Modified;

                var num = await _dbContext.SaveChangesAsync();
                return num > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await Get(id);
                _dbContext.Users.Remove(user);
                var num = await _dbContext.SaveChangesAsync();
                return num > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserModel>> Get()
        {
            return await _dbContext.Users.Include(c => c.Travel).ToListAsync();
        }

        public async Task<UserModel> Get(Guid id)
        {
            return await _dbContext.Users.Where(c => c.UserId == id).Include(c => c.Travel).FirstOrDefaultAsync();
        }

        public async Task<bool> Validate(Guid id)
        {
            return await _dbContext.Users.Where(c => c.UserId == id).CountAsync() > 0;
        }
    }
}
