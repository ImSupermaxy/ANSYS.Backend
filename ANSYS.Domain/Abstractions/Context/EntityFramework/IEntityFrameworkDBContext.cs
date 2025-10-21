namespace ANSYS.Domain.Abstractions.Context.EntityFramework
{
    public interface IEntityFrameworkDBContext : IDBContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
