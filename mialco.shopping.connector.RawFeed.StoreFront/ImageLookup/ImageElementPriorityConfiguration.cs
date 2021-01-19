using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	internal static class ImageElementPriorityConfiguration
	{
		private static Dictionary<string, int> _folderPriorities;
		private static Dictionary<string, int> _attributesPriorities;
		private static Dictionary<string, int> _extensionPriorities;
		private static bool _prioritiesLoaded = false;
		//TODO: Get this from program configuration
		static ImageElementPriorityConfiguration()
		{
			if (!_prioritiesLoaded)
			{
				_prioritiesLoaded = true;
				// Folder Priorities
				_folderPriorities = new Dictionary<string, int>();
				_folderPriorities.Add(@"images\product", 0);
				_folderPriorities.Add(@"images\product\medium", 1);
				_folderPriorities.Add(@"images\product\small", 2);
				_folderPriorities.Add(@"images\product\large", 3);
				_folderPriorities.Add(@"images\product\micro", 4);
				_folderPriorities.Add(@"images\product\icon", 5);
				_folderPriorities.Add(@"images\product\swatch", 6);


				_attributesPriorities = new Dictionary<string, int>();
				_attributesPriorities.Add("", 0);
				_attributesPriorities.Add("white", 1);
				_attributesPriorities.Add("black", 2);
				_attributesPriorities.Add("blue", 3);
				_attributesPriorities.Add("ash", 4);
				_attributesPriorities.Add("red", 5);

				_extensionPriorities = new Dictionary<string, int>();
				_extensionPriorities.Add(".jpg", 0);
				_extensionPriorities.Add("jpg", 0);
				_extensionPriorities.Add("png", 1);
				_extensionPriorities.Add(".png", 1);
				_extensionPriorities.Add("gif", 2);
				_extensionPriorities.Add(".gif", 2);
			}
		}

		public static int GetElementPriority(string elementName, ImageElementsType imageElementsType)
		{
			int result = int.MaxValue;
			elementName = elementName ?? string.Empty;
			switch (imageElementsType)
			{
				case ImageElementsType.Attribute:
					result= (_attributesPriorities.ContainsKey(elementName)) ? _attributesPriorities[elementName] : result;
					break;

				case ImageElementsType.Folder:
					result = (_folderPriorities.ContainsKey(elementName)) ? _folderPriorities[elementName] : result;
					break;

				case ImageElementsType.Extension:
					result = (_extensionPriorities.ContainsKey(elementName)) ? _extensionPriorities[elementName] : result;
					break;

				default:
					result = int.MaxValue;
					break;
			}
		

			return result;

		}

	}

}
