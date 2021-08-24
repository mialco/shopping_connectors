# shopping_connectors

#shopping connector cli

## How to run 
Use case: Run a full feed for a store ID
> dotnet biz-connector-cli.dll <command> <command-scope> --store-id <nn> --instance <instance-name>
or
> dotnet biz-connector-cli.dll <command> <command-scope> -s <nn> -i <instance-name>

Example
> dotnet biz-connector-cli.dll feed full-feed -s 33 -i StoreFront-Dev

### Resources:
* Application configuration file : ShoppingConnectorConfiguration 
* Folders = Defined in the application configuration 
** Input Folder
** Output Folder
** Data Folder ?
* Filters
You can configure input data filters listing selected categories ids in a json file in the input folder. 
The filter is associated with an application instance and a store id
The filter file name shoud be <Instance-Name>_<store-id>_Filters.json
Example: StoreFrond-Dev_33_Filters.json

The content of the filter name 

### Application Installation and configuration
06/04/2021
Dployment notes

* Config file settings\folders adn Files have new variable
			"GoogleCategoriesFile": "google_taxonomy-with-ids.en-US.txt",
			"EbayCategoriesFile": "Ebay_US_Category_Changes_(May2021)_NFT-update.csv",

		Under application instances we have new item
						"DefaultEbayCategoryId": 158994,

		Remove from settings:
		"MarketingPlatformCategoriesBase": "taxonomy-with-ids.en-US.txt",




