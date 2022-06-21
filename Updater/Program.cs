using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using HtmlAgilityPack;
using System.Xml.Linq;

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

            WebClient Client = new WebClient();
            Client.DownloadFile(downloadLink, path + "mmdb.gz");

            using (FileStream fInStream = new FileStream(path + "mmdb.gz", FileMode.Open, FileAccess.Read))
            {
                using (GZipStream zipStream = new GZipStream(fInStream, CompressionMode.Decompress))
                {
                    using (FileStream fOutStream = new FileStream(path + "ipdb.mmdb", FileMode.Create, FileAccess.Write))
                    {
                        byte[] tempBytes = new byte[4096];
                        int i;
                        while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            fOutStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }

            File.Delete(path + "mmdb.gz");
        }
    }
}
