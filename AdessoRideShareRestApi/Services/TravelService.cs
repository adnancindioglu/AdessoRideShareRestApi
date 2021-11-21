using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Services
{
    public class TravelService : ITravelService
    {

        private readonly IContextService _dbContext;

        public TravelService(IContextService dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Create(TravelModel travel)
        {
            travel.CreatedDate = DateTime.Now;
            _dbContext.Travels.Add(travel);
            await _dbContext.SaveChangesAsync();
            return travel.TravelId;
        }

        public async Task<bool> Update(TravelModel travel)
        {
            try
            {
                if (travel.Passengers.Count <= travel.SeatingCapacity)
                {
                    travel.UpdatedDate = DateTime.Now;
                    _dbContext.Entry(travel).State = EntityState.Modified;
                    foreach (var item in travel.Passengers)
                    {
                        _dbContext.Entry(item).State = EntityState.Added;
                    }
                   
                    _dbContext.Entry(travel).Property(c => c.CreatedDate).IsModified = false;

                    var num = await _dbContext.SaveChangesAsync();
                    return num > 0;
                }
                else
                {
                    _dbContext.Entry(travel).State = EntityState.Unchanged;
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var travel = await Get(id);
                _dbContext.Travels.Remove(travel);
                var num = await _dbContext.SaveChangesAsync();
                return num > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TravelModel>> Get(string startCity = null, string endCity = null)
        {
            var query = _dbContext.Travels.Where(t => t.IsActive == true).AsQueryable();

            if(string.IsNullOrWhiteSpace(startCity) == false && string.IsNullOrWhiteSpace(endCity) == false)
            {
                query = query.Where(t => t.StartCity == startCity && t.EndCity == endCity).AsQueryable();
            }

            return await query.Include(t => t.Passengers).ToListAsync();
        }
        
        public async Task<TravelModel> Get(Guid id)
        {
            return await _dbContext.Travels.Where(c => c.TravelId == id).FirstOrDefaultAsync();
        }

        public async Task<TravelModel> GetActive(Guid id)
        {
            return await _dbContext.Travels.Where(c => c.TravelId == id && c.IsActive == true ).FirstOrDefaultAsync();
        }

        public async Task<bool> Validate(Guid id)
        {
            return await _dbContext.Travels.Where(c => c.TravelId == id).CountAsync() > 0;
        }
    }
}
