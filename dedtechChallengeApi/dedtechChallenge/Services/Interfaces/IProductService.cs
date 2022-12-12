using DedtechChallenge.Models;

namespace DedtechChallenge.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();

        Task<Product> GetByIdAsync(int productId);

        Task<Product> CreateAsync(Product entity);

        Task<Product> UpdateAsync(Product entity);

        Task DeleteAsync(int productId);
    }
}
