using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGRP.Aliexpress.Web.Models;

namespace SGRP.Aliexpress.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {

            Random rnd = new Random();


            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Jan",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Feb",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Mar",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Apr",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "May",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "June",
                Quantity = rnd.Next(10)
            });
            var lstModel1 = new List<SimpleReportViewModel>();
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "July",
                Quantity = rnd.Next(10)
            });
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "Aug",
                Quantity = rnd.Next(10)
            });
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "Sep",
                Quantity = rnd.Next(10)
            });
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "Oct",
                Quantity = rnd.Next(10)
            });
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "Nov",
                Quantity = rnd.Next(10)
            });
            lstModel1.Add(new SimpleReportViewModel
            {
                DimensionOne = "Dec",
                Quantity = rnd.Next(10)
            });
            var t = new List<List<SimpleReportViewModel>>
            {
                lstModel,
                lstModel1
            };

            return View(t);
        }
    }
}