using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IStorageProvider
{
    public interface IStorageProvider
    {
        Task<bool> ExistsAsync(string filename);
        Task<byte[]> ReadAsync(string filename);
        Task WriteAsync(string filename, byte[] value);
        Task CopyAsync(string filenameFrom, string filenameTo);
        Task MoveAsync(string filenameFrom, string filenameTo);
        Task DeleteAsync(string filename);
        //Task<IList<string>> ListAsync(string path = null);
    }
    public class FileStore : IStorageProvider
    {
        public Task<bool> ExistsAsync(string filename)
        {
            return Task.Run(() =>
            {
                return File.Exists(filename);
            });
        }

        public Task<byte[]> ReadAsync(string filename)
        {
            return Task.Run(() =>
            {
                byte[] bytes;
                try
                {
                    using (FileStream fsSource = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fsSource.Length];
                        int numBytesToRead = (int)fsSource.Length;
                        int numBytesRead = 0;

                        while (numBytesToRead > 0)
                        {
                            int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                            if (n == 0)
                                break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                    }
                    return bytes;
                }
                catch (FileNotFoundException ioEx)
                {
                    Console.WriteLine(ioEx.Message);
                    bytes = null;
                    return bytes;
                }
            }
            );
        }

        public Task WriteAsync(string filename, byte[] value)
        {
            return Task.Run(() =>
            {
                int numBytesToRead = (int)value.Length;

                using (FileStream fsNew = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fsNew.Write(value, 0, numBytesToRead);
                }
            });
        }

        public Task CopyAsync(string filenameFrom, string filenameTo)
        {
            return Task.Run(() =>
            {
                byte[] bytes;
                try
                {
                    using (FileStream fsSource = new FileStream(filenameFrom, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fsSource.Length];
                        int numBytesToRead = (int)fsSource.Length;
                        int numBytesRead = 0;

                        while (numBytesToRead > 0)
                        {
                            int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                            if (n == 0)
                                break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        numBytesToRead = bytes.Length;
                        using (FileStream fsNew = new FileStream(filenameTo, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fsNew.Write(bytes, 0, numBytesToRead);
                        }
                    }
                }
                catch (FileNotFoundException ioEx)
                {
                    Console.WriteLine(ioEx.Message);
                }
            });
        }

        public Task MoveAsync(string filenameFrom, string filenameTo)
        {
            return Task.Run(() =>
            {
                byte[] bytes;
                try
                {
                    using (FileStream fsSource = new FileStream(filenameFrom, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fsSource.Length];
                        int numBytesToRead = (int)fsSource.Length;
                        int numBytesRead = 0;

                        while (numBytesToRead > 0)
                        {
                            int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                            if (n == 0)
                                break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        File.Delete(filenameFrom);
                        numBytesToRead = bytes.Length;
                        using (FileStream fsNew = new FileStream(filenameTo, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fsNew.Write(bytes, 0, numBytesToRead);
                        }
                    }
                }
                catch (FileNotFoundException ioEx)
                {
                    Console.WriteLine(ioEx.Message);
                }
            });
        }

        public Task DeleteAsync(string filename)
        {
            return Task.Run(() =>
            {
                try
                {
                    File.Delete(filename);
                }
                catch (FileNotFoundException ioEx)
                {
                    Console.WriteLine(ioEx);
                }
            }
            );
        }

    }
}
