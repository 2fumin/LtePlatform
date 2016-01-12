using System;

namespace ZipLib.Zip
{
    public class KeysRequiredEventArgs : EventArgs
    {
        private readonly string _fileName;
        private byte[] _key;

        public KeysRequiredEventArgs(string name)
        {
            _fileName = name;
        }

        public KeysRequiredEventArgs(string name, byte[] keyValue)
        {
            _fileName = name;
            _key = keyValue;
        }

        public string FileName => _fileName;

        public byte[] Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
    }
}
