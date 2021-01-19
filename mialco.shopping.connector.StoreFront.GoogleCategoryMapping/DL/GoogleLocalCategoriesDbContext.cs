using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping.DL
{
	public class GoogleLocalCategoriesDbContext : DbContext
	{
		private string _connectionString;

		public GoogleLocalCategoriesDbContext(string connectionString) : base()
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
			modelBuilder.Entity<GoogleCategory>(entity =>
			{
				entity.HasKey("Id");
			});
			modelBuilder.Entity<StoreFrontCategoryMap>(entity =>
			entity.HasKey("Id"));
		}


		public DbSet<GoogleCategory> GoogleCategory { get; set; }
		public DbSet<StoreFrontCategoryMap> StoreFrontCategoryMap {get; set ;}

	}
}
