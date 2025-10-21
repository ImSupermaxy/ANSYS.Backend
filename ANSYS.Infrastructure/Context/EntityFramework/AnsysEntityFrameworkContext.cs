using ANSYS.Domain.Abstractions.Context.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ANSYS.Infrastructure.Context.EntityFramework
{
    public class AnsysEntityFrameworkContext : DbContext, IEntityFrameworkDBContext
    {
        public AnsysEntityFrameworkContext(
            DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnsysEntityFrameworkContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                int result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception occurred.", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
