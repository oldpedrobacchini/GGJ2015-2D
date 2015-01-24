using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	int numNodes = 0;
	public List<Node> nodes = new List<Node>();
	public AudioClip rightSong;
	public AudioClip wrongSong;

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
		if (numNodes == -1)
			Application.LoadLevel (Application.loadedLevelName);
		else {
			Destroy (destoyerChild);
			updateNodes ();
		}
	}
	
	public void updateNodes()
	{
//		Debug.Log (nodes.Count+" "+numNodes);
		//Debug.Log ("update node");
		GameObject child = (GameObject)Instantiate(nodes[numNodes].gameObject);
		child.transform.parent = gameObject.transform;
		child.transform.localPosition = Vector3.zero;
	}

	// Use this for initialization
	void Start () {
		updateNodes ();
		Game game = FindObjectOfType<Game> ();
		gameObject.transform.position = game.getAvaliablePosition (gameObject.transform.localScale.x);
	}
}
