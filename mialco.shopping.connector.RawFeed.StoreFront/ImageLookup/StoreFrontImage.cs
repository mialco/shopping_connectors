using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	/// <summary>
	/// A class to hold the elements of an image used in the storefront ecommerce applications.
	/// The class will help in making a decision when selecting the image for the feed 
	/// </summary>
	class StoreFrontImage
	{
		private readonly string _imageFullPath;
		private int _imageId;
		private FolderElement _folderElement;
		private GroupElement _groupElement;
		private ExtensionElement _extensionElement;
		private AttributeElement _attributeElement;
		private bool _isConfigured;

		public StoreFrontImage(string imageFullPath)
		{
			_isConfigured = false;
			this._imageFullPath = imageFullPath;
			ParseImagePath(imageFullPath);
		}



		public string ImageFullPath => _imageFullPath;

		public int ImageId { get => _imageId; private set => _imageId = value; }

		/// <summary>
		/// It indicates that the object was sucessfully configured. 
		/// This is to allow the objects using it to know if it has all the information and it can be be used accuratelly
		/// </summary>
		public bool IsConfigured { get => _isConfigured; private set => _isConfigured = value; }

		/// <summary>
		/// Return true if all the elements are at their highest priority (the lowest value for the priority field)
		/// </summary>
		/// <returns></returns>
		public bool HasHighestPriority()
		{
			//todo: This high priority thing may not be needed as we have the comparison functions. Review and remove if found not necessary
			var folderResult = _folderElement != null ? _folderElement.IsHighestPriority : false;
			var extensionResult = _extensionElement != null ? _extensionElement.IsHighestPriority : false;
			var attributeResult = _attributeElement != null ? _attributeElement.IsHighestPriority : false;
			var sectionResult = _groupElement != null ? _groupElement.IsHighestPriority : false;

			return folderResult & extensionResult & attributeResult & sectionResult;
		}

		public List<StoreFrontImage> SortPriority(StoreFrontImage imageToCompare, StoreFrontImage alternateImageToCompare)
		{
			var result = new SortedList<String,StoreFrontImage>() ;
			var storeImagePriority = CalculatePrioritySortKey(this);
			result.Add(storeImagePriority, this);

			storeImagePriority = CalculatePrioritySortKey(imageToCompare);
			result.Add(storeImagePriority, imageToCompare);

			storeImagePriority = CalculatePrioritySortKey(alternateImageToCompare);
			result.Add(storeImagePriority, alternateImageToCompare);
			return result.Values as List<StoreFrontImage> ;
		}


		/// <summary>
		/// We buid a string composed of all priorities in the StoreFrontImage, left padded with zeroes up to max int
		/// We use this string to compare priorities of the differnt FrontStoreImages
		/// The Null StoreFrontImages priority is considered as MAX int
		/// </summary>
		/// <param name="storeFrontImage"></param>
		/// <returns></returns>
		private string CalculatePrioritySortKey(StoreFrontImage storeFrontImage)
		{
			var result = string.Empty;
			var maxValueString = int.MaxValue.ToString();
			var maxLength = maxValueString.Length; 
			var priorities = new string[] {maxValueString,maxValueString,maxValueString, maxValueString };

			if (storeFrontImage != null)
			{
				priorities[0] = _folderElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[1] = _groupElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[2] = _attributeElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[3] = _extensionElement.Priority.ToString().PadLeft(maxLength, '0');
			}

			result = string.Join("",priorities);

			return result;
		}
		/// <summary>
		/// This method will parse the full path of the image pased as a parameter
		/// into the elements and will establish the priority for each element 
		/// The following are the elements composing the source image file 
		/// <productId>_<group>_<attribute>.<imgExtenssion>
		/// </summary>
		/// <param name="imageFullPath"></param>
		private void ParseImagePath(string imageFullPath)
		{
			//TODO: build a test method for this 
			imageFullPath = (imageFullPath ?? string.Empty).Trim();

			if (string.IsNullOrEmpty(imageFullPath)) return;
			var path = Path.GetDirectoryName(_imageFullPath);
			var fileNameWitoutExtension = Path.GetFileNameWithoutExtension(_imageFullPath);
			var fileExtension = Path.GetExtension(_imageFullPath);
			var parts = fileNameWitoutExtension.Split(new string[] { "_" }, StringSplitOptions.None);
			if (parts.Length < 1) return;
			int imageId = 0;
			if (! int.TryParse(parts[0].Trim(), out imageId)) return;

			_imageId = imageId;

			if (parts.Length >= 2)
			{
				_groupElement = new GroupElement(parts[1], 0);
			}
			else
			{
				_attributeElement = new AttributeElement(string.Empty, 0, 0);
			}

			if (parts.Length >= 3)
			{
				var attribune = parts[2];
				var attributePriority = ImageElementPriorityConfiguration.GetElementPriority(attribune, ImageElementsType.Attribute);
				_attributeElement = new AttributeElement(parts[2], attributePriority, 0);
			}
			var extensionPriority = ImageElementPriorityConfiguration.GetElementPriority(fileExtension, ImageElementsType.Extension);
			_extensionElement = new ExtensionElement(fileExtension, extensionPriority, 0);
			IsConfigured = true;
		}

	}
}
