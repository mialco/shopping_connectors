using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.EbayFeed.EbayClasses
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class inventoryRequest
    {

        private InventoryType[] inventoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("inventory")]
        public InventoryType[] inventory
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
