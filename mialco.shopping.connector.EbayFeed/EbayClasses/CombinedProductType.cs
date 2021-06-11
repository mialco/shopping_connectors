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
    public partial class CombinedProductType : ProductType
    {

        private DistributionType distributionField;

        private InventoryType inventoryField;

        /// <remarks/>
        public DistributionType distribution
        {
            get
            {
                return this.distributionField;
            }
            set
            {
                this.distributionField = value;
            }
        }

        /// <remarks/>
        public InventoryType inventory
        {
            get
            {
                return this.inventoryField;
            }
            set
            {
                this.inventoryField = value;
            }
        }
    }


}
