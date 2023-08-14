using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Common
{
    public class IpAddressFinder
    {
        public static string GetHostName()
        {
            string ip = "";
            var hostName = Dns.GetHostName();
            var adresses = Dns.GetHostAddresses(hostName);
            foreach (var address in adresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = address.ToString();
                }
            }
            return ip;
        }
    }
}
