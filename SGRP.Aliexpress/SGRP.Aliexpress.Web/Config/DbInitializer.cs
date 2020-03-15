using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Data;

namespace SGRP.Aliexpress.Web.Config
{
    public static class DbInitializer
    {
        //public static void Run(IWebHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        try
        //        {
        //            var context = services.GetRequiredService<ApplicationDbContext>();
        //            context.Database.Migrate();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}
        //public static void Initialize(ApplicationDbContext context)
        //{
        //    context.Database.EnsureCreated();

        //    if (context.Users.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    var users = new List<User>
        //    {
        //        new User
        //        {
        //            Id = 55,
        //            Email = "abc@gmail.com",
        //            Name = "rupicmax"
        //        }
        //    };

        //    foreach (var user in users)
        //    {
        //        context.Users.Add(user);
        //    }

        //    context.SaveChanges();
        //}
    }
}
