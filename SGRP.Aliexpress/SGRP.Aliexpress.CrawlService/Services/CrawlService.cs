using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.CrawlService.Interfaces;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class CrawlService : ICrawlService
    {
        private static readonly Random Random = new Random();
        

        public List<CategoryViewModel> GetData(List<InputUrlModel> urls)
        {
            var pid = 1;
            var result = new List<CategoryViewModel>();

            foreach (var url in urls)
            {

                var executeNodeResult = Node("nodescript.js",
                    "\"" + -101 + "\"" + " \"" + GetLoginUrl(url) + "\"" + " \"" + url.Id + "\"" + " \"" + url.IsCategory + "\""  + " \"" +
                    GetRandomMailPass(Random) + "\"", ref pid);

                if (executeNodeResult.Count == 1)
                {
                    var rawData = JsonConvert.DeserializeObject<List<CategoryViewModel>>(executeNodeResult[0]);
                    new DataResolverService(rawData).ResolveData(url);
                    
                }
            }


            return result;
        }


        public string GetLoginUrl(InputUrlModel model)
        {
            string result;
            if (model.IsCategory)
            {
                using (var client = new HttpClient())
                {
                    using (var response = client.GetAsync(model.FormattedUrl).Result)
                    {
                        using (var content = response.Content)
                        {
                            result = content.ReadAsStringAsync().Result;
                            var document = new HtmlDocument();
                            document.LoadHtml(result);
                            var collection = document.DocumentNode.SelectNodes("//script");
                            if (collection.Count != 1) return result;
                            var loginUrl = new Regex("location.href=\"(.*?)\"\\)").Match(collection[0].InnerHtml)
                                .Groups[1].Value.Trim() + "\")";
                            result = FilterUrl(loginUrl);
                        }
                    }
                }
            }
            else
            {
                result = $"https://login.aliexpress.com/?from=sm&return_url=https://www.aliexpress.com/store/all-wholesale-products/{model.Id}.html?scene=allproducts";
            }


            return result;
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
            int timeout = 0)
        {
            var isDebug = false;

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
                        process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
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

                    if (timeout > 0)
                    {
                        process.WaitForExit(timeout);
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
            var result = "intgrupic";
            for (var i = 0; i < 6; i++)
            {
                result += GetRandomCharacter(text, rng);
            }
            result += rng.Next(0, 9) + rng.Next(1, 9) + GetRandomCharacter(text, rng);
            result += "@gmail.com|" + "sgg120475104" + GetRandomCharacter(text, rng) + GetRandomCharacter(text, rng);

            return result;
        }
    }
}