using first.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace first.Controllers
{
	public class RemoteController : Controller
	{
		public IActionResult Index()
		{
			
			return View();
		}
		Context context	= new Context();
		public IActionResult EmailExist(string Email)
		{
			if(context.Users.FirstOrDefault(u => u.Email == Email) != null)
			{
				//true 
				return Json(true);
				//
			}
			//false
			else return Json(false);
		}
	}
}
