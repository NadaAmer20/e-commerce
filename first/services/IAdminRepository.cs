using first.Models;

namespace first.services
{
    public interface IAdminRepository
    {
        Admin GetAdminByEmail(string Email);
        Admin getAdminById(int id);
    }
}