using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.EtsyFeed
{
	public class EtsyListingItem
	{
		public int quantity { get; set; }
		/// <summary>
		/// The listing's title string. 
		/// Valid title strings contain only letters, numbers, punctuation marks, mathematical symbols, whitespace characters, ™, ©, and ®. 
		/// (regex: /[^\p{L}\p{Nd}\p{P}\p{Sm}\p{Zs}™©®]/u) You can only use the %, :, & and + characters once each.
		/// </summary>
		public string title { get; set; }
		public string description { get; set; }
		public float price { get; set; }
		public string who_made { get; set; }

		public string when_made { get; set; }

		public int taxonomy_id { get; set; }

		public int shipping_profile_id { get; set; }

		public List<string> materials { get; set; }

		public int? shop_section_id { get; set; }
		public int? processing_min { get; set; }

		public int? processing_max { get; set; }
		public List<string> tags { get; set; }
		public List<string> styles { get; set; }
		public float? item_weight {get;set;}
		public float? item_length { get; set; }
		public float? item_width { get; set; }
		public float? item_height { get; set; }
		public string item_weight_unit { get; set; }
		public string item_dimensions_unit { get; set; }
		public bool? is_personalizable { get; set; }
		public bool ? personalization_is_required { get; set; }
		public int personalization_char_count_max	{ get; set; }
		public string personalization_instructions { get; set; }
		public List<int> production_partner_ids { get; set; }
		public List<int> image_ids { get; set; }
		/// <summary>
		/// When true, tags the listing as a supply product, else indicates that it's a finished product. 
		/// Helps buyers locate the listing under the Supplies heading. Requires 'who_made' and 'when_made'.
		/// </summary>
		public bool is_supply { get; set; }
		/// <summary>
		/// When true, a buyer may contact the seller for a customized order. 
		/// The default value is true when a shop accepts custom orders. 
		/// Does not apply to shops that do not accept custom orders.
		/// </summary>
		public bool is_customizable { get; set; }

		public bool is_taxable { get; set; }

	}
}
