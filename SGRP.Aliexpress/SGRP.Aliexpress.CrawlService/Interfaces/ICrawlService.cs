using System;
using System.Collections.Generic;
using System.Text;
using SGRP.Aliexpress.Bussiness.ViewModel;

namespace SGRP.Aliexpress.CrawlService.Interfaces
{
    public interface ICrawlService
    {
        List<UserInfoViewModel> GetData(List<string> urls);
    }
}
