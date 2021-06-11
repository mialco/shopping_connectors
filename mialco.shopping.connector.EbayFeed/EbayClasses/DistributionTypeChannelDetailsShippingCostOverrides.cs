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
    public partial class DistributionTypeChannelDetailsShippingCostOverrides
    {

        private decimal shippingCostField;

        private decimal additionalCostField;

        private decimal surchargeField;

        private string priorityField;

        private string shippingServiceTypeField;

        /// <remarks/>
        public decimal shippingCost
        {
            get
            {
                return this.shippingCostField;
            }
            set
            {
                this.shippingCostField = value;
            }
        }

        /// <remarks/>
        public decimal additionalCost
        {
            get
            {
                return this.additionalCostField;
            }
            set
            {
                this.additionalCostField = value;
            }
        }

        /// <remarks/>
        public decimal surcharge
        {
            get
            {
                return this.surchargeField;
            }
            set
            {
                this.surchargeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
            }
        }

        /// <remarks/>
        public string shippingServiceType
        {
            get
            {
                return this.shippingServiceTypeField;
            }
            set
            {
                this.shippingServiceTypeField = value;
            }
        }
    }

}
