namespace WebAPI.Interfaces
{
    public interface IAuthentication
    {
        Task<string> GetSecurityToken();
    }
}
