using Microsoft.VisualStudio.TestTools.UnitTesting;
using Provider.Base.Storeable;
using Provider.Base.Test.Fixtures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Base.Test
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void TestSettingsShouldOnlyHaveStorableSettingParameters() {
            SettingsParameters settings = new SettingsParameters();

            Hashtable settingsTable = settings.GetSettings();

            List<PropertyAttribute> expectedProperties = GetStoreableProperties(settings);

            foreach(String propertyName in settingsTable.Keys) {
                PropertyAttribute attribute = (
                    from prop in expectedProperties
                    where prop.PropertyName == propertyName
                    select prop as PropertyAttribute).First();

                Assert.IsNotNull(attribute);
                Assert.AreEqual(propertyName, attribute.PropertyName);
            }
        }

        [TestMethod]
        public void TestShouldHaveDifferentKeyWithAsNameParam() {
            SettingsParameters settings = new SettingsParameters();
            settings.TestStringAs = "Random String - " + (new Random()).Next(50000).ToString();
            Hashtable settingsTable = settings.GetSettings();

            Assert.IsTrue(settingsTable.ContainsKey("test-string-as-new-key"));
            Assert.AreEqual(settingsTable["test-string-as-new-key"], settings.TestStringAs);
        }

        [TestMethod]
        public void TestShouldNotHaveNonSerializableParam() {
            SettingsParameters settings = new SettingsParameters();
            Hashtable settingsTable = settings.GetSettings();

            Assert.IsFalse(settingsTable.ContainsKey("NotSerializableParameter"));
        }

        protected List<PropertyAttribute> GetStoreableProperties(BaseSettings settings) {

            PropertyInfo[] properties = settings.GetType().GetProperties();

            List<PropertyAttribute> allAttributes = new List<PropertyAttribute>();
            foreach (PropertyInfo prop in properties) {
                allAttributes.AddRange(
                  (from attribute in (StoreableSetting[])Attribute.GetCustomAttributes(prop, typeof(StoreableSetting))
                   select new PropertyAttribute() {
                       Property = prop,
                       SettingsAttribute = attribute,
                       PropertyName = attribute.AsName != null ? attribute.AsName : prop.Name
                   }).ToList()
               );
            }

            return allAttributes;

        }
    }
}
