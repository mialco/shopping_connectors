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
    public partial class ProductType
    {

        private SKUType sKUField;

        private ProductTypeProductInformation productInformationField;

        /// <remarks/>
        public SKUType SKU
        {
            get
            {
                return this.sKUField;
            }
            set
            {
                this.sKUField = value;
            }
        }

        /// <remarks/>
        public ProductTypeProductInformation productInformation
        {
            get
            {
                return this.productInformationField;
            }
            set
            {
                this.productInformationField = value;
            }
        }
    }


}
