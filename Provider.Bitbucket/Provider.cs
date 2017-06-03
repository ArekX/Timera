using Provider.Base;
using System;
using System.Collections.Generic;
using System.Collections;
using Provider.Base.Storeable;
using System.Reflection;

namespace Provider.Bitbucket
{
    public class Provider : BaseProvider {

        public ProviderSettings Settings { get { return (ProviderSettings)settingsFile.Settings; } }

        protected SettingsFile settingsFile;

        public Provider() {
            settingsFile = new SettingsFile();
            settingsFile.Settings = new ProviderSettings();
        }

        public override string Name {
            get { return "Bitbucket"; }
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
