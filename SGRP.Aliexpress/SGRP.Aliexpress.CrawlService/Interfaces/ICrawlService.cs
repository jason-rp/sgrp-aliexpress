using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;

namespace SGRP.Aliexpress.CrawlService.Interfaces
{
    public interface ICrawlService
    {
        Task GetData(List<int> browsers,int browserNumber,List<InputUrlModel> urls);
    }
}
