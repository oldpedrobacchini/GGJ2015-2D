using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	public string gameName = "Pedro_Bacchini_Estudo_Network_01";
	public int serverPort = 25001;
	public GameObject serverButton;

	public Transform LobbyRooms;
	public Transform PanelRoom;

	public UINetworkManager UImanager;

	public GameObject refreshSprite;
	public GameObject refreshButton;

	public GameObject Player1;
	public GameObject Player2;

	public GameObject ButtonReadyPlayer1;
	public GameObject ButtonReadyPlayer2;

	void Start()
	{
		RefreshHostsList ();
	}

	void spawnPlayer(GameObject playerPrefab)
	{
		Network.Instantiate (playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation, 0);
	}

	void OnServerInitialized() 
	{
		Debug.Log("Server initialized and ready");
		UImanager.UpdateUI ();

		Player2.SetActive (false);
		ButtonReadyPlayer2.SetActive (false);
		ButtonReadyPlayer2.GetComponent<Button> ().enabled = false;

		ButtonReadyPlayer1.GetComponent<Button> ().enabled = true;
		ButtonReadyPlayer1.GetComponent<Image> ().color = Color.white;
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		UImanager.UpdateUI ();

		Player2.SetActive (true);
		ButtonReadyPlayer2.SetActive (true);
		ButtonReadyPlayer2.GetComponent<Button> ().enabled = true;

		ButtonReadyPlayer1.GetComponent<Button> ().enabled = false;
		ButtonReadyPlayer1.GetComponent<Image> ().color = new Color (200f/255f, 200f/255f, 200f/255f, 128f/255f);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log("Server registered");
		}

		if(msEvent == MasterServerEvent.HostListReceived)
		{
			HostData[] hostData = MasterServer.PollHostList();
			ServerInfo[] serversInfo = FindObjectsOfType<ServerInfo>();

			for (int i = 0; i < hostData.Length; i++)
			{

				bool findHostData = false;

				for(int j = 0; j<serversInfo.Length;j++)
				{
					if(serversInfo[j] != null)
					{
						if(serversInfo[j].hostData.guid == hostData[i].guid)
						{
							findHostData = true;
							serversInfo.SetValue(null,j);
							break;
						}
					}
				}

				if(findHostData)
				{
					findHostData = false;
					continue;
				}

				Debug.Log("Game name: " + hostData[i].gameName);
				GameObject buttonServer = GameObject.Instantiate (serverButton);
				
				buttonServer.transform.SetParent(LobbyRooms);
				buttonServer.transform.localScale = Vector3.one;
				buttonServer.transform.GetChild(0).GetComponent<Text>().text = hostData[i].gameName;
				buttonServer.GetComponent<ServerInfo>().hostData = hostData[i];
				
				buttonServer.GetComponent<Button>().onClick.AddListener(delegate {
					NetworkConected(buttonServer.GetComponent<ServerInfo>().hostData);
				});
			}

			if(hostData.Length == 0)
			{
				for(int j = 0; j<serversInfo.Length;j++)
				{
					Destroy(serversInfo[j].gameObject);
				}
			}
			else
			{
				for(int j = 0; j<serversInfo.Length;j++)
				{
					if(serversInfo[j] != null)
					{
						Destroy(serversInfo[j].gameObject);
					}
				}
			}
			refreshSprite.SetActive (false);
			refreshButton.SetActive (true);
		}
	}

	void OnPlayerConnected(NetworkPlayer player) 
	{
		Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
		Player2.SetActive (true);
		ButtonReadyPlayer2.SetActive (true);
	}

	void OnPlayerDisconnected(NetworkPlayer player) 
	{
		Debug.Log("Clean up after player " + player);
		Player2.SetActive (false);
		ButtonReadyPlayer2.SetActive (false);
		//Network.RemoveRPCs(player);
		//Network.DestroyPlayerObjects(player);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
		else
		{
			Debug.Log("Successfully diconnected from the server");
		}
	}
	
	void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		Debug.Log (info);
	}
	
	void OnFailedToConnect(NetworkConnectionError info)
	{
		Debug.Log ("Sala Provalvelmente nao existe mais");
		Debug.Log (info);
		RefreshHostsList ();
	}

	public void StartServer(string roomName, int serverPort)
	{
		Debug.Log("Starting Server");
		bool useNat = Network.HavePublicAddress();
		Debug.Log("HavePublicAddress: "+useNat);
		Network.InitializeServer(2, serverPort, !useNat);
		MasterServer.RegisterHost(gameName,roomName,"Esse e o primeiro estudo de Network com Unity3D");
	}

	public void NetworkConected(HostData hostData)
	{
		Debug.Log("Connect in "+hostData.gameName);
		Network.Connect (hostData);
	}

	public void RefreshHostsList()
	{
		Debug.Log("Refreshing");
		MasterServer.RequestHostList(gameName);
		refreshSprite.SetActive (true);
		refreshButton.SetActive (false);
	}
	
	public void Disconnect()
	{
		if (ButtonReadyPlayer2.GetComponent<ReadyButtonPlayer2> ().getMyState () == ReadyButtonPlayer2.state.cancel) 
		{
			ButtonReadyPlayer2.GetComponent<ReadyButtonPlayer2> ().OnClick ();
		}
		Network.Disconnect();
		MasterServer.UnregisterHost();
		UImanager.UpdateUI ();
	}
}



