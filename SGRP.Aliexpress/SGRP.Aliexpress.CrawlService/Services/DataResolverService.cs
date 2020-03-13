using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.Data;
using SGRP.Aliexpress.Data.Entities;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class DataResolverService
    {
        private readonly List<CategoryViewModel> _viewModel;
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
                    var ratingDesc = 0M;
                    var ratingComm = 0M;
                    var ratingShip = 0M;
                    var shippingCompany = "";
                    var shippingFee = 0M;
                    var shippping1 = "";
                    var shipping2 = "";
                    var shipping3 = "";

                    if (!string.IsNullOrEmpty(viewModel.StoreRatingMultiple))
                    {
                        var ratingDetail = JsonConvert.DeserializeObject<StoreRatingDetailModel>(viewModel.StoreRatingMultiple);
                        if (ratingDetail != null)
                        {
                            ratingDesc = ratingDetail.StoreRatingDesc.Score;
                            ratingComm = ratingDetail.StoreRatingSeller.Score;
                            ratingShip = ratingDetail.StoreRatingShipping.Score;
                        }
                    }

                    if (!string.IsNullOrEmpty(viewModel.ShippingContent))
                    {
                        var shippingData = JsonConvert.DeserializeObject<ShippingContentModel>(viewModel.ShippingContent);
                        if (shippingData != null)
                        {
                            var ePack = shippingData.ShippingBody.ShippingFreightResult.Where(n =>
                                n.Company == "ePacket").ToList();
                            if (ePack.Any())
                            {
                                shippingCompany = ePack.First().Company;
                                shippingFee = ePack.First().ShippingFreightAmount.Value;
                            }

                            var sorts = shippingData.ShippingBody.ShippingFreightResult
                                .Select(n => n.ShippingFreightAmount).OrderBy(n => n.Value).ToList();
                            var count = 1;

                            foreach (var sort in sorts)
                            {
                                if (count <= 3)
                                {
                                    var current = shippingData.ShippingBody.ShippingFreightResult.First(n => n.ShippingFreightAmount.Value == sort.Value);
                                    var shippingStr = current.Company + "|" + current.ShippingFreightAmount.Value;
                                    switch (count)
                                    {
                                        case 1:
                                            shippping1 = shippingStr;
                                            break;
                                        case 2:
                                            shipping2 = shippingStr;
                                            break;
                                        case 3:
                                            shipping3 = shippingStr;
                                            break;
                                    }
                                }

                                count += 1;
                            }

                        }
                    }

                    var product = new Product
                    {
                        ProductName = viewModel.ProductName,
                        ProductId = viewModel.ProductId,
                        ProductKeyId = viewModel.ProductId.ToString(),
                        IsParent = true,
                        Description = viewModel.Description,
                        ItemLot = viewModel.ItemLot,
                        BrandName = viewModel.BrandName,
                        StockNumber = viewModel.StockNumber,
                        CategoryId = viewModel.CategoryId,
                        CategoryName = viewModel.CategoryName,
                        StoreId = viewModel.StoreId,
                        StoreName = viewModel.StoreName,
                        StoreYear = viewModel.StoreYear,
                        StoreRatingPercent = viewModel.StoreRating,
                        StoreRatingDescribed = ratingDesc,
                        StoreRatingCommunication = ratingComm,
                        StoreRatingShippingSpeed = ratingShip,
                        OrderNumber = viewModel.OrderNumber,
                        RatingNumber = viewModel.RatingNumber,
                        RatingPercent = Convert.ToDecimal(viewModel.RatingPercentNumber),
                        ProcessingTime = string.Empty,
                        
                        
                    };
                    if (!string.IsNullOrEmpty(viewModel.ProductSkuProps))
                    {
                        var productSkuItems = viewModel.ProductSkuProps.Split("::");
                        foreach (var productSkuItem in productSkuItems)
                        {
                            var skuItems = productSkuItem.Split(":");
                            
                        }
                    }

                    
                }

                context.SaveChanges();
            }
        }

    }
}
