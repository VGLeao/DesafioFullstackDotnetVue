using DedtechChallenge.Data;
using DedtechChallenge.Models;
using DedtechChallenge.Repositories.Interfaces;
using DedtechChallenge.Services.Interfaces;

namespace DedtechChallenge.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAndSortBy(sortBy => sortBy.Id);

            return products;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            DateTime now = DateTime.Now;
            product.CreatedAt = now;
            product.UpdatedAt = now;

            await _unitOfWork.Products.SaveAsync(product);

            await _unitOfWork.Save();

            return product;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _unitOfWork.Products.GetByIdAsync(productId);
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

                _unitOfWork.Products.Update(productSaved);
                await _unitOfWork.Save();

                return productSaved;
            }
            else
            {
                throw new BadHttpRequestException("Não foi encontrado o produto a ser editado");
            }

        }

        public async Task DeleteAsync(int productId)
        {
            var productSaved = await GetByIdAsync(productId);

            _unitOfWork.Products.Delete(productSaved);

            await _unitOfWork.Save();
        }
    }
}
