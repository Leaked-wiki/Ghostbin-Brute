using System;
using System.Linq;
using System.Net;
using System.Threading;
using Leaf.xNet;
using System.IO;
using Colorful;
using System.Drawing;

namespace Throwbin_Brute
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string credit1 = "Created by c.to/Sango | Sango#0123";
            string credit2 = " | and by: c.to/amboss | amboss#2199";
            try
            {
                credit1 = client.DownloadString($"https://leaked.wiki/info.txt");
                credit2 = client.DownloadString($"https://pastehub.net/info2.txt");
            }
            catch (Exception) { }
            string credit = credit1 + credit2;

            Colorful.Console.Title = $"Ghostbin.co Bruteforcer | {credit}";
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            int good = 0; int bad = 0; int err = 0; int cpm = 0; int cpm_aux = 0;
            showHeader();
            Colorful.Console.Write("Threads: ", Color.OrangeRed);
            int threadNum = int.Parse(Colorful.Console.ReadLine());
            showHeader();
            void update() { 
                while (true) {
                    cpm = (cpm + (cpm_aux * 60)) / 2;
                    cpm_aux = 0;
                    Colorful.Console.Title = $"Ghostbin.co Bruteforcer | Good: {String.Format("{0:n0}", good)} | Bad: {String.Format("{0:n0}", bad)} | Errors: {String.Format("{0:n0}", err)} | CPM: {String.Format("{0:n0}", cpm)} | {credit}"; 
                    Thread.Sleep(1000); 
                } 
            }
            new Thread(update).Start();
            void gen()
            {
                HttpRequest req = new HttpRequest();
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36";
                req.KeepAlive = true;
                req.IgnoreProtocolErrors = true;
                req.ConnectTimeout = 5000;
                req.Cookies = null;
                req.UseCookies = true;
                
                while (true)
                {
                    string code = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
                    try
                    {
                        var res1 = req.Get("https://ghostbin.co/paste/" + code + "/raw", null);
                        if (res1.StatusCode == Leaf.xNet.HttpStatusCode.OK)
                        {
                            Colorful.Console.WriteLine("[GOOD] " + $"https://ghostbin.co/paste/{code}", Color.Lime);
                            good++;
                            cpm_aux++;
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("hits.txt", true)) { file.WriteLine($"https://ghostbin.co/paste/{code}"); }
                        }
                        else if (res1.StatusCode == Leaf.xNet.HttpStatusCode.NotFound) { bad++; cpm_aux++; }
                    }
                    catch (Exception e) { err++; }
                }
            }
            for (int x = 0; x < threadNum; x++) { new Thread(gen).Start(); }
            string Parse(string source, string left, string right)
            {
                return source.Split(new string[1] { left }, StringSplitOptions.None)[1].Split(new string[1]
                {
                right
                }, StringSplitOptions.None)[0];
            }

            void showHeader()
            {
                Colorful.Console.Clear();
                Colorful.Console.WriteAscii("Ghostbin Brute", Color.DarkRed);
                Colorful.Console.Write("Powered by ", Color.DarkRed);
                Colorful.Console.Write("Leaked.wiki | c.to/Sango", Color.IndianRed);
                Colorful.Console.Write(" & ", Color.DarkRed);
                Colorful.Console.Write("pastehub.net | c.to/amboss\n", Color.IndianRed);
                Colorful.Console.WriteLine();
            }
        }
    }
}