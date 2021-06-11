using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mialco.configuration
{
	public class ApplicationSettings
	{

		public Folders Folders { get; set; }
		public IReadOnlyDictionary<string, ApplicationInstanceSettings> ApplicationInstances;
		public Files Files { get; set; }
		public bool CreateFolders { get; set; }
		public string DefaultInstance { get; set; }

		public FeedPlatforms FeedPlatforms  {get;set;}

	}
}
