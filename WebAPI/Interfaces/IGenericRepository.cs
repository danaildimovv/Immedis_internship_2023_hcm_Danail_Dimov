namespace WebAPI.Interfaces
{
    public interface IGenericRepository
    {
        Task<bool> SaveAsync();
    }
}
