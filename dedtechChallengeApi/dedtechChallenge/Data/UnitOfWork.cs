using DedtechChallenge.Repositories;
using DedtechChallenge.Repositories.Interfaces;

namespace DedtechChallenge.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DedtechChallengeContext _context;

        public IProductRepository Products { get; private set; } = null!;

        public UnitOfWork(DedtechChallengeContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
