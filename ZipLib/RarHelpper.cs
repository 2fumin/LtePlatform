using System;
using System.IO;
using ZipLib.Gzip;

namespace ZipLib
{
    public class RarHelpper
    {
        public bool GZipFile(string sourcefilename, string zipfilename)
        {
            bool flag;
            FileStream stream = File.OpenRead(sourcefilename);
            GZipOutputStream stream2 = new GZipOutputStream(File.Open(zipfilename.Replace(".txt", ".gz"), FileMode.Create));
            try
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                stream2.Write(buffer, 0, buffer.Length);
                flag = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                flag = false;
            }
            stream.Close();
            stream2.Close();
            return flag;
        }

        public bool GZipFileTest(string sourcefilename, string zipfilename)
        {
            bool flag;
            FileStream stream = File.OpenRead(sourcefilename);
            GZipOutputStream stream2 = new GZipOutputStream(File.Open(zipfilename.Replace(".txt", ".gz"), FileMode.Create));
            try
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                stream2.Write(buffer, 0, buffer.Length);
                flag = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                flag = false;
            }
            stream.Close();
            stream2.Close();
            return flag;
        }

        public bool UnGzipFile(string zipfilename, string unzipfilename)
        {
            bool flag;
            GZipInputStream stream = new GZipInputStream(File.OpenRead(zipfilename));
            FileStream stream2 = File.Open(unzipfilename, FileMode.Create);
            try
            {
                int count = 0x800;
                byte[] buffer = new byte[count];
                while (count > 0)
                {
                    count = stream.Read(buffer, 0, count);
                    stream2.Write(buffer, 0, count);
                }
                flag = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                flag = false;
            }
            stream2.Close();
            stream.Close();
            return flag;
        }
    }
}
