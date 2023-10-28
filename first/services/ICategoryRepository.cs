using first.Models;
using System.Collections.Generic;

namespace first.services
{
	public interface ICategoryRepository
	{
		int Create(Category cata);
		int Delete(int id);
		int DeleteProduct(int id);

		int getAdminId(int categoryId);
        List<Category> getAll();
		int Update(int id, Category cata); 
		string getNameById(int id);
		Category getById(int id);
		 int getIdByName(string name);

    }

}