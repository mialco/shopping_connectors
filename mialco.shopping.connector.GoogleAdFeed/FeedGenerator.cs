using GoogleAdFeed;
using mialco.shopping.connector.FeedCommon;
using mialco.shopping.connector.RawFeed;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
//using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;



namespace mialco.shopping.connector.GoogleAdFeed
{
	/// <summary>
	/// Given a raw Feed shoudl create file with the proper format for google ad 
	/// </summary>
	public class FeedGenerator
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="FileName">The full path of the file name where the XML Feed will be written</param>
		/// <param name="feedRecords">A list of records extracted from a source (a shopping card application for example). 
		/// The records in this list are in a genercic format where the fields are stored in a dictionary   </param>
		/// <param name="feedProerties"></param>
		public void GenerateXmlFeed(string fileName, List<GenericFeedRecord> feedRecords, FeedProperties feedProperties)
		{
			

			//GoogleXmlFileWriter is a helper class which writes records in
			//an xml file formatted to align with the specifications requested 
			//by google shopping feed  
			using (GoogleXmlFileWriter xmlw = new GoogleXmlFileWriter(fileName))
			{
				var counter = 0;
				xmlw.OpenFeed(feedProperties.XmlFeedPrefix, feedProperties.XmlFeedNameSpace ,feedProperties.Title, feedProperties.Description,feedProperties.Link);
				//xmlw.StartItem();
				/* StporeFrontGoogleFeedMap is a class which maps the fields extracted from the 
				 * Storefront shopping cart to the filelds of the google feed xml file*/
				StoreFrontGoogleFeedMap storeFrontGoolgeFeedMap = new StoreFrontGoogleFeedMap();
				/* We write each reord of feedRecords (passed as parameter) to the google feed XML file */
				foreach (var rec in feedRecords)
				{

					//TODO: REMOVE AFTER TESTING
					counter++;
					//if (counter > 500) break;
					var value = string.Empty;
					//We write the record id first
					//xmlw.WriteItemElement("id", rec.ProductId, GooglePrefix, GoogleNamepace);
					//xmlw.WriteItemElement(  "title", GetFeedValueFromGenericRecord( rec, "Title"));
					//xmlw.WriteItemElement
					/* Then, we traverse the property mapping which contains the name of the google feed fields 
					 * mapped to the properties of the 
					 * current source (which in this case contains the data from the StoreFront database).
					 * For each propery, we look up for the field vale 
					* to the the name of the google feed field  */
					xmlw.StartItem();
					foreach (DictionaryEntry item in storeFrontGoolgeFeedMap)
					{
						//TODO take the try outside the loop with the option of continuing the loop in ase of error
						try
						{
							var feedValue = GetFeedValueFromGenericRecord(rec, item.Value.ToString());
							xmlw.WriteItemElement(item.Key.ToString(), feedValue, feedProperties.XmlFeedPrefix, feedProperties.XmlFeedNameSpace);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}

					}

				}
				xmlw.CloseFeed();
			}
		}
		
		/// <summary>
		/// Will return the value of the feed record stored under key
		/// If the key does not exits, will return empty string and will report the isue in the log 
		/// </summary>
		/// <param name="key">The Key in the generic record for which to extract the value</param>
		/// <returns></returns>
		public string GetFeedValueFromGenericRecord(GenericFeedRecord rec , string key)
		{
			var result = string.Empty;

			if (string.IsNullOrEmpty(key)) return string.Empty;
			try
			{
				if (!rec.FeedRecord.TryGetValue(key, out result))
				{
					result = string.Empty;
					//todo: Logging of the error
				}
				result = result ?? string.Empty;
			}
			catch (Exception)
			{

				//todo: add logging
			}
			return result;
		}


	}
}
