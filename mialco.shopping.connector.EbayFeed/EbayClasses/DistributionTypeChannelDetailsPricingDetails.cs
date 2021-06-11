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
    public partial class DistributionTypeChannelDetailsPricingDetails
    {

        private decimal listPriceField;

        private bool listPriceFieldSpecified;

        private decimal strikeThroughPriceField;

        private bool strikeThroughPriceFieldSpecified;

        private decimal minimumAdvertisedPriceField;

        private bool minimumAdvertisedPriceFieldSpecified;

        private string minimumAdvertisedPriceHandlingField;

        private bool soldOffEbayField;

        private bool soldOffEbayFieldSpecified;

        private bool soldOnEbayField;

        private bool soldOnEbayFieldSpecified;

        /// <remarks/>
        public decimal listPrice
        {
            get
            {
                return this.listPriceField;
            }
            set
            {
                this.listPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool listPriceSpecified
        {
            get
            {
                return this.listPriceFieldSpecified;
            }
            set
            {
                this.listPriceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal strikeThroughPrice
        {
            get
            {
                return this.strikeThroughPriceField;
            }
            set
            {
                this.strikeThroughPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool strikeThroughPriceSpecified
        {
            get
            {
                return this.strikeThroughPriceFieldSpecified;
            }
            set
            {
                this.strikeThroughPriceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal minimumAdvertisedPrice
        {
            get
            {
                return this.minimumAdvertisedPriceField;
            }
            set
            {
                this.minimumAdvertisedPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minimumAdvertisedPriceSpecified
        {
            get
            {
                return this.minimumAdvertisedPriceFieldSpecified;
            }
            set
            {
                this.minimumAdvertisedPriceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string minimumAdvertisedPriceHandling
        {
            get
            {
                return this.minimumAdvertisedPriceHandlingField;
            }
            set
            {
                this.minimumAdvertisedPriceHandlingField = value;
            }
        }

        /// <remarks/>
        public bool soldOffEbay
        {
            get
            {
                return this.soldOffEbayField;
            }
            set
            {
                this.soldOffEbayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool soldOffEbaySpecified
        {
            get
            {
                return this.soldOffEbayFieldSpecified;
            }
            set
            {
                this.soldOffEbayFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool soldOnEbay
        {
            get
            {
                return this.soldOnEbayField;
            }
            set
            {
                this.soldOnEbayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool soldOnEbaySpecified
        {
            get
            {
                return this.soldOnEbayFieldSpecified;
            }
            set
            {
                this.soldOnEbayFieldSpecified = value;
            }
        }
    }

}
