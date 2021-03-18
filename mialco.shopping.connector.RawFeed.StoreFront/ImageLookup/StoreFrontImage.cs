using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	/// <summary>
	/// A class to hold the elements of an image used in the storefront ecommerce applications.
	/// The class will help in making a decision when selecting the image for the feed 
	/// </summary>
	class StoreFrontImage
	{
		private string _imagePath;
		private int _imageId;
		private FolderElement _folderElement;
		private GroupElement _groupElement;
		private ExtensionElement _extensionElement;
		private AttributeElement _attributeElement;
		private bool _isConfigured;

		public StoreFrontImage(string imagePath)
		{
			_isConfigured = false;
			_folderElement = new FolderElement(string.Empty,0 ,0);
			_groupElement = new GroupElement(string.Empty, 0);
			_extensionElement = new ExtensionElement(string.Empty,0,0);
			_attributeElement = new AttributeElement(string.Empty, 0,0);

			ParseImagePath(imagePath);

		}



		public string ImagePath => _imagePath;

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
		//public bool HasHighestPriority()
		//{
		//	//todo: This high priority thing may not be needed as we have the comparison functions. Review and remove if found not necessary
		//	var folderResult = _folderElement != null ? _folderElement.IsHighestPriority : false;
		//	var extensionResult = _extensionElement != null ? _extensionElement.IsHighestPriority : false;
		//	var attributeResult = _attributeElement != null ? _attributeElement.IsHighestPriority : false;
		//	var sectionResult = _groupElement != null ? _groupElement.IsHighestPriority : false;

		//	return folderResult & extensionResult & attributeResult & sectionResult;
		//}

		public List<StoreFrontImage> SortPriority(StoreFrontImage imageToCompare, StoreFrontImage additionalImageToCompare)
		{
			var sortedList = new  SortedList<String,StoreFrontImage>() ; 
			// We are using the storeImagePriority as a key in the SortedList object
			//We are adding an index at the end of the priority string 
			// in order to prevent errors in case two or more priorities have the same value (Sorted list requires unique keys)
			// We return the images in the order of the sorted list
			var storeImagePriority = CalculatePrioritySortKey(this) + "1";
			sortedList.Add(storeImagePriority, this);

			storeImagePriority = CalculatePrioritySortKey(imageToCompare) + "2";
			sortedList.Add(storeImagePriority, imageToCompare);

			storeImagePriority = CalculatePrioritySortKey(additionalImageToCompare) + "3";
			sortedList.Add(storeImagePriority, additionalImageToCompare);
			return sortedList.Values.ToList<StoreFrontImage>();

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
				priorities[0] = storeFrontImage._folderElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[1] = storeFrontImage._groupElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[2] = storeFrontImage._attributeElement.Priority.ToString().PadLeft(maxLength, '0');
				priorities[3] = storeFrontImage._extensionElement.Priority.ToString().PadLeft(maxLength, '0');
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
			var path = Path.GetDirectoryName(imageFullPath);
			var fileNameWitoutExtension = Path.GetFileNameWithoutExtension(imageFullPath);
			var fileExtension = Path.GetExtension(imageFullPath);
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
				var attribute = parts[2];
				var attributePriority = ImageElementPriorityConfiguration.GetElementPriority(attribute, ImageElementsType.Attribute);
				_attributeElement = new AttributeElement(parts[2], attributePriority, 0);
			}
			var extensionPriority = ImageElementPriorityConfiguration.GetElementPriority(fileExtension, ImageElementsType.Extension);
			_extensionElement = new ExtensionElement(fileExtension, extensionPriority, 0);

			var folderPriority = ImageElementPriorityConfiguration.GetElementPriority(path, ImageElementsType.Folder);
			_folderElement = new FolderElement(path, folderPriority, 0);
			//prepare image path for URL 
			if (imageFullPath.StartsWith("\\")) imageFullPath = imageFullPath.TrimStart('\\');
			_imagePath = imageFullPath.Replace("\\", "/");

			IsConfigured = true;
		}

	}
}
