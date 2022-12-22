using Microsoft.EntityFrameworkCore;
using Service1.Data;
using Service1.Entities;

namespace Service1.Repositories
{
    public class ProductOfferService : IProductOfferService
    {
        private readonly DbContextClass _dbContext;

        public ProductOfferService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Offer>> GetOfferListAsync()
        {
            return await _dbContext.Offer.ToListAsync();
        }

        public async Task<Offer> GetOfferByIdAsync(int Id)
        {
            return await _dbContext.Offer.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Offer> AddOfferAsync(Offer offer)
        {
            var result = _dbContext.Offer.Add(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Offer> UpdateOfferAsync(Offer offer)
        {
            var result = _dbContext.Offer.Update(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteOfferAsync(int Id)
        {
            var filteredData = _dbContext.Offer.Where(x => x.Id == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
            return result != null ? true : false;
        }
    }

}
