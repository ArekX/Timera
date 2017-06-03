using Provider.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Provider.Base.Storeable;

namespace Provider.Github
{
    public class Provider : BaseProvider
    {
        public ProviderSettings Settings { get { return (ProviderSettings)settingsFile.Settings; } }

        protected SettingsFile settingsFile;

        public Provider() {
            settingsFile = new SettingsFile();
            settingsFile.Settings = new ProviderSettings();
        }

        public override string Name {
            get { return "Github"; }
        }

        public override void ExecuteAction(BaseAction action) {
            throw new NotImplementedException();
        }

        public override void ExecuteActionFlow(BaseActionFlow flow) {
            throw new NotImplementedException();
        }

        public override List<BaseAction> GetActions() {
            throw new NotImplementedException();
        }

        public override void Activate() {
            TokenForm form = new TokenForm(this);

            form.Tag = this;

            form.ShowDialog();
            Debug.WriteLine("TEST!");
        }

        public override void Deactivate() {
            throw new NotImplementedException();
        }

        protected override SettingsFile GetSettingsFile(string fileName, string encryptionKey) {
            settingsFile.FileName = fileName;
            settingsFile.EncryptionKey = encryptionKey;

            return settingsFile;
        }
    }
}
