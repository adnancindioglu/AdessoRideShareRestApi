using AdessoRideShareRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Infrastructure
{
    public interface IUserService
    {
        public Task<Guid> Create(UserModel user);
        public Task<bool> Update(UserModel user);
        public Task<bool> Delete(Guid UserId);
        public Task<IEnumerable<UserModel>> Get();
        public Task<UserModel> Get(Guid UserId);
        public Task<bool> Validate(Guid UserId);
    }
}
