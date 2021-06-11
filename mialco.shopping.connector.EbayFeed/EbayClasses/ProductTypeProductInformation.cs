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
    public partial class ProductTypeProductInformation
    {

        private string titleField;

        private string subtitleField;

        private ProductTypeProductInformationDescription descriptionField;

        private AttributeType[] attributeField;

        private string uPCField;

        private string iSBNField;

        private string eANField;

        private string brandField;

        private string mPNField;

        private string ePIDField;

        private string[] pictureURLField;

        private ProductTypeProductInformationConditionInfo conditionInfoField;

        private ProductTypeProductInformationShippingDetails shippingDetailsField;

        private string localizedForField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string subtitle
        {
            get
            {
                return this.subtitleField;
            }
            set
            {
                this.subtitleField = value;
            }
        }

        /// <remarks/>
        public ProductTypeProductInformationDescription description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attribute")]
        public AttributeType[] attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                this.uPCField = value;
            }
        }

        /// <remarks/>
        public string ISBN
        {
            get
            {
                return this.iSBNField;
            }
            set
            {
                this.iSBNField = value;
            }
        }

        /// <remarks/>
        public string EAN
        {
            get
            {
                return this.eANField;
            }
            set
            {
                this.eANField = value;
            }
        }

        /// <remarks/>
        public string Brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }

        /// <remarks/>
        public string MPN
        {
            get
            {
                return this.mPNField;
            }
            set
            {
                this.mPNField = value;
            }
        }

        /// <remarks/>
        public string ePID
        {
            get
            {
                return this.ePIDField;
            }
            set
            {
                this.ePIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pictureURL")]
        public string[] pictureURL
        {
            get
            {
                return this.pictureURLField;
            }
            set
            {
                this.pictureURLField = value;
            }
        }

        /// <remarks/>
        public ProductTypeProductInformationConditionInfo conditionInfo
        {
            get
            {
                return this.conditionInfoField;
            }
            set
            {
                this.conditionInfoField = value;
            }
        }

        /// <remarks/>
        public ProductTypeProductInformationShippingDetails shippingDetails
        {
            get
            {
                return this.shippingDetailsField;
            }
            set
            {
                this.shippingDetailsField = value;
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
