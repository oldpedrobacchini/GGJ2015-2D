using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

enum ClientState {
	invalid=0,
	joinmenu=1,
	waitserver=2,
	failconnect=3,
	playing=4
}



public class GameClient : MonoBehaviour {
	
	private string LocalAddress = "127.0.0.1";
	private string ServerAddress = "127.0.0.1";
	private int clientState = (int)ClientState.invalid;
	private float nextNetworkUpdateTime = 0.0F;
	private GameObject localPlayerObject;
	private Hashtable players = new Hashtable();
	private Vector3 lastLocalPlayerPosition;
	
	public NetworkPlayer localPlayer;
	public GameObject playerPrefab;
	public float networkUpdateIntervalMax = 0.1F; // maximum of 10 updates per second
	
	
	void Start () {
		LocalAddress = Utility.GetLocalIPAddress();
		ServerAddress = LocalAddress;
		Debug.Log("Local IP Address: " + LocalAddress);
		clientState = (int)ClientState.joinmenu;
	}
	
	
	void Update () {
		
		if(Network.isClient && Time.realtimeSinceStartup > nextNetworkUpdateTime)
		{
			nextNetworkUpdateTime = Time.realtimeSinceStartup + networkUpdateIntervalMax;
			if(localPlayerObject!=null)
			{
				if(lastLocalPlayerPosition != localPlayerObject.transform.position)
				{
					lastLocalPlayerPosition = localPlayerObject.transform.position;
					GetComponent<NetworkView>().RPC("ClientUpdatePlayer",RPCMode.Server,lastLocalPlayerPosition);
				}
			}
		}
	}
	
	void ConnectToServer()
	{
		Network.Connect(ServerAddress, 25000);
		clientState = (int)ClientState.waitserver;
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
				NetworkPlayer gonp = gop.GetComponent<Player>().netPlayer;
				NetworkViewID gonvid = gop.GetComponent<NetworkView>().viewID;
				
				// only tell the requestor about others
				
				// we make this comparison using the
				// server-assigned index number of the 
				// player instead of the ipAddress because
				// more than one player could be playing
				// under one ipAddress -- ToString()
				// returns this player index number
				
				
				if(gonp.ToString() != info.sender.ToString())
				{
					GetComponent<NetworkView>().RPC("JoinPlayer", info.sender, gonvid, gop.transform.position, gonp);
				}
			}
		}
		
	}
	
	void OnFailedToConnect(NetworkConnectionError error) 
	{
		Debug.Log("Could not connect to server: " + error);
		clientState = (int)ClientState.failconnect;
	}

	[RPC]
	void ClientUpdatePlayer(Vector3 pos, NetworkMessageInfo info)
	{
		Debug.Log ("ClientUpdatePlayer");
		// a client is sending us a position update
		// normally you would do a lot of bounds checking here
		// but for this simple example, we'll just
		// trust the player (normally wouldn't do this)
		
		
		NetworkPlayer p = info.sender;
		GetComponent<NetworkView>().RPC("ServerUpdatePlayer",RPCMode.Others, p, pos);
		
		// now update it for myself the server
		
		ServerUpdatePlayer(p, pos);
		
	}
	
	[RPC]
	void ServerUpdatePlayer(NetworkPlayer p, Vector3 pos)
	{
		Debug.Log ("ServerUpdatePlayer");
		// the server is telling us to update a player
		// again, normally you would do a lot of bounds
		// checking here, but this is just a simple example
		
		if(players.ContainsKey(p))
		{
			GameObject gop = (GameObject)players[p];
			//gop.GetComponent<MovimentPlayer1>().target = pos;
			gop.GetComponent<MovimentPlayer1>().startMovimentServer(pos);
		}
		
	}

	
	[RPC]
	void JoinPlayer(NetworkViewID newPlayerView, Vector3 pos, NetworkPlayer p)
	{
		// instantiate the prefab
		// and set some of its properties
		
		GameObject newPlayer = Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
		newPlayer.GetComponent<NetworkView>().viewID = newPlayerView;
		newPlayer.tag = "Player";
		
		// set the remote player's target to its current location
		// so that non-moving remote player don't move to the origin
		//newPlayer.GetComponent<MovimentPlayer1>().target = pos;
		newPlayer.GetComponent<MovimentPlayer1>().startMovimentServer(pos);
		
		// most importantly, populate the NetworkPlayer
		// structure with the data received from the player
		// this will allow us to ignore updates from ourself
		
		newPlayer.GetComponent<Player>().netPlayer = p;
		
		// the local GameObject for any player is unknown to
		// the server, so it can only send updates for NetworkPlayer
		// IDs - which we need to translate to a player's local
		// GameObject representation
		
		// to do this, we will add the player to the "players" Hashtable
		// for fast reverse-lookups for player updates
		// Hashtable structure is NetworkPlayer --> GameObject
		
		
		players.Add(p,newPlayer);
		
		if(Network.isClient) 
		{
			Debug.Log(p.ipAddress+" Test "+LocalAddress);
			if(p.ipAddress==LocalAddress)
			{
				Debug.Log("Server accepted my connection request, I am real player now: " + newPlayerView.ToString());
				
				// because this is the local player, activate the character controller
				
				newPlayer.GetComponent<MovimentPlayer1>().isLocalPlayer = true;
				
				// also, set the global localPlayerObject as a convenience variable
				// to easily find the local player GameObject to send position updates
				
				localPlayerObject = newPlayer;
				
				// also, now put us into the "playing" GameState
				
				clientState = (int)ClientState.playing;
				
				// and finally, attach the main camera to me
				
				//Camera.main.transform.parent = newPlayer.transform;
				//Camera.main.transform.localPosition = new Vector3(0,1,0);
				
			} else {
				
				Debug.Log("Another player connected: " + newPlayerView.ToString());
				
				// because this in not the local player, deactivate the character controller
				
				newPlayer.GetComponent<MovimentPlayer1>().isLocalPlayer = false;
			}
		}
	}

	
	[RPC]
	void DisconnectPlayer(NetworkPlayer player)
	{
		if(Network.isClient) 
		{
			Debug.Log("Player Disconnected: " + player.ToString());
		}
	}

	// just GUI from here on out
	// nothing too interesting :)
	
	void OnGUI()
	{
		switch (clientState) 
		{
		case (int)ClientState.joinmenu:
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Server Address: ");
			ServerAddress = GUILayout.TextField(ServerAddress);
			GUILayout.EndHorizontal();
			
//			LANOnly = GUILayout.Toggle(LANOnly, "Local Network Only");
			
			if(GUILayout.Button("Join!"))
			{
				ConnectToServer();
			}
			break;
			
		case (int)ClientState.waitserver:
			GUILayout.Label("Connecting...");
			if(GUILayout.Button("Cancel"))
			{
				Network.Disconnect();
				clientState = (int)ClientState.joinmenu;
			}
			break;
			
		case (int)ClientState.failconnect:
			GUILayout.Label("Connection to server failed");
			if(GUILayout.Button("I'll check my firewall, IP Address, Server Address, etc..."))
			{
				clientState = (int)ClientState.joinmenu;
			}
			break;

		case (int)ClientState.playing:
			GUILayout.Label("Conectado com sucesso!");
			GUILayout.Label("---------------------");
			GUILayout.Label("WASD keys to move");
			break;	
		}
	}
	
}