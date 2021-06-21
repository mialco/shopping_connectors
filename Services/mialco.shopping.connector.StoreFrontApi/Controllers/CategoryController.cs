using mialco.shopping.connector.intefaces;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFrontApi.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mialco.shopping.connector.StoreFrontApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class CategoryController : Controller
	{
		IRepository<Category> _repo;
		public CategoryController(IRepository<Category> repo)
		{
			_repo = repo;
		}
		[HttpGet]
		public JsonResult List()
		{
			var categories = _repo.GetAll().Select(c => new CategoryDto { CategoryId = c.CategoryID, Name = c.Name });
			return new JsonResult(categories);
		}
	}
}
