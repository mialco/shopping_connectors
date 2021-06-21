using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace mialco.shopping.connector.StoreFront
{
	public class StoreFrontDbContext : DbContext
	{
		private string _connectionString;


		public StoreFrontDbContext(string connectionString ) : base()
		{
			_connectionString = connectionString;

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
				entity.Ignore("ProductColor");

				//entity.HasMany<ProductVariant>(pv => pv.ProductVariants)
				//.WithOne(p => p.Product)
				//.HasForeignKey(fk=>fk.ProductID)				
				//.HasPrincipalKey(pk=>pk.ProductID);
				//entity.HasMany<ProductStore>(ps => ps.ProductStores)
				//.WithOne(p => p.Product).HasForeignKey(fk => fk.ProductID)
				//.HasPrincipalKey(pk => pk.ProductID);
				//entity.HasOne(pt => pt.ProductTypeRef)
				//.WithOne(p => p.ProductRef).HasForeignKey<ProductType>(fk=>fk.ProductTypeID);
				
			}
			);


			//modelBuilder.Entity<Product>().HasMany(p => p.ProductVariants);




			modelBuilder.Entity<ProductVariant>(entity =>
			{
				entity.HasKey("VariantID");
				entity.HasOne<Product>(p => p.Product)
				 .WithMany(v => v.ProductVariants)
				  .HasPrincipalKey(pk => pk.ProductID)
				  .HasForeignKey(fk => fk.ProductID);
			});

			modelBuilder.Entity<Store1>(entity =>
			entity.HasKey("StoreID"));

			modelBuilder.Entity<ProductStore>(entity =>
			{
				entity.HasKey(p => new { p.ProductID, p.StoreID });
				entity.Ignore("Name");
				entity.Ignore("Description");
				entity.HasOne(pr => pr.Product).WithMany(pr => pr.ProductStores)
				.HasPrincipalKey(pk => pk.ProductID)
				.HasForeignKey(fk => fk.ProductID);
				entity.HasOne(st => st.Store).WithMany(ps => ps.ProductStores)
				.HasForeignKey(fk => fk.StoreID);
				//.HasPrincipalKey(pk=>pk.StoreID);
			}

		   );

			modelBuilder.Entity<ProductCategory>(entity =>
			{
				entity.HasKey(pk => new { pk.ProductID, pk.CategoryID });
				//entity.Ignore("DisplayOrder");
				//entity.Ignore("CreatedOn");
				//entity.Ignore("UpdatedOn");
				//entity.HasOne(p => p.Product).WithMany
				//	(c => c.ProductCategories).HasPrincipalKey(pk => pk.ProductID).
				//	HasForeignKey(fk => fk.ProductID);
			});

			modelBuilder.Entity<ProductType>(entity => {
				entity.HasKey(pk => pk.ProductTypeID);
				entity.HasOne<Product>(p => p.Product)
				.WithOne(pt => pt.ProductType)
				.HasPrincipalKey<Product>(pk => pk.ProductTypeID)
				.HasForeignKey<ProductType>(fk => fk.ProductTypeID);
				
			});

			// For setting up many to many: 
			//https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

		}

		public DbSet<Product> Product { get; set; }
		public DbSet<ProductVariant> ProductVariant { get; set; }
		public DbSet<Store1> Store { get; set; }
		public DbSet<ProductStore> ProductStore { get; set; }
		public DbSet<ProductCategory> ProductCategory { get; set; }
		public DbSet<ProductType> ProductType { get; set; }
		public DbSet<Category> Category { get; set; }

	}
}
