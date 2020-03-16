using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGRP.Aliexpress.Web.Interfaces;

namespace SGRP.Aliexpress.Web
{
    public class ConnectionMapping : IConnectionMapping
    {
        private readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();


        public string SetConnection(int productId, int memberUid, string connectionId)
        {
            var key = memberUid + "-" + productId;
            var connectionByMember = _connections.ContainsKey(key) ? _connections[key] : string.Empty;
            if (connectionByMember != connectionId)
            {
                _connections.AddOrUpdate(key, connectionId, (k, v) => connectionId);
            }
            return connectionByMember;
        }

        public bool ContainsConnection(string connectionId)
        {
            return _connections.Any(n => n.Value == connectionId);
        }

        public string RemoveConnection(string connectionId)
        {
            var result = string.Empty;
            var connectionById = _connections.SingleOrDefault(n => n.Value == connectionId);
            if (connectionById.Key != null)
            {
                _connections.TryRemove(connectionById.Key, out result);
            }

            return result;

        }


        public int Count() => _connections.Count;
    }
}
