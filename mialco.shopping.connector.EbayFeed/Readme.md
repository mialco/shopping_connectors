*** Documentation for the EBAY feed requirements

https://developer.ebay.com/devzone/merchant-products/mipng/user-guide-en/default.html#definitions-product-feed.html

* Title The title of the listing. Titles are limited to 80 characters. If you have a longer title, we recommended that you set the full-length title to a CustomField so that it can be used in your product's description template.
* Subtitle Additional product information that is shown below the product title. Note: Subtitles are limited to 55 characters.
* Prouct Description - 
	Limit to 800 character as much as posible
	It can be used with html tags like <div><span>blah blah blah</span></div>
	Limit the charactes inside span to 800 characters

	Notice in the example above that the <div> tag has vocab and typeof attributes. As in the example, the value of these attributes must be set to "https://schema.org" and "Product", respectively. The <span> tag has a property attribute that must be set to "description".
	Tags allowed between the span tags 
	Ordered and unordered list tags (<ol>, </ol>, <ul>, </ul>, <li>, and </li>). Each list element (<li>element1</li>) will count as three characters toward the limit. The other necessary tags for lists (<ol>, </ol>, <ul>, </ul>, and </li>) will not count towards the character limit
Each &nbsp; (non-breaking space) will count one character towards the character limit
Each <br> or <br/> tag will count 50 characters towards the character limit

For the description field, the program will need to implement a "cleaner" routine to detect and remove any HTML tags 