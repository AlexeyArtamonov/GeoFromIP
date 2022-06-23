using System.IO;
using System.IO.Compression;
using System.Net;
using HtmlAgilityPack;

namespace Updater
{
    class Program
    {
        static void Main()
        {
            string path = File.ReadAllText("Config.cfg").Remove(0, 5);
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(@"https://db-ip.com/db/download/ip-to-city-lite");

            string downloadLink = null;

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string temp = link.GetAttributeValue("href", "-");
                if (temp.EndsWith(".mmdb.gz"))
                {
                    downloadLink = temp;
                    break;
                }
            }

            if (downloadLink == null)
                return;

            using (WebClient Client = new WebClient())
                Client.DownloadFile(downloadLink, path + "mmdb.gz");

            try
            {
                Decompress(path);
            }
            finally
            {
                if (File.Exists(path + "mmdb.gz"))
                    File.Delete(path + "mmdb.gz");
            }
        }
        public static void Decompress(string path)
        {
            using (FileStream FInStream = new FileStream(path + "mmdb.gz", FileMode.Open, FileAccess.Read))
            {
                using GZipStream zipStream = new GZipStream(FInStream, CompressionMode.Decompress);
                using FileStream FOutStream = new FileStream(path + "ipdb.mmdb", FileMode.Create, FileAccess.Write);

                byte[] temp = new byte[4096];
                int i;

                while ((i = zipStream.Read(temp, 0, temp.Length)) != 0)
                {
                    FOutStream.Write(temp, 0, i);
                }
            }
        }
    }
}

