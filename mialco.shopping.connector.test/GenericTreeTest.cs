using System;
using System.Collections.Generic;
using System.Text;
//using Xunit;
using mialco.utilities;
using mialco.shopping.objectvalues;

namespace mialco.shopping.connector.test
{
	
	public class GenericTreeTest
	{
		//[Fact]
		//[Theory]
		//[MemberData(nameof(GetTreeTestData))]
		//public void ShouldGetTheCorrectcategories(TreeTestData testData)
		public void ShouldGetTheCorrectcategories()
		{
			var x = 1;
			//Create the root node
			IEnumerable<object[]> x1 = GetTreeTestData() ;
			//var x2 = (TreeTestData) x1. ;
			foreach ( var x2 in x1)
			{
				var x3 = ((TreeTestData)x2[0]);
				var rootNode = x3.RootNode;
				var testData1 = x3.ListOfNodesAndResults;

				//var root = (List<GenericTree)x1[0] testData.RootNode;
				foreach (var node in testData1)
				{
					var path = rootNode.AddWithPath(rootNode, node.Item1, 0);
					//Assert.Equal(path, node.Item2);
					if (path != node.Item2)
					{
						Console.WriteLine($"path {path} not verified");
					}
				}


			}



		}

			//Assert.Equal(1, 1);
		


		public static IEnumerable<object[]> GetTreeTestData()
		{
			object[] obj ;
			List<object[]> result = new List<object[]>();

			var td = new TreeTestData();
			var sc = new GenericStoreCategory(447, "Amazon Animals", 0 );
			var node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Amazon Animals"));

			sc = new GenericStoreCategory(451, "Amazon Autism Tees", 0 );
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Amazon Autism Tees"));

			sc = new GenericStoreCategory(20, "Wedding", 0);
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Wedding"));

			sc = new GenericStoreCategory(28, "Soaps", 0 );
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Soaps"));

			sc = new GenericStoreCategory(4, "Organza", 20);
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Wedding > Organza"));

			sc = new GenericStoreCategory(19, "10000 Count Petals", 28);
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Soaps > 10000 Count Petals"));

			sc = new GenericStoreCategory(35, "Roses Petals", 19);
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Soaps > 10000 Count Petals > Roses Petals"));

			sc = new GenericStoreCategory(40, "Roses Petals Blue", 35);
			node = new GenericTree<GenericStoreCategory>(sc);
			td.ListOfNodesAndResults.Add(new Tuple<GenericTree<GenericStoreCategory>, string>(node, "Soaps > 10000 Count Petals > Roses Petals > Roses Petals Blue"));
			result.Add(new object[] { td });

			return result ;
		}
	}
	

		

	public class TreeTestData
	{

		public TreeTestData()
		{
			RootNode = new GenericTree<GenericStoreCategory>(new GenericStoreCategory(0, "", 0));
			ListOfNodesAndResults = new List<Tuple<GenericTree<GenericStoreCategory>, string>>();
		}
		public GenericTree<GenericStoreCategory> RootNode { get; set; }
		public List<Tuple<GenericTree<GenericStoreCategory>, string>> ListOfNodesAndResults { get; set; }

	}
	
}


