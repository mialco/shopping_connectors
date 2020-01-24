using mialco.shopping.connector.entities;
using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using Xunit;

namespace mialco.shopping.connector.test
{
	public class ProductEntityTest
	{
		[Theory]
		[MemberData(nameof(getEqualsProductsPairs))]
		public void True_If_Two_Objects_Equals(Entity p1, Entity p2)
		{
			Assert.True(p1.Equals(p2));
		}

		[Theory]
		[MemberData(nameof(getUnEqualsProductsPairs))]
		public void Should_Fail_If_Products_Are_Diferent(Entity p1, Entity p2)
		{
			Assert.False(p1.Equals(p2));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(-200)]
		[InlineData(0)]
		public void Should_Fail_If_Assign_Zero_Or_Negative_Identifier(int id)
		{
			Assert.Throws<ArgumentOutOfRangeException>(()=>new GenericProductEntity(id));
			var p = new GenericProductEntity();
			Assert.Throws<ArgumentOutOfRangeException>(() => p.AssignId(id));
		}


		public static IEnumerable<object[]> getEqualsProductsPairs()
		{
			var productList = new List<object[]>();

			var p1 = new GenericProductEntity();
			var p2 = new GenericProductEntity();

			//2  empty products 
			productList.Add(new object[] { p1, p2 });
			//
			// 2 produts with the same ID, name
			p1 = new GenericProductEntity(22);
			p2 = new GenericProductEntity(22);
			p1.Name = "Product1";
			p2.Name = "Product1";
			productList.Add(new object[] { p1, p2 });
			return productList;
			
		}

		public static IEnumerable<object[]> getUnEqualsProductsPairs()
		{
			var productList = new List<object[]>();
			var p1 = new GenericProductEntity();
			var p2 = new GenericProductEntity();

			p1 = new GenericProductEntity();
			// Add 2 null objects 
			productList.Add(new object[] { p1, null });

			//2  empty products 
			productList.Add(new object[] { p1, p2 });
			//
			// 2 produts with the same ID, name
			p1 = new GenericProductEntity(22);
			p2 = new GenericProductEntity(22);
			p1.Name = "Product1";
			p2.Name = "Product2";

			// products of different type should not be equal even with the same ID or if they are empty
			var p3 = new NewProductEntity();
			p1 = new GenericProductEntity();
			productList.Add(new object[] { p1, p3 });
			
			p3 = new NewProductEntity();
			p1 = new GenericProductEntity(21);
			p3.AssignId(21);
			p1.Name = "Name1";
			p3.Name = "Name1";
			productList.Add(new object[] { p1, p3 });


			return productList;

		}

		class NewProductEntity : Entity
		{
		}

	}
}
