using Provider.Base.Storeable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Base.Test.Mocks
{
    public class MockSettingsFile : SettingsFile
    {
        public Stream SaveStream { get; set; }
        public Stream LoadStream { get; set; }

        public MockSettingsFile() {
            ResetStreams();

            EncryptionKey = "TEST KEY";
            FileName = "dummy.file";
        }


        public void ResetStreams() {
            ResetLoadStream();
            ResetSaveStream();
        }

        public void ResetLoadStream() {
            if (LoadStream != null && LoadStream.CanRead) {
                LoadStream.Close();
            }
            LoadStream = new MemoryStream();
        }

        public void ResetLoadStreamWithHeader(bool resetSeek = true) {
            ResetLoadStream();

            WriteHeader(LoadStream);

            if (resetSeek) {
                LoadStream.Seek(0, SeekOrigin.Begin);
            }
        }

        public void ResetSaveStream() {
            if (SaveStream != null && SaveStream.CanRead) {
                SaveStream.Close();
            }
            SaveStream = new MemoryStream();
        }

        protected override Stream GetFileSaveStream() {
            return SaveStream;
        }

        protected override Stream GetFileLoadStream() {
            return LoadStream;
        }

        protected override void CloseFileStream(Stream stream) {
            stream.Seek(0, SeekOrigin.Begin);
        }
    }
}
