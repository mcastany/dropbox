namespace PhoneApp1.ViewModel
{
    using System.Xml.Serialization;

    public class Files
    {
        [XmlElement("name")]
        public string Name { get; set; }

        public bool? IsFolder { get; set; }
    }
}