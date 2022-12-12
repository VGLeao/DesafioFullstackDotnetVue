using Microsoft.EntityFrameworkCore;
using DedtechChallenge.Models;

namespace DedtechChallenge.Data
{
    public class DedtechChallengeContext : DbContext
    {
        public DedtechChallengeContext(DbContextOptions<DedtechChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
    }
}
