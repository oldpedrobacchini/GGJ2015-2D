using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class NetworkLoading : MonoBehaviour 
{
	int lastLevelPrefix = 0;
	NetworkView _networkView; 

	void Awake ()
	{
		DontDestroyOnLoad(this);
		_networkView = GetComponent<NetworkView> ();
		_networkView.group = 1;
	}
	
	public void LoadingSceneNetwork(string sceneName)
	{
		Debug.Log ("Loading Game");
		_networkView.RPC( "LoadLevel", RPCMode.AllBuffered, sceneName, lastLevelPrefix + 1);
	}

	[RPC]
	void LoadLevel(string level, int levelPrefix)
	{
		Debug.Log ("Loading Level " + level + " with prefix " + levelPrefix);
		lastLevelPrefix = levelPrefix;
		
		//There is no reason to send any more data over the network on the default channel, 
		//because we are about to load level, because all those objects will get deleted anyway
		Network.SetSendingEnabled (0, false);
		
		//We need to stop receiving because first the level must be loaded 
		//Once the level is loaded, RPC's and other state update attached to objects in the level are allowed to fire
		Network.isMessageQueueRunning = false;
		
		//All network views loaded from a level will get a prefix into their NetworkViewID
		//This will prevent old updates from clients leaking into a newly created scene.
		Network.SetLevelPrefix (levelPrefix);
		Application.LoadLevel (level);
		//yield return null;
		
		//Allow receiving data again 
		Network.isMessageQueueRunning = true;
		//Now the level has been loaded and we can start sending out data
		Network.SetSendingEnabled (0, true);

		Destroy (gameObject);
	}
}
