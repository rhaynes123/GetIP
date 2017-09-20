using System;
using System.Net;
using System.Net.NetworkInformation;

namespace GetMyNetworkInformation
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string myMacAddress = GetMacAddress().ToString();
            string ipv4Address = GetLocalIPAddress();
            string computerName = GetLocalHostname();
            Console.WriteLine("ComputerName:{0}\nPhysical Address:{1}\nIPV4Address:{2}",computerName,myMacAddress,ipv4Address);
		}
		public static string GetLocalHostname()
		{
			string myHostName = Dns.GetHostName();
			return myHostName;
		}

        public static string GetLocalIPAddress()
		{
            string myIPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            return myIPAddress;
		}
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
    }
}
