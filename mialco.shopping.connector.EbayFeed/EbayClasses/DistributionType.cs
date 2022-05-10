﻿using System;
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
    public partial class DistributionType
    {

        private string sKUField;

        private DistributionTypeChannelDetails channelDetailsField;

        /// <remarks/>
        public string SKU
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

        private string localizedForField;

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



        /// <remarks/>
        public DistributionTypeChannelDetails channelDetails
        {
            get
            {
                return this.channelDetailsField;
            }
            set
            {
                this.channelDetailsField = value;
            }
        }
    }

}
