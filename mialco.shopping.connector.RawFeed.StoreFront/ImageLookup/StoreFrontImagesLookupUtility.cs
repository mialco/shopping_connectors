using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	/// <summary>
	/// 
	/// </summary>
	class StoreFrontImagesLookupUtility
	{
		private const string ImagesFileName = "ApplicationInstanceImages.txt"; 
		private readonly int _applicationInstanceId;
		private bool _imagesLoaded;
		private Dictionary<int, StoreFrontImage> _imageDictionary;
		private Dictionary<int, StoreFrontImage> _alternantimageDictionary;

		public StoreFrontImagesLookupUtility(int applicationInstanceId)
		{
			this._applicationInstanceId = applicationInstanceId;
			_imagesLoaded = false;
			var appPath = utilities.AppUtilities.GetApplicationDataPath();
			var imagesListFileName = Path.Combine(appPath, ImagesFileName);
			LoadImagesFromFile(imagesListFileName);
		}

		public int ApplicationInstanceId => _applicationInstanceId;



		/// <summary>
		/// Loads images from an external file into internal images dictionary.
		/// As we read the files we call LoadImagesInTheLookupDictionary, which holde 2 dictionaries
		/// One for the main image and  another one for the alternant image
		/// </summary>
		/// <param name="imagesListFileName"></param>
		/// <returns></returns>
		private bool LoadImagesFromFile(string imagesListFileName)
		{

			return false;
		}

		/// <summary>
		/// Creates an StoreFrontImage object with the provided filePath.
		/// If the StoreFrontImage Object IsConfigured property is true
		/// we check if the dictionary does not have the file then we add it to the dictionary.
		/// If it has it if the image from the dictionary has already the lowest priority achieved 
		/// by invoking the method HasHighestPriority(), we move on to the checking the alternant image dictionary 
		/// If not, we chose the smallest priority file and put it in the dictionary
		/// 
		/// </summary>
		/// <param name="filePath"></param>
		protected virtual void LoadImagesInLookupDictionary(string filePath)
		{
			try
			{
				var storeFrontImage = new StoreFrontImage(filePath);
				if (!storeFrontImage.IsConfigured) return;
				// We checkk the image dictionary
				if (!_imageDictionary.ContainsKey(storeFrontImage.ImageId))
				{
					_imageDictionary.Add(storeFrontImage.ImageId, storeFrontImage);
					return;
				}
				else
				{
					var existingImage = _imageDictionary[storeFrontImage.ImageId];
					if (existingImage.)
				}
			}
			catch (Exception ex)
			{
				//todo: Better Error handling
				Console.WriteLine(ex.ToString());
			}

			return;
		}

		

	}

}
