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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProductVariationGroupTypeGroupInformationSharedProductInformationDescription
    {

        private string productDescriptionField;

        /// <remarks/>
        public string productDescription
        {
            get
            {
                return this.productDescriptionField;
            }
            set
            {
                this.productDescriptionField = value;
            }
        }
    }

}
