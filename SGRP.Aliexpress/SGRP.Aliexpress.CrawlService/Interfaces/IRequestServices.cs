using System;
using System.Collections.Generic;
using System.Text;

namespace SGRP.Aliexpress.CrawlService.Interfaces
{
    public interface IRequestServices
    {
        string RequestUrl(string ip = "", string port = "", string mail = "", string pass = "", string url = "",
            string cookies = "");
    }
}
