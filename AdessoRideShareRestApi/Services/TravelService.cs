using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Services
{
    public class TravelService : ITravelService
    {
        public Task<Guid> Create(TravelModel travel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid TravelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TravelModel>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<TravelModel> Get(Guid TravelId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TravelModel travel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Validate(Guid TravelId)
        {
            throw new NotImplementedException();
        }
    }
}
