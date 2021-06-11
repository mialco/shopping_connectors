using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace mialco.shopping.connector.EbayFeed.EbayClasses
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ProductVariationGroupType
    {

        private string groupIDField;

        private ProductVariationGroupTypeGroupInformation groupInformationField;

        /// <remarks/>
        public string groupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }

        /// <remarks/>
        public ProductVariationGroupTypeGroupInformation groupInformation
        {
            get
            {
                return this.groupInformationField;
            }
            set
            {
                this.groupInformationField = value;
            }
        }
    }

}
