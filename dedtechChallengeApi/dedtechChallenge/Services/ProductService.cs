using DedtechChallenge.Data;
using DedtechChallenge.Models;
using DedtechChallenge.Repositories.Interfaces;
using DedtechChallenge.Services.Interfaces;

namespace DedtechChallenge.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        private readonly DedtechChallengeContext _context;

        public ProductService(IProductRepository productRepository, DedtechChallengeContext context)
        {
            _repository = productRepository;
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _repository.GetAllAndSortBy(sortBy => sortBy.Id);

            return products;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _repository.SaveAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _repository.GetByIdAsync(productId);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productSaved = await GetByIdAsync(product.Id);

            if (productSaved != null)
            {
                productSaved.Title = product.Title;
                productSaved.Description = product.Description;
                productSaved.Price = product.Price;
                productSaved.UpdatedAt = DateTime.Now;

            _repository.Update(productSaved);
            await _context.SaveChangesAsync();

            return productSaved;
            } else
            {
                return product;
            }

        }

        public async Task DeleteAsync(int productId)
        {
            var productSaved = await GetByIdAsync(productId);

            _repository.Delete(productSaved);

            await _context.SaveChangesAsync();
        }
    }
}
