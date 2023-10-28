using first.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace first.services
{
	public class ProductServices : IProductRepository
	{
		Context context;

		public ProductServices(Context _context)
		{
			context = _context;
		
		}
		public List<Product> getAll(int CategoryId)
		{
			Category category =  context.Categories.Include(p => p.Products).FirstOrDefault(c => c.Id == CategoryId);
			return category.Products.ToList();
		}

		public List<Product> getAll()
		{
			return context.Products.ToList();
		}

		public Product GetById(int id)
		{
			return context.Products.FirstOrDefault(u => u.Id == id);
		}
		public int Create(Product product, IFormFile file)
		{

			string fileName = file.FileName;//Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
			using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			product.Image = fileName;
			context.Products.Add(product);
			int raw = context.SaveChanges();
			return raw;
		}
		public int Update(int id, Product NewProduct, IFormFile file)
		{
			string fileName = file.FileName;
			string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
			using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			Product OldProduct = context.Products.FirstOrDefault(u => u.Id == id);
			// OldProduct.Id = OldProduct.Id;
			OldProduct.Price = NewProduct.Price;
			OldProduct.Description = NewProduct.Description;
			OldProduct.StockQuantity = NewProduct.StockQuantity;
			OldProduct.Image = fileName;
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
		/// ///
		/// 
		public Product getById(int id)
		{
			return context.Products.FirstOrDefault(p => p.Id == id);
		}
	}
}

