namespace DigitalLeader.Web.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;

    [ConfigurationCollection(typeof(CultureElement), AddItemName = "CultureElement")]
    public class CultureCollection : ConfigurationElementCollection, IEnumerable<CultureElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var el = element as CultureElement;
            if (el != null)
            {
                return el.Name;
            }
            else
            {
                return null;
            }
        }

        public CultureElement this[int index]
        {
            get
            {
                return (CultureElement)BaseGet(index);
            }
        }

        public new IEnumerator<CultureElement> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}