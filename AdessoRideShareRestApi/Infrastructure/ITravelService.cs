using AdessoRideShareRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Infrastructure
{
    public interface ITravelService
    {
        public Task<Guid> Create(TravelModel travel);
        public Task<bool> Update(TravelModel travel);
        public Task<bool> Delete(Guid TravelId);
        public Task<IEnumerable<TravelModel>> Get();
        public Task<TravelModel> Get(Guid TravelId);
        public Task<bool> Validate(Guid TravelId);
    }
}
