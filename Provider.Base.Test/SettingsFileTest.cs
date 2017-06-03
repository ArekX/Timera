using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Provider.Base.Test.Mocks;
using Provider.Base.Test.Fixtures;
using System.IO;
using Provider.Base.Storeable;
using System.Runtime.Serialization;
using System.Text;

namespace Provider.Base.Test
{
    [TestClass]
    public class SettingsFileTest
    {
        [TestMethod]
        public void TestSettingsFileSave() {
            MockSettingsFile settingsFile = GetSettingsMock();
            Assert.IsTrue(settingsFile.SaveStream.Length > 0);
        }

        [TestMethod]
        public void TestSettingsFileLoad() {
            MockSettingsFile settingsFile = GetSettingsMock();
            DefaultStoreableSettings saveSettings = (DefaultStoreableSettings)settingsFile.Settings;

            MockSettingsFile loadSettingsFile = new MockSettingsFile();
            loadSettingsFile.SerializationBinder = saveSettings.GetSerializationBinder();
            loadSettingsFile.LoadStream = settingsFile.SaveStream;
            loadSettingsFile.Load();

            DefaultStoreableSettings loadSettings = (DefaultStoreableSettings)loadSettingsFile.Settings;

            Assert.IsNotNull(loadSettings);
            Assert.IsNotNull(saveSettings);
            Assert.IsTrue(saveSettings.TestRandomInteger == loadSettings.TestRandomInteger);
            Assert.IsTrue(saveSettings.TestString == loadSettings.TestString);
            Assert.IsTrue(saveSettings.TestStringAs == loadSettings.TestStringAs);
        }

        [TestMethod]
        public void TestNotExistingFileLoad() {
            SettingsFile file = new SettingsFile();
            file.FileName = null;
            file.Load();

            Assert.IsNull(file.Settings);

            file.FileName = "dummy.file";
            file.Load();

            Assert.IsNull(file.Settings);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Settings object is not set!")]
        public void TestSaveWithNoSettings() {
            SettingsFile file = new SettingsFile();
            file.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Filename must be set!")]
        public void TestSaveWithNoFileName() {
            SettingsFile file = new SettingsFile();
            file.Settings = new DefaultStoreableSettings();
            file.Save();
        }

        [TestMethod]
        public void TestLoadWithInvalidData() {
            MockSettingsFile settingsFile = GetMockSettingsObject();
            settingsFile.ResetLoadStream();
            settingsFile.Load();

            Assert.IsNull(settingsFile.Settings);
        }

        [TestMethod]
        public void TestLoadWithValidHeaderButInvalidEncryption() {
            MockSettingsFile settingsFile = GetMockSettingsObject();
            settingsFile.ResetLoadStreamWithHeader(false);
            byte[] invalidData = Encoding.UTF8.GetBytes("BAD DATA");
            settingsFile.LoadStream.Write(invalidData, 0, invalidData.Length);
            settingsFile.LoadStream.Seek(0, SeekOrigin.Begin);

            settingsFile.Load();

            Assert.IsNull(settingsFile.Settings);
        }

        [TestMethod]
        public void TestIsValidHeaderWritten() {
            MockSettingsFile settingsFile = GetSettingsMock();

            byte[] header = new byte[6];
           
            settingsFile.SaveStream.Read(header, 0, 6);

            Assert.AreEqual(Encoding.UTF8.GetString(header), "TMRCFG");
        }

        [TestMethod]
        public void TestAreEventsCalled() {
            MockSettingsFile settingsFile = GetSettingsMock();

            settingsFile.LoadStream = settingsFile.SaveStream;

            bool isSaved = false;
            bool isLoaded  = false;

            settingsFile.OnDataSaved += () => {
                isSaved = true;
            };


            settingsFile.OnDataLoaded += () => {
                isLoaded = true;
            };

            settingsFile.Save();
            settingsFile.Load();

            Assert.IsTrue(isSaved);
            Assert.IsTrue(isLoaded);
        }

        protected MockSettingsFile GetSettingsMock() {
            MockSettingsFile settingsFile = GetMockSettingsObject();
            DefaultStoreableSettings settings = (DefaultStoreableSettings)settingsFile.Settings;

            settings.TestString = "This is a test string.";
            settings.TestStringAs = "This is a test string stored as different key.";

            Random r = new Random();
            settings.TestRandomInteger = r.Next();
            
            settingsFile.Save();

            return settingsFile;
        }

        protected MockSettingsFile GetMockSettingsObject() {
            MockSettingsFile settingsFile = new MockSettingsFile();
            DefaultStoreableSettings settings = new DefaultStoreableSettings();

            settingsFile.Settings = settings;

            return settingsFile;
        }
    }
}
