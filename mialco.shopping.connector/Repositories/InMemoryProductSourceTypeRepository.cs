using mialco.shopping.connector.Entities;
using mialco.shopping.connector.abstraction;
using mialco.utilities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.Repositories
{
	class InMemoryProductSourceTypeRepository : IRepository<ProductSourceType>
	{
		private List<ProductSourceType> _data;
		private MialcoLogger _logger;
		/// <summary>
		/// The parametrized costructor is to ensure future compatibility with other implementation which will require a connection string
		/// </summary>
		/// <param name="connectionString"></param>
		public InMemoryProductSourceTypeRepository(string connectionString, MialcoLogger logger)
		{
			BuildData();
		
		}
		public ProductSourceType GetById(long id)
		{
			var result = _data.FirstOrDefault(x => x.Id == id);
			return result;
		}

		public IEnumerable<ProductSourceType> GettAll()
		{
			return _data;
		}

		public ProductSourceType Insert(ProductSourceType item)
		{
			if (item == null) return null;
			if (_data.Exists(x => x.Id == item.Id))
			{
				try
				{
					_logger.LogError("An item with the same Id already exists", "");
					return null;

				}
				catch (Exception)
				{
					//ignore . Cannot right to the log
					return null;
				}
			}
			else
			{
				if (item.Id == 0)
				{
					item.Id = _data.Max(x => x.Id) + 1;
				}
				_data.Add(item) ;
				return item;				
			}
		}

		public bool Update(ProductSourceType item)
		{
			var result  = false;
			if (item == null) return result;
			if (!_data.Exists(x => x.Id == item.Id))
			{
				try
				{
					_logger.LogError("An item with the same Id Does not exist, therefore cannot be updated", "");
					return result;

				}
				catch (Exception)
				{
					//ignore . Cannot right to the log
					return result;
				}
			}
			else
			{
				var itemToUpdate = _data.Single(x => x.Id == item.Id);
				itemToUpdate.Name = item.Name;
				itemToUpdate.Description = item.Description;
				itemToUpdate.ModifiedBy = item.ModifiedBy ?? itemToUpdate.ModifiedBy;
				itemToUpdate.ModifiedTime = DateTime.Now;
				result = true;
			}
			return result;
		}


		private void BuildData()
		{
			_data = new List<ProductSourceType>();
			_data.Add(new ProductSourceType {Id=1,Name="StoreFront", Description="ASP.NET Storefront Shopping Cart Application", Created=DateTime.Now, ModifiedBy="mike" });
			_data.Add(new ProductSourceType { Id = 2, Name = "Nopcommerce", Description = "Nopcommerce Shopping Cart Application", Created = DateTime.Now, ModifiedBy = "mike" });
			_data.Add(new ProductSourceType { Id = 3, Name = "Local", Description = "Mialco Shopping Connector Managed products", Created = DateTime.Now, ModifiedBy = "mike" });
		}
	}



}
