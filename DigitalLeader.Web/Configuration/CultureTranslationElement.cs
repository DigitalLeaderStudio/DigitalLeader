namespace DigitalLeader.Web.Configuration
{
    using System.Configuration;

    public class CultureTranslationElement : ConfigurationElement
    {
        [ConfigurationProperty("Action", IsRequired = true)]
        public string Action
        {
            get
            {
                return (string)base["Action"];
            }
            set
            {
                base["Action"] = value;
            }
        }

        [ConfigurationProperty("Controller", IsRequired = true)]
        public string Controller
        {
            get
            {
                return (string)base["Controller"];
            }
            set
            {
                base["Controller"] = value;
            }
        }

        [ConfigurationProperty("ControllerNamespace", IsRequired = true)]
        public string ControllerNamespace
        {
            get
            {
                return (string)base["ControllerNamespace"];
            }
            set
            {
                base["ControllerNamespace"] = value;
            }
        }

        [ConfigurationProperty("Value", IsRequired = true, IsKey = true)]
        public string Value
        {
            get
            {
                return (string)base["Value"];
            }
            set
            {
                base["Value"] = value;
            }
        }
    }
}