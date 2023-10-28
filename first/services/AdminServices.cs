using first.Models;
using System.Collections.Generic;
using System.Linq;

namespace first.services
{
    public class AdminServices : IAdminRepository
    {
        Context context;
        public AdminServices(Context _context)
        {
            context = _context;
        }

        public Admin GetAdminByEmail(string Email)
        {
            return context.Admins.FirstOrDefault(x => x.Email == Email);
        }

		public Admin getAdminById(int id)
		{
            return context.Admins.FirstOrDefault(x => x.Id == id);
        }
        //public List<Admin> getAll()
        //{
        //    return context.Admins.ToList();
        //}

        //public Admin GetById(int id)
        //{
        //    return context.Admins.FirstOrDefault(u => u.Id == id);
        //}
        //public Admin GetByPass(string pass)
        //{
        //    return context.Admins.FirstOrDefault(u => u.Password == pass);
        //}
        //public int Create(Admin admin)
        //{
        //    context.Admins.Add(admin);
        //    int raw = context.SaveChanges();
        //    return raw;
        //}
        //public int Update(int id, Admin admin)
        //{
        //    Admin AdminOld = context.Admins.FirstOrDefault(u => u.Id == id);
        //    AdminOld.FirstName = admin.FirstName;
        //    AdminOld.LastName = admin.LastName;
        //    AdminOld.Address = admin.Address;
        //    AdminOld.UserName = admin.UserName;
        //    AdminOld.Phone = admin.Phone;
        //    AdminOld.Email = admin.Email;
        //    AdminOld.Password = admin.Password;
        //  //  AdminOld.ConfirmPassword = admin.ConfirmPassword;
        //    int raw = context.SaveChanges();
        //    return raw;
        ////}
        //public int Delete(int id)
        //{
        //    Admin admin = context.Admins.FirstOrDefault(u => u.Id == id);
        //    context.Admins.Remove(admin);
        //    int raw = context.SaveChanges();
        //    return raw;
        //}
    }
}
