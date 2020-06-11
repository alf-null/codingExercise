using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace restAPI
{
    [Serializable, XmlRoot("item")]
    [XmlType("item")]
    public class Item
    {
        [XmlElement("id")]
        public long Id { set; get; }
        [XmlElement("guid")]
        public string Guid { set; get; }
    }
}
