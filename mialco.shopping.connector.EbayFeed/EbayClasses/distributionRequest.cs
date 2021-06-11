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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class distributionRequest
    {

        private DistributionType[] distributionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("distribution")]
        public DistributionType[] distribution
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
    }

}
