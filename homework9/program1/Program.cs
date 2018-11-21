using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace program1
{
    class Crawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;

        static void Main(string[] args)
        {
            Crawler myCrawler = new Crawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            string startUrl2 = "http://www.cnblogs.com/dstang2000/archive/";
            //if (args.Length >= 1) startUrl = args[0];

            myCrawler.urls.Add(startUrl, false);    //加入初始页面
            myCrawler.urls.Add(startUrl2, false);

            Parallel.Invoke(new Action[] {
                ()=>myCrawler.Crawl(),
                ()=>myCrawler.Crawl()
            });
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了...");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)  //找到一个还没有下载过的链接
                {
                    if ((bool)urls[url]) continue;//已经下载过的不再下载
                    current = url;
                    urls[current] = true;
                    break;
                }

                if (current == null || count > 10) break;
                
                Console.WriteLine("爬行" + current + "页面");
                
                string html = DownLoad(current);    //下载
                                                  
                Parse(html);    //解析并加入新的链接
            }
            Console.WriteLine("爬行结束");
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = (count++).ToString() + ".html";
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Parse(string html)
        {
            try
            {
                string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
                MatchCollection matches = new Regex(strRef).Matches(html);
                foreach (Match match in matches)
                {
                    strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\"', '#', ' ', '>');
                    if (strRef.Length == 0) continue;
                    if (urls[strRef] == null) urls[strRef] = false;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}

