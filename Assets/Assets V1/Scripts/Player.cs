using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;

	public Match match;

	public GameObject redSignalPrefab;
	public GameObject greenSignalPrefab;

	public CanvasRenderer numGreenNodes;

	List<GameObject> greenNodes = new List<GameObject>();

	public int getNumNodes()
	{
		return greenNodes.Count;
	}

	public void addNode(GameObject newGreenNode)
	{
		GetComponent<AudioSource>().clip = rightSong;
		GetComponent<AudioSource>().Play ();
		greenNodes.Add(newGreenNode);
		newGreenNode.gameObject.GetComponent<GreenNode>().Attract(this.gameObject);
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
	}

	// Use this for initialization
	void Start () 
	{

	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.tag == "green" || other.gameObject.tag == "red") 
		{	
			if (other.gameObject.tag == "green" && 
			    getNumNodes() < Game.MAX_NUMBER_GREEN && 
			    !other.gameObject.GetComponent<GreenNode>().isAttract())
			{
				addNode(other.gameObject);
				StartCoroutine(Utility.InstantiateSignal(greenSignalPrefab,gameObject));
			}
			else if (other.gameObject.tag == "red")
			{
				Destroy (other.gameObject);
				removeNode();
				StartCoroutine(Utility.InstantiateSignal(greenSignalPrefab,gameObject));
			}
			numGreenNodes.GetComponent<Text>().text = getNumNodes().ToString();
		}
	}
}
