using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	int numNodes = 0;
	public List<Node> nodes = new List<Node>();
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;
	public Color corNucleu;

	public LastSpot lastSpot;

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
			lastSpot.Ideath(gameObject.tag);
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
		child.GetComponentInChildren<Node>().changeColor (corNucleu);
	}

	// Use this for initialization
	void Start () {
		updateNodes ();
	}
}
