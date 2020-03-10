using System;
using System.Collections.Generic;
using System.Text;
using Chilkat;
using SGRP.Aliexpress.CrawlService.Interfaces;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class RequestService : IRequestServices, IDisposable
    {
        private HttpResponse __Rep = null;
        private Http __Html = null;

        private bool disposed = false;
        private int listenPort = 0;



        public string RequestUrl(string ip = "", string port = "", string mail = "", string pass = "", string url = "", string cookies = "")
        {
            /*try
            {
                listenPort = Convert.ToInt32(__Port);
            }
            catch
            {
            }

            if (_Mail.Trim().Contains(" "))
            {
                return "False";
            }*/

            __Html = new Http();

            /*__Html.ProxyDomain = __Ip;
            __Html.ProxyPort = listenPort;

            __Html.SocksHostname = ip;
            __Html.SocksPort = int;
            __Html.SocksVersion = 5;*/

            __Html.ConnectTimeout = 120 * 1000;
            __Html.ReadTimeout = 150 * 1000;
            __Html.CookieDir = "memory";
            __Html.SendCookies = true;
            __Html.FollowRedirects = false;
            __Html.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.122 Safari/537.36";
            __Html.AddQuickHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            __Html.AddQuickHeader("accept-encoding", "gzip, deflate, br");
            __Html.AddQuickHeader("accept-language", "en-GB,en;q=0.9");
            __Html.AddQuickHeader("cache-control", "max-age=0");
            __Html.AddQuickHeader("cookie", cookies);
            __Html.AddQuickHeader("sec-fetch-dest", "document");
            __Html.AddQuickHeader("sec-fetch-mode", "navigate");
            __Html.AddQuickHeader("sec-fetch-site", "cross-site");
            __Html.AddQuickHeader("sec-fetch-user", "?1");
            __Html.AddQuickHeader("upgrade-insecure-requests", "1");
            __Html.AddQuickHeader("user-agent", __Html.UserAgent);
            __Rep = __Html.QuickGetObj(url);

            return __Rep.BodyStr;
        }

        ~RequestService()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
        }

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (__Html != null)
                    {
                        try
                        {
                            __Html.Dispose();
                        }
                        catch { }

                        __Html = null;
                    }
                    if (__Rep != null)
                    {
                        try
                        {
                            __Rep.Dispose();
                        }
                        catch { }

                        __Rep = null;
                    }
                }

                disposed = true;
            }
        }
    }
}
