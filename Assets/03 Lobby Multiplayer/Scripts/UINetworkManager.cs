using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINetworkManager : MonoBehaviour 
{
	public enum type { Lobby, ServerClient, Server, Client};

	public UIElementNetwork[] UIElementsNetwork;

	void Start()
	{
		UpdateUI ();
	}

	// Update is called once per frame
	public void UpdateUI () 
	{

		foreach(UIElementNetwork UIElementNetwork in UIElementsNetwork)
		{
			switch(UIElementNetwork.type)
			{
			case type.Lobby: 
				if(Network.isClient || Network.isServer)
				{
					UIElementNetwork.gameObject.SetActive(false);
				}
				else
				{
					UIElementNetwork.gameObject.SetActive(true);
				}
				break;
			case type.ServerClient:
				if(Network.isClient || Network.isServer)
				{
					UIElementNetwork.gameObject.SetActive(true);
				}
				else
				{
					UIElementNetwork.gameObject.SetActive(false);
				}
				break;
			case type.Server:
				if(Network.isServer)
				{
					UIElementNetwork.gameObject.SetActive(true);
				}
				else
				{
					UIElementNetwork.gameObject.SetActive(false);
				}
				break;
			case type.Client:
				if(Network.isClient)
				{
					UIElementNetwork.gameObject.SetActive(true);
				}
				else
				{
					UIElementNetwork.gameObject.SetActive(false);
				}
				break;
			}
		}
	}
}
