using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.CrawlService.Interfaces;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class CrawlService : ICrawlService
    {
        private static readonly Random Random = new Random();
        private readonly string cookie = string.Empty;

        public CrawlService()
        {
            //ChilkatService.ActiveChilKat();

        }

        public List<UserInfoViewModel> GetData(List<string> urls)
        {

            GetLoginUrlFromCategory(urls[0]);
            var pid = 1;
            var result = new List<UserInfoViewModel>();
            var htmlDocument = new HtmlDocument();
            var cookies = Node("nodescript.js", "\"" + -109 + "\"" + " \"" + urls[0] + "\"" + " \"" + GetRandomMailPass(Random) + "\"", ref pid);
            //foreach (var url in urls)
            //{
            //    var rawData = _requestServices.RequestUrl(url: url, cookies: cookie);
            //    var collection = htmlDocument.DocumentNode.SelectNodes("//script");
            //    if (collection.Count == 1)
            //    {
            //        var loginUrl = new Regex("location.href=\"(.*?)\"\\)").Match(collection[0].InnerHtml)
            //            .Groups[1].Value.Trim() + "\")";
            //        loginUrl = FilterUrl(loginUrl);
            //        var cookies = Node("index.js", "\"" + -109 + "\"" + " \"" + loginUrl + "\"" + " \"" + GetRandomMailPass(Random) + "\"", ref pid);
            //        if (cookies.Count == 1)
            //        {
            //            var json = JsonConvert.DeserializeObject(cookies[0]);

            //        }
            //    }
            //}



            return result;
        }

        public  void GetLoginUrlFromCategory(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(result);
                        var nodes = document.DocumentNode.SelectNodes("Your nodes");
                        //Some work with page....
                    }
                }
            }
        }

        private UserInfoViewModel GetDetailData(string detailUrl)
        {
            try
            {
               
            }
            catch (Exception e)
            {
                throw;
            }


            return null;
        }

        private static string FilterUrl(string url)
        {
            if (url.Contains("location.href="))
            {
                url = new Regex("location.href=\"(.*)\".*?$").Match(url).Groups[1].Value.Trim();
            }

            if (url.Contains("encodeURIComponent"))
            {
                var match = new Regex("(.*?)\".*?encodeURIComponent.*?\"(.*)$").Match(url);
                url = match.Groups[1].Value.Trim() + "" + Uri.EscapeDataString(match.Groups[2].Value.Trim());
            }

            return url;
        }

        public static List<string> Node(string fileName, string command, ref int pId, string dir = "Node",
    int Ms = 0)
        {
            var isDebug = true;
            var ms = 60000;
            var result = new List<string>();
            try
            {
                using (var process = new Process())
                {
                    ProcessStartInfo __Proc_Start_Info = new ProcessStartInfo();

                    __Proc_Start_Info.FileName = "node.exe";
                    __Proc_Start_Info.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dir);
                    __Proc_Start_Info.Arguments =
                        (command.Trim().Length > 0) ? fileName + " " + command : fileName;
                    if (!isDebug)
                    {
                        __Proc_Start_Info.RedirectStandardOutput = true;
                        __Proc_Start_Info.UseShellExecute = false;
                        __Proc_Start_Info.CreateNoWindow = true;
                    }

                    __Proc_Start_Info.Verb = "runas";

                    process.StartInfo = __Proc_Start_Info;
                    if (!isDebug)
                    {
                        process.OutputDataReceived += delegate (object sender, DataReceivedEventArgs args)
                        {
                            lock (result)
                            {
                                if (args.Data != null)
                                {
                                    result.Add(args.Data);
                                }
                            }
                        };
                    }
                    process.Start();
                    if (!isDebug)
                    {
                        process.BeginOutputReadLine();
                    }

                    pId = process.Id;

                    if (ms > 0)
                    {
                        process.WaitForExit(ms);
                    }
                    else
                    {
                        process.WaitForExit();
                    }

                    __Proc_Start_Info = null;
                }
            }
            catch
            {

            }

            return result;
        }

        private static char GetRandomCharacter(string text, Random rng)
        {
            var index = rng.Next(text.Length);
            return text[index];
        }

        private static string GetRandomMailPass(Random rng)
        {
            var text = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            var result = "ssaintgrupic";
            for (var i = 0; i < 6; i++)
            {
                result += GetRandomCharacter(text, rng);
            }
            result += rng.Next(0, 9) + rng.Next(1, 9) + GetRandomCharacter(text, rng);
            result += "@gmail.com|" + "saint120475104" + GetRandomCharacter(text, rng) + GetRandomCharacter(text, rng);

            return result;
        }
    }
}
