using System;
using System.Collections.Generic;
using System.Text;
using SGRP.Aliexpress.Bussiness.ViewModel;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public static class DataResolverContext
    {
        public static DataResolverService Init(List<CategoryViewModel> rawData)
        {
            return new DataResolverService(rawData);
        }
    }
}
