using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Provider.Base.Storeable
{
    public class PropertyAttribute
    {
        public string PropertyName;
        public PropertyInfo Property;
        public StoreableSetting SettingsAttribute;
    }

    [Serializable()]
    public abstract class BaseSettings : ISerializable
    {
        public Hashtable GetSettings() {
            Hashtable settings = new Hashtable();
            
            IEnumerable<PropertyAttribute> results = GetStoreableAttributes();
            Type klass = GetType();
            foreach (PropertyAttribute s in results) {
                string storeWithKey = s.SettingsAttribute.AsName ?? s.Property.Name;
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
                                             where result.PropertyName == key
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
                   select new PropertyAttribute() {
                       Property = prop,
                       SettingsAttribute = attribute,
                       PropertyName = attribute.AsName ?? prop.Name
                   }).ToList()
               );
            }

            return allAttributes;
        }
        
        public BaseSettings(SerializationInfo info, StreamingContext context) {

            if (info == null) {
                return;
            }

            List<PropertyAttribute> properties = GetStoreableAttributes();

            Hashtable table = new Hashtable();

            foreach (PropertyAttribute property in properties) {
                table.Add(property.PropertyName, info.GetValue(property.PropertyName, property.Property.PropertyType));
            }

            SetSettings(table);
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            Hashtable data = GetSettings();

            foreach (string item in data.Keys) {
                info.AddValue(item, data[item]);
            }
        }

        public abstract ContainerSerializationBinder GetSerializationBinder();
    }

}
