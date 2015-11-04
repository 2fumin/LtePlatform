using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public class BitMaskStream
    {
        private readonly Queue<bool> _list;

        public BitMaskStream(BitArrayInputStream input, int count)
        {
            _list = new Queue<bool>(count);
            for (int i = 0; i < count; i++)
            {
                _list.Enqueue(input.readBit() != 0);
            }
        }

        public bool Read()
        {
            return _list.Dequeue();
        }
    }
}
