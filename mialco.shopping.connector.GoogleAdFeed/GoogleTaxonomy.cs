using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.GoogleAdFeed
{
	/// <summary>
	/// A class to hold store the google taxonomy
	/// Sample from the file:
	/// # Google_Product_Taxonomy_Version: 2019-07-10
	/// #1 - Animals & Pet Supplies
	/// 3237 - Animals & Pet Supplies > Live Animals
	/// 2 - Animals & Pet Supplies > Pet Supplies
	/// 3 - Animals & Pet Supplies > Pet Supplies > Bird Supplies
	/// 7385 - Animals & Pet Supplies > Pet Supplies > Bird Supplies > Bird Cage Accessories
	/// Lines starting with # are comments
	/// It is presented to the user as a flat file delimited by 2 different charaters
	///
	/// First element, delimited by a dash "-"	Is an integer identifier for the record
	/// The second element, to the right side of the dash represents a path into the taxonomy tree
	/// The elemets of the second elemet are delimited by the charater ">"
	/// This class will read the taxonomy from a file and put it into a tree structure
	/// </summary>
	public class GoogleTaxonomy
	{

	}
}
