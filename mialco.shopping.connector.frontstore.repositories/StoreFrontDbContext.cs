using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;


namespace mialco.shopping.connector.frontstore.repositories
{

	/// <summary>
	/// https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
	/// </summary>
	public class StoreFrontDbContext : DbContext
	{
		public StoreFrontDbContext() : base()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server =.\SQLExpress; Database = irosepetals; Trusted_Connection = True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<Product>(entity =>
			//entity.Property(e => e.LongDescription)
			//.HasColumnName("Summary"));


			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey("ProductID");
				entity.HasMany<ProductVariant>(pv => pv.ProductVariants)
				.WithOne(p => p.Product)
				.HasForeignKey(fk=>fk.ProductID);
				//.HasPrincipalKey("ProductID");
				
			}
			);


			//modelBuilder.Entity<Product>().HasMany(p => p.ProductVariants);



			modelBuilder.Entity<Product>(entity =>
			entity.Ignore("ProductColor"));

			modelBuilder.Entity<ProductVariant>(entity =>
			{ entity.HasKey("VariantID");

			});

			modelBuilder.Entity<Store>(entity =>
			entity.HasKey("StoreID"));

			modelBuilder.Entity<ProductStore>(entity => {
				entity.HasKey( p=> new { p.ProductID, p.StoreID});
				entity.Ignore("Name");
				entity.Ignore("Description");
				entity.HasOne(pr => pr.Product).WithMany(pr => pr.ProductStores)
				//.HasPrincipalKey(pk => pk.ProductID)
				.HasForeignKey(fk => fk.ProductID);
				entity.HasOne(st => st.Store).WithMany(ps => ps.ProductStores)
				.HasForeignKey(fk => fk.StoreID);
				//.HasPrincipalKey(pk=>pk.StoreID);
		   }

		   );

			// For setting up many to many: 
			//https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

		}	

		public DbSet<Product> Product { get; set; }
		public DbSet<ProductVariant> ProductVariant { get; set; }
		public DbSet<Store> Store { get; set; }
		public DbSet<ProductStore> ProductStore { get;set;}
    }
}
