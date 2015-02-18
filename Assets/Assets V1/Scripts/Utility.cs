using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

static public class Utility {

	static public string GetLocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		
		localIP = host.AddressList[0].ToString();
		
		return localIP;
	}
}
