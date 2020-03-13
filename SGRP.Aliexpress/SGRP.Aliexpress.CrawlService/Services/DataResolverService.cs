using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.Data;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class DataResolverService
    {
        private List<CategoryViewModel> _viewModel;
        public DataResolverService(List<CategoryViewModel> rawData)
        {
            _viewModel = rawData;
        }

        public void ResolveData()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            {
                //var releases = context.Users.Take(5);
                foreach (var viewModel in _viewModel)
                {
                    context.Products.Add(new Product
                    {
                        ProductId = viewModel.ProductId,
                        ProductName = viewModel.ProductName
                    });
                }

                context.SaveChanges();
            }
        }

    }
}
