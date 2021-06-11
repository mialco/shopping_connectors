using System.Collections.Generic;
using System.Text;
using Xunit;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.GoogleAdFeed;
using System.IO;
using mialco.shopping.connector.FeedCommon;

namespace mialco.shopping.connector.test
{

	public class GoogleAddFeedGeneratorTest
	{

		[Theory]
		[MemberData(nameof(GetGenericFeedRecordForValuesTest))]
		public void Should_Return_Record_Value_From_Feed(GenericFeedRecord genericFeedRecord , string key, string expectedResult  )
		{
			var x = GetListOfFeedRecordsForXmlFeedTest();
			var feedGenerator = new FeedGenerator();
			var expectedValue = string.Empty;
			var value = feedGenerator.GetFeedValueFromGenericRecord(genericFeedRecord, key);
			Assert.Equal(expectedResult, value);
		}

		[Theory]
		[MemberData (nameof(GetListOfFeedRecordsForXmlFeedTest))]
		public void Should_Write_Xml_Feed(string outputFileName, List<GenericFeedRecord> feedRecords, FeedProperties feedProperties)
		{
			FeedGenerator feedGenerator = new FeedGenerator();
			// Delete Output File of exists
			if (File.Exists(outputFileName)) File.Delete(outputFileName);
			feedGenerator.GenerateXmlFeed(outputFileName, feedRecords, feedProperties);
			Assert.True(File.Exists(outputFileName));
		}

		/// <summary>
		/// Used to produce data for testing extracting the record value from the GenericFeedRecord
		/// Returns an object composed of : 
		/// * a GenericFeedRecord, 
		/// * The key to be extracted from the record 
		/// * the expected value extracted from the record
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<object[]> GetGenericFeedRecordForValuesTest()
		{
			var recList = new List<object[]>();
			var feedRecord = GetGenericFeedRecordForValuesTest();
			recList.Add(new object[] { feedRecord,  "Title", "New Bride and Groom Wedding Baseball Caps-Black" });

			//recList.Add(new object[] {fullFileName,  feedRecords, "Title", "MyTitle",  });
			//recList.Add(new object[] { feedRecord, null, string.Empty });
			//recList.Add(new object[] { feedRecord, string.Empty, string.Empty });
			//recList.Add(new object[] { feedRecord, "EmptyValue", string.Empty });
			//recList.Add(new object[] { feedRecord, "NullValue", string.Empty });
			//recList.Add(new object[] { feedRecord, "NonExistingKey", string.Empty });




			return recList;
		}

		private List<GenericFeedRecord> GetGenericFeedRecordsForTesting()
		{
			var recordDict = new Dictionary<string, string> {
				{ "Title", "New Bride and Groom Wedding Baseball Caps-Black" },
				{ "Description","You are getting both the Bride and the Groom Cap.Have fun with these.One size fits all.Great wedding gift.Fun to wear prior to the wedding to parties and at the reception for the honeymoon and bachelor party.They are made 100 % cotton, with a beautiful embroidery" },
				{ "ProductType", "Blowout Sale!" },
				{ "Category" , "Apparel & amp; Accessories & gt; Clothing & gt; Shirts & amp; Tops"},
				{ "Link" , "http://www.amoretees.com/p-306-new-bride-and-groom-wedding-baseball-caps-black.aspx" },
				{ "ImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "AdditionalImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "Condition" , "new"},
				{ "Availability", "InStock"},
				{ "Price", "34.9900 USD"},
				{ "SalePrice","32.222 USD" }


			};

			var feedRecord = new GenericFeedRecord { Id = 1, ProductId = "1", FeedRecord = recordDict };
			var feedRecords = new List<GenericFeedRecord> { feedRecord };
			{
			/*					

			  < g:additional_image_link />

			   < g:condition > new</ g:condition >

				   < g:availability >in stock </ g:availability >

					   < g:price > 34.9900 USD </ g:price >

							< g:sale_price > 19.9900 USD </ g:sale_price >

								 < g:sale_price_effective_date />

								  < g:brand > Amore Tees </ g:brand >

									   < g:gtin />

										< g:mpn > Hats </ g:mpn >

											 < g:item_group_id />

											  < g:color />

											   < g:material />

												< g:pattern />

												 < g:size />

												  < g:gender > unisex </ g:gender >

													   < g:age_group > adult </ g:age_group >

															< g:shipping_weight > 0.5000 LBS </ g:shipping_weight >

																 < g:shipping_length />

																  < g:shipping_width />

																   < g:shipping_height />

																	< g:excluded_destination />

																	 < g:expiration_date />

																	  < g:adwords_redirect />

																	   < g:custom_label_0 />

																		< g:custom_label_1 />

																		 < g:custom_label_2 />

																		  < g:custom_label_3 />

																		   < g:custom_label_4 />

																			< g:identifier_exists />

																			 < g:multipack />

																			  < g:adult />

																			   < mobile_link />

																			   < g:availability_date />

																				< g:size_type />

																				 < g:size_system />

																				  < g:shipping_label />

																				   < g:is_bundle />

																					< g:unit_pricing_measure />

																					 < g:unit_pricing_base_measure />

																					  < g:promotion_id />

				*/
		}

			return feedRecords;

		}

		/// <summary>
		/// Used to produce data for testing Producing Google XML Feed from a list of GenericFeedRecord
		/// Returns list of  GenericFeedRecords : 
		/// </summary>
		/// <returns></returns>
		private List<GenericFeedRecord> GetGenericFeedRecordsForTestingXml()
		{
			var recordDict = new Dictionary<string, string> {
				{ "Title", "New Bride and Groom Wedding Baseball Caps-Black" },
				{ "Description","You are getting both the Bride and the Groom Cap.Have fun with these.One size fits all.Great wedding gift.Fun to wear prior to the wedding to parties and at the reception for the honeymoon and bachelor party.They are made 100 % cotton, with a beautiful embroidery" },
				{ "ProductType", "Blowout Sale!" },
				{ "Category" , "Apparel & amp; Accessories & gt; Clothing & gt; Shirts & amp; Tops"},
				{ "Link" , "http://www.amoretees.com/p-306-new-bride-and-groom-wedding-baseball-caps-black.aspx" },
				{ "ImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "AdditionalImageLink" , "http://www.amoretees.com/images/product/medium/306.jpg"},
				{ "Condition" , "new"},
				{ "Availability", "InStock"},
				{ "Price", "34.9900 USD"},
				{ "SalePrice","32.222 USD" }			


			};

			var feedRecord = new GenericFeedRecord { Id = 1, ProductId = "1", FeedRecord = recordDict };
			var feedRecords = new List<GenericFeedRecord> { feedRecord };
			{
				/*					

				  < g:additional_image_link />

				   < g:condition > new</ g:condition >

					   < g:availability >in stock </ g:availability >

						   < g:price > 34.9900 USD </ g:price >

								< g:sale_price > 19.9900 USD </ g:sale_price >

									 < g:sale_price_effective_date />

									  < g:brand > Amore Tees </ g:brand >

										   < g:gtin />

											< g:mpn > Hats </ g:mpn >

												 < g:item_group_id />

												  < g:color />

												   < g:material />

													< g:pattern />

													 < g:size />

													  < g:gender > unisex </ g:gender >

														   < g:age_group > adult </ g:age_group >

																< g:shipping_weight > 0.5000 LBS </ g:shipping_weight >

																	 < g:shipping_length />

																	  < g:shipping_width />

																	   < g:shipping_height />

																		< g:excluded_destination />

																		 < g:expiration_date />

																		  < g:adwords_redirect />

																		   < g:custom_label_0 />

																			< g:custom_label_1 />

																			 < g:custom_label_2 />

																			  < g:custom_label_3 />

																			   < g:custom_label_4 />

																				< g:identifier_exists />

																				 < g:multipack />

																				  < g:adult />

																				   < mobile_link />

																				   < g:availability_date />

																					< g:size_type />

																					 < g:size_system />

																					  < g:shipping_label />

																					   < g:is_bundle />

																						< g:unit_pricing_measure />

																						 < g:unit_pricing_base_measure />

																						  < g:promotion_id />

					*/

			}
			return feedRecords;

		}

		/// <summary>
		/// Used to produce data for testing the creation of the Gogle Feed XML file
		/// from a list of GenericFeedRecord
		/// Returns an object composed of : 
		/// * Output File Name
		/// * List of  GenericFeedRecord, 
		/// * Feed Properties Object
		/// * the expected value extracted from the record
		/// 
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<object[]> GetListOfFeedRecordsForXmlFeedTest()
		{
			var recList = new List<object[]>();

			var path = @"C:\Data\Mialco\Development\ShoppingConnectorResources";
			var fileName = "GooleFeedTest.xml";
			var outputFilename = Path.Combine(path, fileName);
			var feedRecords = GetListOfFeedRecordsForXmlFeedTest();
			var feedProperties = new FeedProperties("GoogleTestFeed","http://googletest.com","This is a the test feed for google feed xml","","");

			var objArray = new object[] {outputFilename, feedRecords,feedProperties };

			recList.Add(objArray);

			return recList;
		}
	}
}
