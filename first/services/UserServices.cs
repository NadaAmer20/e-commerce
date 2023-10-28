using first.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace first.services
{
    public class UserServices : IUserRepository
    {
        Context context;
        public UserServices(Context _context)
        {
            context = _context;
        }
        public List<User> getAll()
        {
            return context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByEmail(string Email)
        {
              User user = context.Users.FirstOrDefault(u => u.Email == Email);
              return user; 
        }
        public User GetUserByPassword(string pass)
        {
            return context.Users.FirstOrDefault(u => u.Password == pass);
        }
        public int Create(User user)
        {
            context.Users.Add(user);
            int raw = context.SaveChanges();
            return raw;
        }
        public int Update(int id, User user)
        {

            User UserOld = context.Users.FirstOrDefault(u => u.Id == id);
            UserOld.FirstName = user.FirstName;
            UserOld.LastName = user.LastName;
            UserOld.Address = user.Address;
            UserOld.UserName = user.UserName;
            UserOld.Phone = user.Phone;
            UserOld.Email = user.Email;
            UserOld.Password = user.Password;
            UserOld.ConfirmPassword = user.ConfirmPassword;
            int raw = context.SaveChanges();
            return raw;
        }
        public int Delete(int id)
        {
            User user = context.Users.FirstOrDefault(u => u.Id == id);
            context.Users.Remove(user);
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
