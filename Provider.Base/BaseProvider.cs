using Provider.Base.Storeable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Provider.Base
{
    public abstract class BaseProvider
    {
        protected string encryptionKey;

        public virtual string Name {
            get { return "Unknown Provider"; }
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract List<BaseAction> GetActions();

        public abstract void ExecuteAction(BaseAction action);

        public abstract void ExecuteActionFlow(BaseActionFlow flow);

        protected abstract SettingsFile GetSettingsFile(string fileName, string encryptionKey);

        public virtual void SetEncryptionKey(string key) {
            encryptionKey = key;
        }

        public virtual void SaveSettings(string fileName) {

            if (string.IsNullOrWhiteSpace(encryptionKey)) {
                throw new Exception("Encryption key must set.");
            }

            GetSettingsFile(fileName, encryptionKey).Save();
        }

        public virtual void LoadSettings(string fileName) {
            if (string.IsNullOrWhiteSpace(encryptionKey)) {
                throw new Exception("Encryption key must set.");
            }

            GetSettingsFile(fileName, encryptionKey).Load();
        }
    }
}
