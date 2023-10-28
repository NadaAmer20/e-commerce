using first.Models;
using System.Collections.Generic;

namespace first.services
{
    public interface IUserRepository
    {
        int Create(User user);
        int Delete(int id);
        List<User> getAll();
        User GetUserById(int id);
        User GetUserByEmail(string Email);
        User GetUserByPassword(string pass);
        int Update(int id, User user);
    }
}