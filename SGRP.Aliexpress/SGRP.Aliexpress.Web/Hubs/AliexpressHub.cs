﻿using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SGRP.Aliexpress.Helper;
using SGRP.Aliexpress.Web.Interfaces;
using StackExchange.Redis;

namespace SGRP.Aliexpress.Web.Hubs
{
    public class AliexpressHub : Hub
    {

        public Task UpdateTotalCounter()
        {
            if (Clients != null)
            {
                var connection = RedisConnectionFactory.GetConnection();
                connection.GetSubscriber().Subscribe("aaa", (c, v) =>
                {
                    Clients.All.SendAsync("ReceiveMessage","rupic" ,v);
                });
            }
            return Task.CompletedTask;
            
        }
    }
}