using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkView))]
public class MatchMultiplayer : MonoBehaviour 
{
	public GameObject PrefabServer;
	public GameObject PrefabClient;

	public GameObject blackHole;

	public GameObject player1Prefab;
	public int indiceSkinPlayer1;
	public GameObject player2Prefab;
	public int indiceSkinPlayer2;

	public GameObject[] SkinsPrefabs;

	NetworkView _networkView;

	public Text timer_tex;
	private bool isTimer = false;
	private float timer = 0;

	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		_networkView = GetComponent<NetworkView> ();

//		GameObject skin;

		if (Network.isServer) 
		{
			//Seta a posicao do Jogador 1
			player = (GameObject)Network.Instantiate (player1Prefab, new Vector3 (0f, 5f, 0f), Quaternion.identity, 0);
			/*
			skin = (GameObject)Network.Instantiate (SkinsPrefabs [indiceSkinPlayer1], player.transform.position, Quaternion.identity, 0) as GameObject;
			skin.transform.SetParent (player.transform);
			skin.GetComponent<ConfigurationSkinPlayer> ().UpdateSkinPlayer (ConfigurationSkinPlayer.playerType.Player1);
			*/
			player.GetComponent<MovimentPlayer> ().enabled = true;
		}
		
		if (Network.isClient)
		{
			//Seta a posicao do Jogador 2
			player = (GameObject) Network.Instantiate(player2Prefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
			/*
			skin = (GameObject) Network.Instantiate (SkinsPrefabs[indiceSkinPlayer2], player.transform.position, Quaternion.identity, 0) as GameObject;
			skin.transform.SetParent (player.transform);
			skin.GetComponent<ConfigurationSkinPlayer> ().UpdateSkinPlayer (ConfigurationSkinPlayer.playerType.Player2);
			*/
			player.GetComponent<MovimentPlayer> ().enabled = true;
		}



		if(Network.isServer)
		{
			Debug.Log("Start Sincronization");
			_networkView.RPC("SincronizationWithClient", RPCMode.OthersBuffered, "Start Sincronization");
		}
	}
	
	[RPC]
	void SincronizationWithClient(string someInfo) 
	{
		Debug.Log (someInfo);
		Debug.Log ("Back Sincronization");
		_networkView.RPC("SincronizationBackWithServer", RPCMode.Server, "Back Sincronization");
	}

	[RPC]
	void SincronizationBackWithServer(string someInfo) 
	{
		Debug.Log (someInfo);
		_networkView.RPC("StartNetwork", RPCMode.All, "Start Game");
	}

	[RPC]
	void StartNetwork(string someInfo)
	{
		Debug.Log(someInfo);
	
		if(Network.isServer)
		{
			GameObject matchServer = GameObject.Instantiate(PrefabServer);
			matchServer.GetComponent<MatchServerMultiplayer>().match = this;
		}
		else if(Network.isClient)
		{
			GameObject.Instantiate(PrefabClient);
		}
	}

	public void positionBlackHole(Vector3 newPosition)
	{
		_networkView.RPC("positionBlackHoleNetwork", RPCMode.All, newPosition);
	}

	[RPC]
	void positionBlackHoleNetwork(Vector3 newPosition)
	{
		blackHole.transform.position = newPosition;
	}

	public void positionPlayer2(Vector3 newPosition)
	{
		_networkView.RPC("positionPlayer2Network", RPCMode.Others, newPosition);
	}
	
	[RPC]
	void positionPlayer2Network(Vector3 newPosition)
	{
		player.transform.position = newPosition;
	}

	void Update () 
	{
		if(isTimer)
		{
			timer += Time.deltaTime;
			timer_tex.text = timer.ToString();
		}
	}
}
