using first.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace first.services
{
	public interface IProductRepository
	{
		int Create(Product product, IFormFile file);
		int Delete(int id);
		List<Product> getAll(int CategoryId);
		List<Product> getAll();
		Product getById(int id);
		Product GetById(int id);
	    

        int Update(int id, Product product, IFormFile file);
	}
}