using dvdrental.Entity;

namespace dvdrental.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin> AddAdminAsync(Admin admin);
    }
}
