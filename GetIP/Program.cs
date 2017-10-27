using System;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;//Stores the Physical Address return type

namespace GetMyNetworkInformation
{
    class MainClass
    {
        public static string GetARPTable()
        {
            Process getArpProcess = new Process();
            ProcessStartInfo arpProcessStart = new ProcessStartInfo();
            arpProcessStart.FileName = "arp";
            arpProcessStart.Arguments = "-a";

            arpProcessStart.RedirectStandardOutput = true;
            arpProcessStart.UseShellExecute = false;
            getArpProcess.StartInfo = arpProcessStart;
            getArpProcess.Start();

            String arpOutPut = getArpProcess.StandardOutput.ReadToEnd();

            return arpOutPut;
            
        }
		public static string GetLocalHostname()
		{
            //Returns the computers hostname or name of machine shared on the network as a string
			string myHostName = Dns.GetHostName();
			return myHostName;
		}
        public static string GetLocalIPAddress()
		{
			//Returns the computers Internet Version 4 Protocol Address as a string
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
		public static void Main(string[] args)
		{
			//Code below converts returns from methods into strings to output to the user
			string myMacAddress = GetMacAddress().ToString();//Mac Address is the only method with a return type that isn't a string so ToString is needed for the casting.
			string ipv4Address = GetLocalIPAddress();
			string computerName = GetLocalHostname();
            string arpOutPut = GetARPTable().ToString();
			Console.WriteLine("ComputerName:{0}\nPhysical Address:{1}\nIPV4Address:{2}", computerName, myMacAddress, ipv4Address);
            Console.WriteLine("Below are other Computers on my Network!\n{0}",arpOutPut);
           

		}
    }
}
