using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.configuration
{
	public enum Sections
	{
		ApplicationSettings,
		RawFeedBuilderSettings,
		CategoriesSettings,
	}

	public enum SubSections
	{
		None,
		Instances,
		Folders,
		Files
	}

	public enum ApplicationSettingKeys
	{
			CreateFolders,
			Outputfolders
	}

	enum FolderKeys
	{
		InputFolder,
		OutputFolder,
	}


	enum RawFeedBuilderKeys
	{
		DefaultfCurrency
	}

	enum FilesKeys
	{
		CategoriesBaseFile,
		CategoryMappingBase,
		MarketingPlatformCategoriesBase,
		ImageListBase,
		XmlOutputFeedBase,
	}

	enum ApplicationInstanceKeys
	{
		Name,
		Description,
		ConnectionString,
	}

	public enum MarketingPlatforms
	{ 
		Google,
		Ebay,
		Amazon,
		Etsy
	}

}
