using mialco.shopping.connector.Orchestrator;
using mialco.shopping.connector.RawFeed.StoreFront;
using mialco.shopping.connector.shared;
using mialco.shopping.connector.StoreFront;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static mialco.shopping.connector.Orchestrator.StoreFrontOrchestratorZero;


namespace mialco.shopping.connector.test
{
	public class StoreFrontOrchestratorZeroTest
	{
		[Theory]
		[MemberData(nameof(getStoresForProductionUri))]
		public void Should_Get_The_Right_Path_Frome_Store_Record(Store1 store, string expectedResult)
		{
			WebStoreDeploymentType deploymentType = WebStoreDeploymentType.Production;
			StoreFrontOrchestratorZero sfoz = new StoreFrontOrchestratorZero(1, deploymentType);
			var calcURI = sfoz.GetStoreURI(store, deploymentType);
			Assert.Equal(calcURI, expectedResult);


		}


		[Theory]
		[InlineData(1, 2, "size1", "color1", "1_2_size1_color1")]
		[InlineData(1, 2, null, null, "1_2_NS_NC")]
		[InlineData(1, 2, null, "color1", "1_2_NS_color1")]
		[InlineData(1, 2, "size1", null, "1_2_size1_NC")]
		[InlineData(1, 2, "", "", "1_2_NS_NC")]
		[InlineData(1, 2, "", "color1", "1_2_NS_color1")]
		[InlineData(1, 2, "size1", "", "1_2_size1_NC")]
		public void Should_Create_Correct_Raw_Product_Id(int productId, int variantId,
			string sizeskuModifier, string colorSkuModifier,
			string expectedResult)
		{
			StoreFrontOrchestratorZero orch = new StoreFrontOrchestratorZero(1, WebStoreDeploymentType.Production);
			string rawProdId = orch.GenerateProductId(productId, variantId, sizeskuModifier, colorSkuModifier);
			Assert.Equal(rawProdId, expectedResult);
			}


		[Theory]
		[MemberData(nameof(get_Product_Attributes))]
		public void Should_Return_List_Of_Size_Options(string attributeListWithPriceModifiers, string skuModifiersList, List<ProductAttribute> size)
		{
			var sfo = new StoreFrontOrchestratorZero(1,WebStoreDeploymentType.Production);
			List<ProductAttribute> result = sfo.GetProductAttributes( attributeListWithPriceModifiers,skuModifiersList,typeof(SizeOption)	);
			for (int i = 0; i < result.Count; i++)
			{

				var resultItem = result[i];
				var item = size[i];
				Assert.Equal(resultItem, item);
			}
		}

		[Theory]
		[InlineData(null, "", 0.0)]
		[InlineData("", "", 0.0)]
		[InlineData("color0", "color0", 0.0)]
		[InlineData("color1[12]", "color1", 12.0)]
		[InlineData(" color1[12]", "color1", 12.0)]
		[InlineData("color1 [12]", "color1", 12.0)]
		[InlineData("color1 [ 12]", "color1", 12.0)]
		[InlineData("color1[12 ]", "color1", 12.0)]
		[InlineData("  color1 [ 12 ] ", "color1", 12.0)]
		[InlineData("color1[12.23]", "color1", 12.23)]
		[InlineData("color1[0]", "color1", 0)]
		[InlineData("color1[.]", "color1", 0)]
		[InlineData("color1[.0]", "color1", 0)]
		[InlineData("color1[ . ]", "color1", 0)]
		[InlineData("color1[0.0]", "color1", 0)]
		[InlineData("color1[00.00]", "color1", 0)]
		[InlineData("color1[0.0000]", "color1", 0)]
		[InlineData("color1[.0000]", "color1", 0)]
		[InlineData("color1[.2345]", "color1", 0.2345)]
		[InlineData("color1[.23450000]", "color1", 0.2345)]
		[InlineData("color1 [ 12.123456]", "color1", 12.123456)]
		public void GetPriceFromAttributeString_Should_Return_Name_and_Price(string attributeString, string name, decimal price)
		{
			var sfo = new StoreFrontOrchestratorZero(1, WebStoreDeploymentType.Production);
			Tuple<string, decimal> tp  ;
			tp = sfo.GetPriceFromAttributeString(attributeString);
			Assert.Equal(tp.Item1, name);
			Assert.Equal(tp.Item2 ,price);
		}

		public static IEnumerable<object[]> getStoresForProductionUri()
		{
			var storelist = new List<object[]>();
			var s1 = new Store1()
			{
				StoreID = 1,
				DevelopmentDirectoryPath = "dev/path",
				DevelopmentPort = "33",
				DevelopmentURI = "www.devuri.com",
				ProductionDirectoryPath = "prod/path",
				ProductionPort = "33",
				ProductionURI = "www.produri.com",
				StagingDirectoryPath = "staging/path",
				StagingPort = "33",
				StagingURI = "www.staging.com"
			};

			string expectedResult = "http://www.produri.com:33/prod/path";
			storelist.Add(new object[] { s1, expectedResult });

			s1 = new Store1()
			{
				StoreID = 1,
				DevelopmentDirectoryPath = "dev/path",
				DevelopmentPort = "33",
				DevelopmentURI = "www.devuri.com",
				ProductionDirectoryPath = "prod/path",
				ProductionPort = "33",
				ProductionURI = "http://www.produri.com",
				StagingDirectoryPath = "staging/path",
				StagingPort = "33",
				StagingURI = "www.staging.com"
			};
			storelist.Add(new object[] { s1, expectedResult });

			expectedResult = "http://www.produri.com:33/prod/path";
			storelist.Add(new object[] { s1, expectedResult });

			s1 = new Store1()
			{
				StoreID = 1,
				DevelopmentDirectoryPath = "dev/path",
				DevelopmentPort = "33",
				DevelopmentURI = "https://www.devuri.com",
				ProductionDirectoryPath = "prod/path",
				ProductionPort = "33",
				ProductionURI = "https://www.produri.com",
				StagingDirectoryPath = "staging/path",
				StagingPort = "33",
				StagingURI = "https://www.staginguri.com"
			};
			expectedResult = "https://www.produri.com:33/prod/path";
			storelist.Add(new object[] { s1, expectedResult });

			s1 = new Store1()
			{
				StoreID = 1,
				DevelopmentDirectoryPath = "dev/path",
				DevelopmentPort = "",
				DevelopmentURI = "www.devuri.com",
				ProductionDirectoryPath = "prod/path",
				ProductionPort = "",
				ProductionURI = "www.produri.com",
				StagingDirectoryPath = "staging/path",
				StagingPort = "",
				StagingURI = "www.staginguri.com"
			};
			expectedResult = "http://www.produri.com/prod/path";
			storelist.Add(new object[] { s1, expectedResult });




			return storelist;
		}
		//sizes : XSmall,Small[1],Medium[1],Large[1],XLarge[2],2X-Large[4],3X-Large[4],4X-Large[5],5X-Large[5]
		//SizeSKUMdifiers :XS,S,M,L,XL,2XL,3XL,4XL,5XL

		//Colors: white[0],pink[2],ash[2],black[4]
		// ColorSkuModifiers white,pink,ash,black

		public static IEnumerable<object[]> get_Product_Attributes()
		{
			var result = new List<object[]>();
			var sizes = "XSmall,Small[1],Medium[1],Large[1],XLarge[2],2X-Large[4]," +
				"3X-Large[4],4X-Large[5],5X-Large[5]";
			var sizeSKUModifiers = "XS,S,M,L,XL,2XL,3XL,4XL,5XL";
			var attributes = new List<ProductAttribute> {
				new SizeOption {Name="XSmall",AddedPrice=0,SkuModifier="XS" } ,
				new SizeOption {Name="Small",AddedPrice=1,SkuModifier="S" } ,
				new SizeOption {Name="Medium",AddedPrice=1,SkuModifier="M" },
				new SizeOption {Name="Large",AddedPrice=1,SkuModifier="L" } ,
				new SizeOption {Name="XLarge",AddedPrice=2,SkuModifier="XL" } ,
				new SizeOption {Name="2X-Large",AddedPrice=4,SkuModifier="2X" } ,
				new SizeOption {Name="3X-Large",AddedPrice=4,SkuModifier="3X" } ,
				new SizeOption {Name="4X-Large",AddedPrice=5,SkuModifier="4X" } ,
				new SizeOption {Name="5X-Large",AddedPrice=5,SkuModifier="SX" } ,
			};
			result.Add(new object [] {sizes,sizeSKUModifiers,attributes });

			return result;
		}
	}

	public static class StoreExtension
		{
			public static Store1 GetInstanceForTest(this Store1 store)
			{
				
				var s1 = new Store1()
				{
					StoreID = 1,
					DevelopmentDirectoryPath = "dev/path",
					DevelopmentPort = "33",
					DevelopmentURI = "www.devuri.com",
					ProductionDirectoryPath = "prod/path",
					ProductionPort = "33",
					ProductionURI = "www.produri.com",
					StagingDirectoryPath = "staging/path",
					StagingPort = "33",
					StagingURI = "www.staging.com"
				};
				return s1;
			}


		}

}
