using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NodePlayer : Node
{
	public AudioClip rightSong;
	public AudioClip wrongSong;
	
	public NetworkPlayer netPlayer;

	public Match match;

	public GameObject redSignalPrefab;
	public GameObject greenSignalPrefab;

	public CanvasRenderer numGreenNodes;

	[HideInInspector]
	public List<NodeElement> greenNodes = new List<NodeElement>();

	public int getNumNodes()
	{
		return greenNodes.Count;
	}

	public void addNode(NodeElement newGreenNode)
	{
		GetComponent<AudioSource>().clip = rightSong;
		GetComponent<AudioSource>().Play ();
		greenNodes.Add(newGreenNode);

		if (tag == "Player1")
			newGreenNode.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.red;
		if (tag == "Player2")
			newGreenNode.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = Color.cyan;

		newGreenNode.BeginAttract(this.gameObject,Random.Range (3.0f, 5.0f),Random.Range (0.1f, 0.4f));
		newGreenNode.StopUpDown ();
	}
	
	public void removeNode()
	{
		if (getNumNodes () > 0) 
		{
			NodeElement removeGreenNode = greenNodes [0];
			greenNodes.RemoveAt (0);
			removeGreenNode.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
			removeGreenNode.StopAttract();
			removeGreenNode.BeginUpDown();
		}
		else
		{
			match.Ideath(gameObject.tag);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "green" || collision.gameObject.tag == "red") 
		{	
			if (collision.gameObject.tag == "green" && 
			    getNumNodes() < Game.MAX_NUMBER_GREEN && 
			    !collision.gameObject.GetComponent<NodeElement>().isAttract())
			{
				addNode(collision.gameObject.GetComponent<NodeElement>());
				StartCoroutine(Utility.InstantiateSignal(greenSignalPrefab,gameObject));
			}
			else if (collision.gameObject.tag == "red")
			{
				collision.gameObject.GetComponent<NodeElement>().Decrease(10f,Vector3.zero);
				collision.collider.enabled = false;

				removeNode();
				StartCoroutine(Utility.InstantiateSignal(redSignalPrefab,gameObject));
			}
			numGreenNodes.GetComponent<Text>().text = getNumNodes().ToString();
		}
	}
}
