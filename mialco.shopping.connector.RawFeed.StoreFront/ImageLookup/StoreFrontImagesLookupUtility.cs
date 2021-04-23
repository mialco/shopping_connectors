using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	/// <summary>
	/// This class loads images from the mages list and exposes a method to lookup the images by product id
	/// In order to use the class, an implementation needs to call the LoadImagesFromFile() method
	/// </summary>
	public class StoreFrontImagesLookupUtility
	{
		private const string ImagesFileName = "StoreFrontImagesList.txt"; 
		private readonly int _applicationInstanceId;
#pragma warning disable CS0414 // The field 'StoreFrontImagesLookupUtility._imagesLoaded' is assigned but its value is never used
		private bool _imagesLoaded;
#pragma warning restore CS0414 // The field 'StoreFrontImagesLookupUtility._imagesLoaded' is assigned but its value is never used
		private Dictionary<int, StoreFrontImage> _imageDictionary;
		private Dictionary<int, StoreFrontImage> _alternantImageDictionary;

		public StoreFrontImagesLookupUtility(string imagesFullFileName)
		{
			_imagesFileName = imagesFullFileName;
			_imagesLoaded = false;
			var appPath = utilities.AppUtilities.GetApplicationDataPath();
			var imagesListFileName = Path.Combine(appPath, ImagesFileName);
		}


		private string _imagesFileName;

		/// <summary>
		/// Loads images from an external file into internal images dictionary.
		/// As we read the files we call LoadImagesInTheLookupDictionary, which holde 2 dictionaries
		/// One for the main image and  another one for the alternant image
		/// </summary>
		/// <param name="imagesListFileName"></param>
		/// <returns></returns>
		public bool LoadImagesFromFile(string imagesListFileName)
		{
			var result = true;
			_imageDictionary = new Dictionary<int, StoreFrontImage>();
			_alternantImageDictionary = new Dictionary<int, StoreFrontImage>();
			try
			{
				using (var inputFile = new StreamReader(imagesListFileName))
				{
					//TODO: Record some stats for the loaded images
					while (!inputFile.EndOfStream)
					{
						var filePath = inputFile.ReadLine();
						var newStoreFrontImage = new StoreFrontImage(filePath);
						if (newStoreFrontImage.ImageId == 26)
						{
							var stophe = 1;
						}
						if (newStoreFrontImage.IsConfigured)
						{
							var existingImage = _imageDictionary.ContainsKey(newStoreFrontImage.ImageId) ? _imageDictionary[newStoreFrontImage.ImageId] : null;
							var existingAltImage = _alternantImageDictionary.ContainsKey(newStoreFrontImage.ImageId) ? _imageDictionary[newStoreFrontImage.ImageId] : null;

							if (existingImage == null)
							{
								_imageDictionary.Add(newStoreFrontImage.ImageId, newStoreFrontImage);
								existingImage = newStoreFrontImage;
								if (existingAltImage == null)
								{
									_alternantImageDictionary.Add(newStoreFrontImage.ImageId, newStoreFrontImage);
								}
								else
								{
									var sortedImages = existingImage.SortPriority(existingImage, existingAltImage);
									_imageDictionary.Add(sortedImages[0].ImageId, sortedImages[0]);
									_alternantImageDictionary[sortedImages[1].ImageId] = sortedImages[1];
								}
							}
							else
							{
								if (existingAltImage == null)
								{
									//we add this as alternane so we have non null images to sort
									existingAltImage = newStoreFrontImage;
									_alternantImageDictionary.Add(existingAltImage.ImageId, existingAltImage);
								}
								var sortedImages = newStoreFrontImage.SortPriority(existingImage, existingAltImage);
								_imageDictionary[sortedImages[0].ImageId] = sortedImages[0];
								_alternantImageDictionary[sortedImages[1].ImageId] = sortedImages[1];
							}
						}
						else
						{
							//toodo: log that the image object was not configured
						}
					}

				}

			}
			catch (Exception ex)
			{
				result = false;
				//todo: Write excep
				throw ex;
			}
			_imagesLoaded = true;
			return result;
		}

		/// <summary>
		/// (We may not use this method as it may be already be implemented in the StoreFrontImageClass)
		/// Creates an StoreFrontImage object with the provided filePath.
		/// If the StoreFrontImage Object IsConfigured property is true
		/// we check if the dictionary does not have the file then we add it to the dictionary.
		/// If it has it if the image from the dictionary has already the lowest priority achieved 
		/// by invoking the method HasHighestPriority(), we move on to the checking the alternant image dictionary 
		/// If not, we chose the smallest priority file and put it in the dictionary
		/// </summary>
		/// <param name="filePath"></param>
		protected virtual void LoadImagesInLookupDictionary(string filePath)
		{
			try
			{
				var storeFrontImage = new StoreFrontImage(filePath);
				if (!storeFrontImage.IsConfigured) return;
				// We checkk the image dictionary
				var imageId = storeFrontImage.ImageId;
				if (!_imageDictionary.ContainsKey(imageId))
				{
					_imageDictionary.Add(storeFrontImage.ImageId, storeFrontImage);
					return;
				}
				else
				{
					_imageDictionary[imageId] = storeFrontImage;
					
				}
			}
			catch (Exception ex)
			{
				//todo: Better Error handling
				Console.WriteLine(ex.ToString());
			}

			return;
		}

		/// <summary>
		/// Gets the main and alternative image relative path based on product id
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public string GetImagePath(int productId, out string alternativeImagePath)
		{
			string result = null;
			if (_imageDictionary.ContainsKey(productId)) result = _imageDictionary[productId].ImagePath;

			if (_alternantImageDictionary.ContainsKey(productId))
				alternativeImagePath = _alternantImageDictionary[productId].ImagePath;
			else
				alternativeImagePath = null;
			
			return result;
		}
		

	}

}
