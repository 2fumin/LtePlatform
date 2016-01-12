namespace ZipLib.Comppression
{
    public class DeflaterPending : PendingBuffer
    {
        public DeflaterPending()
            : base(0x10000)
        {
        }
    }
}

