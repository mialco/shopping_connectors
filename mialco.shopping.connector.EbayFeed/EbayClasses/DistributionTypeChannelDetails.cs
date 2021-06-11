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
    public partial class DistributionTypeChannelDetails
    {

        private string channelIDField;

        private int categoryField;

        private bool categoryFieldSpecified;

        private int secondaryCategoryField;

        private bool secondaryCategoryFieldSpecified;

        private string shippingPolicyNameField;

        private DistributionTypeChannelDetailsShippingCostOverrides shippingCostOverridesField;

        private string paymentPolicyNameField;

        private string returnPolicyNameField;

        private string maxQuantityPerBuyerField;

        private DistributionTypeChannelDetailsPricingDetails pricingDetailsField;

        private string storeCategory1NameField;

        private string storeCategory2NameField;

        private bool applyTaxField;

        private bool applyTaxFieldSpecified;

        private string taxCategoryField;

        private string vATPercentField;

        private string templateNameField;

        private DistributionTypeChannelDetailsCustomField[] customFieldsField;

        private bool eligibleForEbayPlusField;

        private bool eligibleForEbayPlusFieldSpecified;

        private int lotSizeField;

        private bool hideBuyerDetailsField;

        private bool hideBuyerDetailsFieldSpecified;

        /// <remarks/>
        public string channelID
        {
            get
            {
                return this.channelIDField;
            }
            set
            {
                this.channelIDField = value;
            }
        }

        /// <remarks/>
        public int category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool categorySpecified
        {
            get
            {
                return this.categoryFieldSpecified;
            }
            set
            {
                this.categoryFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int secondaryCategory
        {
            get
            {
                return this.secondaryCategoryField;
            }
            set
            {
                this.secondaryCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool secondaryCategorySpecified
        {
            get
            {
                return this.secondaryCategoryFieldSpecified;
            }
            set
            {
                this.secondaryCategoryFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string shippingPolicyName
        {
            get
            {
                return this.shippingPolicyNameField;
            }
            set
            {
                this.shippingPolicyNameField = value;
            }
        }

        /// <remarks/>
        public DistributionTypeChannelDetailsShippingCostOverrides shippingCostOverrides
        {
            get
            {
                return this.shippingCostOverridesField;
            }
            set
            {
                this.shippingCostOverridesField = value;
            }
        }

        /// <remarks/>
        public string paymentPolicyName
        {
            get
            {
                return this.paymentPolicyNameField;
            }
            set
            {
                this.paymentPolicyNameField = value;
            }
        }

        /// <remarks/>
        public string returnPolicyName
        {
            get
            {
                return this.returnPolicyNameField;
            }
            set
            {
                this.returnPolicyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string maxQuantityPerBuyer
        {
            get
            {
                return this.maxQuantityPerBuyerField;
            }
            set
            {
                this.maxQuantityPerBuyerField = value;
            }
        }

        /// <remarks/>
        public DistributionTypeChannelDetailsPricingDetails pricingDetails
        {
            get
            {
                return this.pricingDetailsField;
            }
            set
            {
                this.pricingDetailsField = value;
            }
        }

        /// <remarks/>
        public string storeCategory1Name
        {
            get
            {
                return this.storeCategory1NameField;
            }
            set
            {
                this.storeCategory1NameField = value;
            }
        }

        /// <remarks/>
        public string storeCategory2Name
        {
            get
            {
                return this.storeCategory2NameField;
            }
            set
            {
                this.storeCategory2NameField = value;
            }
        }

        /// <remarks/>
        public bool applyTax
        {
            get
            {
                return this.applyTaxField;
            }
            set
            {
                this.applyTaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool applyTaxSpecified
        {
            get
            {
                return this.applyTaxFieldSpecified;
            }
            set
            {
                this.applyTaxFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string taxCategory
        {
            get
            {
                return this.taxCategoryField;
            }
            set
            {
                this.taxCategoryField = value;
            }
        }

        /// <remarks/>
        public string VATPercent
        {
            get
            {
                return this.vATPercentField;
            }
            set
            {
                this.vATPercentField = value;
            }
        }

        /// <remarks/>
        public string templateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("customField", IsNullable = false)]
        public DistributionTypeChannelDetailsCustomField[] customFields
        {
            get
            {
                return this.customFieldsField;
            }
            set
            {
                this.customFieldsField = value;
            }
        }

        /// <remarks/>
        public bool eligibleForEbayPlus
        {
            get
            {
                return this.eligibleForEbayPlusField;
            }
            set
            {
                this.eligibleForEbayPlusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool eligibleForEbayPlusSpecified
        {
            get
            {
                return this.eligibleForEbayPlusFieldSpecified;
            }
            set
            {
                this.eligibleForEbayPlusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int lotSize
        {
            get
            {
                return this.lotSizeField;
            }
            set
            {
                this.lotSizeField = value;
            }
        }

        /// <remarks/>
        public bool hideBuyerDetails
        {
            get
            {
                return this.hideBuyerDetailsField;
            }
            set
            {
                this.hideBuyerDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hideBuyerDetailsSpecified
        {
            get
            {
                return this.hideBuyerDetailsFieldSpecified;
            }
            set
            {
                this.hideBuyerDetailsFieldSpecified = value;
            }
        }
    }

}
