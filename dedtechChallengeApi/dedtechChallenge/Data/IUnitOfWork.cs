using DedtechChallenge.Repositories.Interfaces;

namespace DedtechChallenge.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository Products { get; }

        Task<int> Save();
    }
}
