using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

static public class Utility 
{

	static public string GetLocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		
		localIP = host.AddressList[0].ToString();
		
		return localIP;
	}

	static public IEnumerator InstantiateSignal(GameObject signalPrefab, GameObject reference)
	{
		GameObject signal = (GameObject) GameObject.Instantiate (signalPrefab, reference.transform.position, reference.transform.rotation);
		yield return new WaitForSeconds (1.0f);
		GameObject.Destroy(signal);
	}
}
