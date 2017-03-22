namespace DigitalLeader.Web.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    [ConfigurationCollection(typeof(CultureTranslationElement), AddItemName = "CultureTranslationElement")]
    public class CultureTranslationElementCollection : ConfigurationElementCollection, IEnumerable<CultureTranslationElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureTranslationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var el = element as CultureTranslationElement;
            if (el != null)
            {
                return el.Value;
            }
            else
            {
                return null;
            }
        }

        public CultureTranslationElement this[int index]
        {
            get
            {
                return BaseGet(index) as CultureTranslationElement;
            }
        }

        public new IEnumerator<CultureTranslationElement> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                  .GetEnumerator();
        }
    }
}