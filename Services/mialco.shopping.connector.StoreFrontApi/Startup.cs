using mialco.shopping.connector.shared;
using mialco.shopping.connector.intefaces;
using mialco.shopping.connector.StoreFront;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mialco.shopping.connector.StoreFrontApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString(StringConstants.StoreFrontDbConnectionStringName);
			// Solution referenced here
			//https://www.connectionstrings.com/store-and-read-connection-string-in-appsettings-json/
			services.AddScoped<IRepository<Store1>>(repo => new StoreFrontStoreRepositoryEF(connectionString)); ;
			services.AddScoped<IProductRepository<Product>>(repo=>new StoreFrontProductRepositoryEF(connectionString));
			services.AddScoped<IRepository<Category>>(repo => new StoreFrontCategoryRepository(connectionString));
			services.AddControllers();
			
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			
		}
	}
}
