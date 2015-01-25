using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	int numNodes = 0;
	public List<Node> nodes = new List<Node>();
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;

	public int getNumNodes()
	{
		return numNodes;
	}

	public void addNode(GameObject destoyerChild)
	{
		audio.clip = rightSong;
		audio.Play ();
		numNodes += 1;
		if (numNodes > 3) {
			numNodes = 3;
		}
		else {
			Destroy (destoyerChild);
			updateNodes ();
		}
	}
	
	public void removeNode(GameObject destoyerChild)
	{
		numNodes -= 1;
		if (numNodes == -1) {
			Game game = FindObjectOfType<Game>();
			game.ResetData();
			Application.LoadLevel (Application.loadedLevelName);
		}
		else {
			Destroy (destoyerChild);
			updateNodes ();
		}
	}
	
	public void updateNodes()
	{
//		Debug.Log (nodes.Count+" "+numNodes);
		GameObject child = (GameObject)Instantiate(nodes[numNodes].gameObject);
		child.transform.parent = gameObject.transform;
		child.transform.localPosition = Vector3.zero;
	}

	// Use this for initialization
	void Start () {
		updateNodes ();
		Game game = FindObjectOfType<Game> ();
		gameObject.transform.position = game.getAvaliablePosition (gameObject.transform.GetChild(0).localScale.x);
	}
}
