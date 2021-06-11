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
    public partial class ProductTypeProductInformationShippingDetails
    {

        private decimal weightMajorField;

        private bool weightMajorFieldSpecified;

        private decimal weightMinorField;

        private bool weightMinorFieldSpecified;

        private decimal lengthField;

        private bool lengthFieldSpecified;

        private decimal widthField;

        private bool widthFieldSpecified;

        private decimal heightField;

        private bool heightFieldSpecified;

        private string packageTypeField;

        private string measurementSystemField;

        /// <remarks/>
        public decimal weightMajor
        {
            get
            {
                return this.weightMajorField;
            }
            set
            {
                this.weightMajorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool weightMajorSpecified
        {
            get
            {
                return this.weightMajorFieldSpecified;
            }
            set
            {
                this.weightMajorFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal weightMinor
        {
            get
            {
                return this.weightMinorField;
            }
            set
            {
                this.weightMinorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool weightMinorSpecified
        {
            get
            {
                return this.weightMinorFieldSpecified;
            }
            set
            {
                this.weightMinorFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lengthSpecified
        {
            get
            {
                return this.lengthFieldSpecified;
            }
            set
            {
                this.lengthFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool widthSpecified
        {
            get
            {
                return this.widthFieldSpecified;
            }
            set
            {
                this.widthFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool heightSpecified
        {
            get
            {
                return this.heightFieldSpecified;
            }
            set
            {
                this.heightFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string packageType
        {
            get
            {
                return this.packageTypeField;
            }
            set
            {
                this.packageTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string measurementSystem
        {
            get
            {
                return this.measurementSystemField;
            }
            set
            {
                this.measurementSystemField = value;
            }
        }
    }


}
