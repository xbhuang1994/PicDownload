using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Security.Cryptography;
namespace PicDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://xz5.mm667.com/tnl53/images/0{0}.jpg";
            string folder = @"D:\MM";
            Console.Write("input url:   like http://xz5.mm667.com/tnl57/");
            url = Console.ReadLine() + @"/images/0{0}.jpg";
            DownLoadToFileList(url, folder);
            Console.ReadLine();
        }

        static void DownLoadToFileList(string Url, string FolderPath)
        {
            string md5_str = GetMd5Hash(Url);
            Console.WriteLine("file Name:" + md5_str);
            for (int i = 1; i < 100; i++)
            {
                string a;
                if (i < 10)
                    a = "0" + i;
                else
                    a = i.ToString();
                string url = string.Format(Url, a);
                string file = string.Format(FolderPath + "/{0}_{1}.jpg", md5_str, i);
                bool bl = DownLoadToFile(url, file);
                if (!bl)
                    break;
            }

        }
        static bool DownLoadToFile(string Url, string Path)
        {
            if (File.Exists(Path))
            {
                Console.WriteLine("File already exists!");
                return true;
            }
            try
            {
                Console.WriteLine("Download Start");
                WebRequest HWR = WebRequest.Create(Url);
                WebResponse HWRr = HWR.GetResponse();
                Stream SR = HWRr.GetResponseStream();
                FileStream S = File.Open(Path, FileMode.Create);
                byte[] by = new byte[1024];
                int o = 1;
                while (o > 0)
                {
                    o = SR.Read(by, 0, 1024);
                    S.Write(by, 0, o);
                    string progressStr = "\rDownloading...  " + ((float)S.Position / HWRr.ContentLength).ToString("p0");
                    Console.Write(progressStr);
                }
                Console.WriteLine("\nDownload Complete");
                S.Close();
                SR.Close();
                S.Dispose();
                SR.Dispose();
                HWR = null;
                HWRr = null;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
