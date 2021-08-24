using mialco.shopping.connector.intefaces;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFrontApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mialco.shopping.connector.StoreFrontApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class StoreController : Controller
	{

		IRepository<Store1> _storeRepository;
		//ILogger _logger;

		public StoreController(IRepository<Store1> storeRepository ) : base()
		{			
			
			_storeRepository = storeRepository;
			
		}
		[HttpGet]
		public JsonResult List()
		{

			try
			{
				var stores = _storeRepository.GetAll().Where(s=>s.Published==1).Select(store => new StoreDto {StoreId=store.StoreID, Name=store.Name, StoreUri=store.ProductionURI });
				return new JsonResult(stores);
			}
			catch (Exception)
			{

				throw;
			}
		}


		// POST: StoreController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: StoreController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: StoreController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: StoreController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: StoreController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
