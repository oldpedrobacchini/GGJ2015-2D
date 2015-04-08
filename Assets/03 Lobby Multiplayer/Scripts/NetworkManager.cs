using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	//public GameObject player1Prefab;
	//public GameObject player2Prefab;
	//public GameObject spawnObject;

	public string gameName = "Pedro_Bacchini_Estudo_Network_01";
	public int serverPort = 25001;
	public GameObject serverButton;
	public Transform panelGUI;

	private HostData[] hostData;

	public void StartServer()
	{
		Debug.Log("Starting Server");
		bool useNat = Network.HavePublicAddress();
		Debug.Log("HavePublicAddress: "+useNat);
		Network.InitializeServer(32, serverPort, !useNat);
		MasterServer.RegisterHost(gameName,"Estudos Network 01","Esse e o primeiro estudo de Network com Unity3D");
	}

	//void spawnPlayer(GameObject playerPrefab)
	//{
	//	Network.Instantiate (playerPrefab, spawnObject.transform.position, spawnObject.transform.rotation, 0);
	//}

	void OnServerInitialized() 
	{
		Debug.Log("Server initialized and ready");
		//spawnPlayer (player1Prefab);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		//spawnPlayer (player2Prefab);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log("Server registered");
		}

		if(msEvent == MasterServerEvent.HostListReceived)
		{
			hostData = MasterServer.PollHostList();

			Debug.Log(hostData.Length);

			for (int i = 0; i < hostData.Length; i++)
			{
				Debug.Log("Game name: " + hostData[i].gameName);
				var buttonServer = GameObject.Instantiate (serverButton);
				
				buttonServer.transform.SetParent(panelGUI);
				buttonServer.GetComponent<RectTransform>().anchoredPosition = new Vector2(10,-10);
				buttonServer.transform.GetChild(0).GetComponent<Text>().text = hostData[i].gameName;
				buttonServer.GetComponent<ServerInfo>().hostData = hostData[i];
				
				buttonServer.GetComponent<Button>().onClick.AddListener(delegate {
					NetworkConected(buttonServer.GetComponent<ServerInfo>().hostData);
				});
			}
		}
	}

	public void RefreshHostsList()
	{
		Debug.Log("Refreshing");
		MasterServer.RequestHostList(gameName);
	}

	public void NetworkConected(HostData hostData)
	{
		Debug.Log("Connect in "+hostData.gameName);
		Network.Connect (hostData);
	}

}



