using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping.DL
{
	public class StoreCategoryDbContext : DbContext
	{
		private string _connectionString;
		//public StoreCategoryDbContext() : base()
		//{

		//}

		public StoreCategoryDbContext(string connectionString) : base()
		{
			_connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer(@"Server =.\SQLExpress; Database = irosepetals; Trusted_Connection = True;");
			optionsBuilder.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey("CategoryID");
			}
				);

		}
		public DbSet<Category> Category { get; set; }

	}
}
