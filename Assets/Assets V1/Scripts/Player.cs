using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{

//	int numNodes = 0;
//	public List<Node> nodes = new List<Node>();
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;
	//public Color corNucleu;

	public Match match;

	public GameObject redSinalPrefab;
	public GameObject greenSinalPrefab;

	public CanvasRenderer numGreenNodes;

	List<GameObject> greenNodes = new List<GameObject>();

	public int getNumNodes()
	{
//		return numNodes;
		return greenNodes.Count;
	}

	public void addNode(GameObject newGreenNode)
	{
		GetComponent<AudioSource>().clip = rightSong;
		GetComponent<AudioSource>().Play ();
		greenNodes.Add(newGreenNode);
		newGreenNode.gameObject.GetComponent<GreenNode>().Attract(this.gameObject);
//		if (numNodes > Game.MAX_NUMBER_GREEN) 
//		{
//			numNodes = 4;
//		}
//		else 
//		{
//			Destroy (destoyerChild);
//			updateNodes ();
//		}
	}
	
	public void removeNode()
	{
		if (getNumNodes () > 0) 
		{
			GameObject removeNode = greenNodes [0];
			greenNodes.RemoveAt (0);
			removeNode.GetComponent<GreenNode> ().UnAttract ();
		}
		else
		{
			match.Ideath(gameObject.tag);
		}

//		numNodes -= 1;
//		if (numNodes == -1) 
//		{
//			lastSpot.Ideath(gameObject.tag);
//		}
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

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.tag == "green" || other.gameObject.tag == "red") 
		{
			//Player player = gameObject.GetComponentInParent<Player> ();
			
			//Debug.Log (other.gameObject.tag+" "+gameObject.name);
			
			if (other.gameObject.tag == "green" && 
			    getNumNodes() < Game.MAX_NUMBER_GREEN && 
			    !other.gameObject.GetComponent<GreenNode>().isAttract())
			{
				addNode(other.gameObject);
				StartCoroutine(InstantiateGreenSinal());
			}
			else if (other.gameObject.tag == "red")
			{
				Destroy (other.gameObject);
				removeNode();
				StartCoroutine(InstantiateRedSinal());
			}
			numGreenNodes.GetComponent<Text>().text = getNumNodes().ToString();
		}
	}

	IEnumerator InstantiateRedSinal()
	{
		GameObject redSinal = (GameObject)Instantiate (redSinalPrefab, gameObject.transform.position, gameObject.transform.rotation);
		yield return new WaitForSeconds (1.0f);
		Destroy(redSinal);
	}

	IEnumerator InstantiateGreenSinal()
	{
		GameObject greenSinal = (GameObject) Instantiate (greenSinalPrefab, gameObject.transform.position, gameObject.transform.rotation);
		yield return new WaitForSeconds (1.0f);
		Destroy(greenSinal);
	}
}
