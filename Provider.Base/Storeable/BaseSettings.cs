using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Provider.Base.Storeable
{
    public class PropertyAttribute
    {
        public PropertyInfo Property;
        public StoreableSetting SettingsAttribute;
    }

    public abstract class BaseSettings
    {
        
        public Hashtable GetSettings() {
            Hashtable settings = new Hashtable();
            
            IEnumerable<PropertyAttribute> results = GetStoreableAttributes();
            Type klass = GetType();
            foreach (PropertyAttribute s in results) {
                string storeWithKey = s.SettingsAttribute.AsName != null ? s.SettingsAttribute.AsName : s.Property.Name;
                settings.Add(storeWithKey, klass.GetProperty(s.Property.Name).GetValue(this));
            }

            return settings;
        }

        public void SetSettings(Hashtable settings) {
            Type klass = GetType();
            List<PropertyAttribute> results = GetStoreableAttributes();

            foreach (string key in settings.Keys) {
                var value = settings[key];
                PropertyAttribute setting = (from result in results
                                             where 
                                                (result.SettingsAttribute.AsName != null && result.SettingsAttribute.AsName == key) ||
                                                (result.Property.Name == key)
                                             select result as PropertyAttribute).Single();

                klass.GetProperty(setting.Property.Name).SetValue(this, value);
            }
        }

        protected List<PropertyAttribute> GetStoreableAttributes() {

            PropertyInfo[] properties = GetType().GetProperties();

            List<PropertyAttribute> allAttributes = new List<PropertyAttribute>();
            foreach(PropertyInfo prop in properties) {
                allAttributes.AddRange(
                  (from attribute in (StoreableSetting[])Attribute.GetCustomAttributes(prop, typeof(StoreableSetting))
                   select new PropertyAttribute() { Property = prop, SettingsAttribute = attribute }).ToList()
               );
            }

            return allAttributes;
        }
    }
}
