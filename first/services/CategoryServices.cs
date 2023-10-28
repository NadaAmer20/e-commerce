using first.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace first.services
{
	public class CategoryServices : ICategoryRepository
	{
		Context context;
		IProductRepository productServices;
		public CategoryServices(Context _context, IProductRepository _productServices)
		{
			context = _context;
			productServices = _productServices;
		}
		public string getNameById(int id)
		{
			Category cat = context.Categories.FirstOrDefault(c => c.Id == id);
		
			return cat.Name;
		}
		public Category getById(int id)
		{
			return context.Categories.FirstOrDefault(c => c.Id == id);
			 
		}
		public int getAdminId(int id)
		{
			return context.Categories.FirstOrDefault(c => c.Id == id).AdminId;
		}
		public List<Category> getAll()
		{
			return context.Categories.ToList();
		}



		public int Create(Category cata)
		{
		
			context.Categories.Add(cata);
			int raw = context.SaveChanges();
			return raw;
		}
		public int Update(int id, Category cata)
		{

			Category OldCata = context.Categories.FirstOrDefault(u => u.Id == id);
			OldCata.Name = cata.Name;
			OldCata.Description = cata.Description;
			int raw = context.SaveChanges();
			return raw;
		}
		public int Delete(int id)
		{
			Category Cata = context.Categories.FirstOrDefault(u => u.Id == id);
			List<Product> Products = productServices.getAll(id);
			foreach(var item in Products)
			{
				DeleteProduct(item.Id);
			}
			context.Categories.Remove(Cata);
			int raw = context.SaveChanges();
			return raw;
		}
		
		public int DeleteProduct(int id)
		{
			Product product = productServices.GetById(id);
			//Category category = context.Categories.Include(p => p.Products).FirstOrDefault(c=>c.Id== product.CategoryId);
			context.Products.Remove(product);
			//context.SaveChanges();
			//category.Products.Remove(product);
			//product.CategoryId = 12;
			return  context.SaveChanges();
			
		}
		public int getIdByName(string name)
		{
			return context.Categories.FirstOrDefault(c => c.Name == name).Id;
		}
		
	}
}
