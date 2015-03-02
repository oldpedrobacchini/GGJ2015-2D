using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	int numNodes = 0;
//	public List<Node> nodes = new List<Node>();
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;
	//public Color corNucleu;

	public LastSpot lastSpot;

	public GameObject redSinalPrefab;
	public GameObject greenSinalPrefab;

	public int getNumNodes()
	{
		return numNodes;
	}

	public void addNode()
	{
		audio.clip = rightSong;
		audio.Play ();
		numNodes += 1;
		if (numNodes > Game.MAX_NUMBER_GREEN) 
		{
			numNodes = 4;
		}
//		else 
//		{
//			Destroy (destoyerChild);
//			updateNodes ();
//		}
	}
	
	public void removeNode()
	{
		numNodes -= 1;
		if (numNodes == -1) 
		{
			lastSpot.Ideath(gameObject.tag);
		}
//		else {
//			Destroy (destoyerChild);
//			updateNodes ();
//		}
	}
	
//	public void updateNodes()
//	{
//		Debug.Log (nodes.Count+" "+numNodes);
//		GameObject child = (GameObject)Instantiate(nodes[numNodes].gameObject);
//		child.transform.parent = gameObject.transform;
//		child.transform.localPosition = Vector3.zero;
//		child.GetComponentInChildren<Node>().changeColor (corNucleu);
//	}

	// Use this for initialization
	void Start () 
	{
//		updateNodes ();
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "green" || other.gameObject.tag == "red") 
		{
			//Player player = gameObject.GetComponentInParent<Player> ();
			
			//Debug.Log (other.gameObject.tag+" "+gameObject.name);
			
			if (other.gameObject.tag == "green" && numNodes < Game.MAX_NUMBER_GREEN)
			{
				addNode();
			}
			else if (other.gameObject.tag == "red")
			{
				Destroy (other.gameObject);
				removeNode();
				StartCoroutine(InstantiateRedSinal());
			}
		}
	}

	IEnumerator InstantiateRedSinal()
	{
		GameObject redSinal = (GameObject)Instantiate (redSinalPrefab, gameObject.transform.position, gameObject.transform.rotation);
		yield return new WaitForSeconds (1.0f);
		Destroy(redSinal);
	}
}
