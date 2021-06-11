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
    public partial class ProductVariationGroupTypeGroupInformation
    {

        private string[] variationVectorField;

        private ProductVariationGroupTypeGroupInformationSharedProductInformation sharedProductInformationField;

        private string picturesVaryOnField;

        private string localizedForField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("name", IsNullable = false)]
        public string[] variationVector
        {
            get
            {
                return this.variationVectorField;
            }
            set
            {
                this.variationVectorField = value;
            }
        }

        /// <remarks/>
        public ProductVariationGroupTypeGroupInformationSharedProductInformation sharedProductInformation
        {
            get
            {
                return this.sharedProductInformationField;
            }
            set
            {
                this.sharedProductInformationField = value;
            }
        }

        /// <remarks/>
        public string picturesVaryOn
        {
            get
            {
                return this.picturesVaryOnField;
            }
            set
            {
                this.picturesVaryOnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localizedFor
        {
            get
            {
                return this.localizedForField;
            }
            set
            {
                this.localizedForField = value;
            }
        }
    }


}
