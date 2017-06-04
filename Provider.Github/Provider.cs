using Provider.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Provider.Base.Storeable;
using System.Windows.Controls;

namespace Provider.Github
{
    public class Provider : BaseProvider
    {
        public ProviderSettings Settings { get { return (ProviderSettings)settingsFile.Settings; } }

        protected SettingsFile settingsFile;

        public Provider() {
            settingsFile = new SettingsFile() {
                Settings = new ProviderSettings()
            };
        }

        public override string Name {
            get { return "Github"; }
        }

        public override void ExecuteAction(BaseAction action) {
            throw new NotImplementedException();
        }

        public override List<BaseAction> GetActions() {
            throw new NotImplementedException();
        }

        public override void Activate() {
            Debug.WriteLine("Github Activated!");
        }

        public override void Deactivate() {
            Debug.WriteLine("Github Deactivated!");
        }

        protected override SettingsFile GetSettingsFile(string fileName, string encryptionKey) {
            settingsFile.FileName = fileName;
            settingsFile.EncryptionKey = encryptionKey;

            return settingsFile;
        }

        public override UserControl GetSettingsControl() {
            return new Settings(this);
        }
    }
}
