using Provider.Base.Helpers;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Provider.Base.Storeable
{
    public delegate void OnDataPersistDelegate();

    public class SettingsFile {
        
        protected RijndaelManaged encryption;
        protected BaseSettings settings;

        public BaseSettings Settings {
            get {
                return settings;
            }

            set {
                settings = value;

                if (value != null) {
                    SerializationBinder = value.GetSerializationBinder();
                }
            }
        }

        public ContainerSerializationBinder SerializationBinder { get; set; }

        public string FileName { get; set; }

        public event OnDataPersistDelegate OnDataLoaded;

        public event OnDataPersistDelegate OnDataSaved;

        public string EncryptionKey {
            get {
                return encryption != null ? Encoding.UTF8.GetString(encryption.Key) : null;
            }
            set {
                InitializeEncryption(value);
            }
        }

        protected virtual void InitializeEncryption(string encryptionKey) {
            encryption = new RijndaelManaged() {
                Key = HashHelper.GetSHA256Bytes(encryptionKey),
                Padding = PaddingMode.Zeros
            };
        }

        public virtual void Save() {

            if (Settings == null) {
                throw new Exception("Settings object is not set!");
            }

            if (FileName == null) {
                throw new Exception("Filename must be set.");
            }

            Stream stream = GetFileSaveStream();
            BinaryFormatter bformatter = GetBinaryFormatter();

            encryption.GenerateIV();

            WriteHeader(stream);

            MemoryStream memStream = new MemoryStream();

            using (CryptoStream cryptoStream = GetEncryptionStream(memStream)) {
                bformatter.Serialize(cryptoStream, Settings);

                cryptoStream.FlushFinalBlock();


                memStream.Position = 0;
                memStream.CopyTo(stream);

                cryptoStream.Close();
            }

            memStream.Close();
            CloseFileStream(stream);

            if (OnDataSaved != null) {
                OnDataSaved.Invoke();
            }
        }

        protected virtual void CloseFileStream(Stream stream) {
            stream.Close();
        }

        protected virtual Stream GetFileSaveStream() {
            return File.Open(FileName, FileMode.Create);
        }

        protected virtual CryptoStream GetEncryptionStream(MemoryStream memStream) {
            return new CryptoStream(memStream, encryption.CreateEncryptor(), CryptoStreamMode.Write);
        }

        protected virtual void WriteHeader(Stream stream) {
            stream.Write(Encoding.UTF8.GetBytes("TMRCFG"), 0, 6);
            byte[] lengthBytes = BitConverter.GetBytes(encryption.IV.Length);
            stream.Write(lengthBytes, 0, lengthBytes.Length);
            stream.Write(encryption.IV, 0, encryption.IV.Length);
        }

        protected virtual BinaryFormatter GetBinaryFormatter() {
            BinaryFormatter bformatter = new BinaryFormatter();

            if (SerializationBinder != null) {
                bformatter.Binder = SerializationBinder;
            }

            return bformatter;
        }

        public virtual void Load() {
            Settings = null;

            Stream stream = GetFileLoadStream();

            if (stream == null || !HasValidHeader(stream)) {
                return;
            }

            encryption.IV = GetIVFromStream(stream);

            MemoryStream memStream = ReadEncryptedPayload(stream);

            try {
                using (CryptoStream decryptionStream = GetDecryptionStream(memStream)) {
                    BinaryFormatter bformatter = GetBinaryFormatter();
                    Settings = (BaseSettings)bformatter.Deserialize(decryptionStream);
                    decryptionStream.Close();
                }
            } catch {
                return;
            }

            memStream.Close();
            CloseFileStream(stream);

            if (OnDataLoaded != null) {
                OnDataLoaded.Invoke();
            }
        }

        protected virtual Stream GetFileLoadStream() {
            if (!File.Exists(FileName)) {
                return null;
            }

            return File.Open(FileName, FileMode.Open);
        }

        protected virtual CryptoStream GetDecryptionStream(Stream stream) {
            return new CryptoStream(stream, encryption.CreateDecryptor(), CryptoStreamMode.Read);
        }

        protected virtual MemoryStream ReadEncryptedPayload(Stream stream) {
            MemoryStream memStream = new MemoryStream();

            byte[] buffer = new byte[150000];
            while (stream.Position != stream.Length) {
                int readBytes = stream.Read(buffer, 0, buffer.Length);
                memStream.Write(buffer, 0, readBytes);
            }

            memStream.Position = 0;
            return memStream;
        }

        protected virtual byte[] GetIVFromStream(Stream stream) {
            stream.Position = 6;
            byte[] ivSize = new byte[sizeof(Int32)];
            stream.Read(ivSize, 0, ivSize.Length);
            byte[] iv = new byte[BitConverter.ToInt32(ivSize, 0)];
            stream.Read(iv, 0, iv.Length);
            return iv;
        }

        protected virtual bool HasValidHeader(Stream stream) {
            stream.Position = 0;
            byte[] magicNumber = new byte[6];
            stream.Read(magicNumber, 0, magicNumber.Length);

            return Encoding.UTF8.GetString(magicNumber) == "TMRCFG";
        }
    }
}
