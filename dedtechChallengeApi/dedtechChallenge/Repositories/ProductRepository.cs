using DedtechChallenge.Data;
using DedtechChallenge.Models;
using DedtechChallenge.Repositories.Interfaces;

namespace DedtechChallenge.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(DedtechChallengeContext context) : base(context) { }
    }
}
