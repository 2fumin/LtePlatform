using System;

namespace Lte.Domain.ZipLib.Zip
{
    public class KeysRequiredEventArgs : EventArgs
    {
        private string fileName;
        private byte[] key;

        public KeysRequiredEventArgs(string name)
        {
            fileName = name;
        }

        public KeysRequiredEventArgs(string name, byte[] keyValue)
        {
            fileName = name;
            key = keyValue;
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public byte[] Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
    }
}
