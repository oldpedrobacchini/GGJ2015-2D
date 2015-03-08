using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;


enum ServerState {
	invalid=0,
	waitclient=2
}

public class GameServer : MonoBehaviour {

	private string LocalAddress = "127.0.0.1";
	private int serverState = (int)ServerState.invalid;
	//private bool LANOnly = true;
	private Hashtable players = new Hashtable();
	private int playerCount = 0;
	public GameObject playerPrefab;

	void Start () {
		Application.runInBackground = true;
		LocalAddress = Utility.GetLocalIPAddress();
		Debug.Log("Local IP Address: " + LocalAddress);
		StartServer ();
	}

	void OnConnectedToServer()
	{
		GetComponent<NetworkView>().RPC("SendAllPlayers", RPCMode.Server);
	}
	
	[RPC]
	void SendAllPlayers(NetworkMessageInfo info)
	{
		if(Network.isServer)
		{
			GameObject[] goPlayers = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject gop in goPlayers)
			{
				NetworkPlayer gonp = gop.GetComponent<NodePlayer>().netPlayer;
				NetworkViewID gonvid = gop.GetComponent<NetworkView>().viewID;
				
				if(gonp.ToString() != info.sender.ToString())
				{
					GetComponent<NetworkView>().RPC("JoinPlayer", info.sender, gonvid, gop.transform.position, gonp);
				}
			}
		}
		
	}
	
	void StartServer()
	{
		bool useNat=false;
		//if (LANOnly==true)
		//	useNat=false;
		//else
		//	useNat=!Network.HavePublicAddress();
		
		Network.InitializeServer(16,25000,useNat);
		
	}
	
	void OnServerInitialized() 
	{
		Debug.Log("Server initialized and ready");
		serverState = (int)ServerState.waitclient;
	}
	
	[RPC]
	void ClientUpdatePlayer(Vector3 pos, NetworkMessageInfo info)
	{
		Debug.Log ("ClientUpdatePlayer");

		NetworkPlayer p = info.sender;
		GetComponent<NetworkView>().RPC("ServerUpdatePlayer",RPCMode.Others, p, pos);

		ServerUpdatePlayer(p, pos);
		
	}
	
	[RPC]
	void ServerUpdatePlayer(NetworkPlayer p, Vector3 pos)
	{
		Debug.Log ("ServerUpdatePlayer");
		
		if(players.ContainsKey(p))
		{
			GameObject gop = (GameObject)players[p];
			//gop.GetComponent<MovimentPlayer1>().target = pos;
			gop.GetComponent<MovimentPlayer1>().startMovimentServer(pos);
		}
		
	}
	
	void OnPlayerConnected(NetworkPlayer p) 
	{
		if(Network.isServer)
		{
			playerCount++;
			
			NetworkViewID newViewID = Network.AllocateViewID();
			
			GetComponent<NetworkView>().RPC("JoinPlayer", RPCMode.All, newViewID, Vector3.zero, p);
			
			Debug.Log("Player " + newViewID.ToString() + " connected from " + p.ipAddress + ":" + p.port);
			Debug.Log("There are now " + playerCount + " players.");
		}
	}
	
	[RPC]
	void JoinPlayer(NetworkViewID newPlayerView, Vector3 pos, NetworkPlayer p)
	{
		GameObject newPlayer = Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
		newPlayer.GetComponent<NetworkView>().viewID = newPlayerView;
		newPlayer.tag = "Player";

		newPlayer.GetComponent<MovimentPlayer1>().startMovimentServer(pos);
		newPlayer.GetComponent<NodePlayer>().netPlayer = p;
		players.Add(p,newPlayer);
	}
	
	
	
	void OnPlayerDisconnected(NetworkPlayer player) 
	{
		if(Network.isServer){
			playerCount--;
			
			Debug.Log("Player " + player.ToString() + " disconnected.");
			Debug.Log("There are now " + playerCount + " players.");
			
			GetComponent<NetworkView>().RPC("DisconnectPlayer", RPCMode.All, player);
		}
	}
	
	[RPC]
	void DisconnectPlayer(NetworkPlayer player)
	{
		if(players.ContainsKey(player))
		{
			if((GameObject)players[player]) {
				Destroy((GameObject)players[player]);
			}
			players.Remove(player);
		}
	}
	
	void OnGUI()
	{
		switch (serverState) 
		{		
		case (int)ServerState.waitclient:
			GUILayout.Label("SERVER RUNNING");
			GUILayout.Label("waiting for connections...");
			GUILayout.Space(16);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Connected Players: " + playerCount.ToString());
			GUILayout.EndHorizontal();
			
			if(GUILayout.Button("Kill Server"))
			{
				Network.Disconnect();
				Application.Quit();
			}
			break;
		}
	}
}
