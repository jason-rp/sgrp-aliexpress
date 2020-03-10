using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class ChilkatService
    {
        public static void ActiveChilKat()
        {
            var waffle = "flffegfegkskgkn5lfpa'wfwac";
            var key = "THAOAN.CB10117";
            var __key = "VUVMYnh2MHozMjZC";

            key = Path.Combine(key, Encoding.UTF8.GetString(System.Convert.FromBase64String(__key)));
            if (!new Chilkat.Global().UnlockBundle(key.Replace(
                Encoding.UTF8.GetString(
                    ConvertToHexByte(waffle[15] + "" + waffle.Substring(25, 1))),
                Encoding.UTF8.GetString(
                    ConvertToHexByte(waffle[15] + "" + waffle.Substring(0, 1))))))
            {
                Environment.Exit(0);
            }
        }

        public static byte[] ConvertToHexByte(string hex)
        {
            if (hex == null)
            {
                return null;
            }

            if (hex.Length % 2 != 0)
            {
                return null;
            }

            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var currentHex = hex.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }

            return bytes;
        }
    }
}
