using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            Type t = GetType();
            foreach (PropertyAttribute s in results) {
                settings.Add(s.SettingsAttribute.Name, t.GetProperty(s.Property.Name).GetValue(this));
            }

            return settings;
        }

        public void SetSettings(Hashtable settings) {
            Type t = GetType();
            List<PropertyAttribute> results = GetStoreableAttributes();

            foreach (string key in settings.Keys) {
                var value = settings[key];
                PropertyAttribute setting = (from result in results where result.SettingsAttribute.Name == key select result as PropertyAttribute).Single();
                t.GetProperty(setting.Property.Name).SetValue(this, value);
            }
        }

        protected List<PropertyAttribute> GetStoreableAttributes() {

            PropertyInfo[] properties = GetType().GetProperties();

            List<PropertyAttribute> allAttributes = new List<PropertyAttribute>();
            foreach(PropertyInfo prop in properties) {
                StoreableSetting[] attributes = (StoreableSetting[])Attribute.GetCustomAttributes(prop, typeof(StoreableSetting));

                foreach(StoreableSetting attribute in attributes) {
                    allAttributes.Add(new PropertyAttribute() { Property = prop, SettingsAttribute = attribute });
                }
            }

            return allAttributes;
        }
    }
}
