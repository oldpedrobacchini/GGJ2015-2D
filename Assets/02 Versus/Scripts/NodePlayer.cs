using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NodePlayer : Node
{
	public AudioClip greenEffect;
	public AudioClip redEffect;

	public AudioSource audioSource;
	
	public NetworkPlayer netPlayer;

	public Match match;

	public GameObject redSignalPrefab;
	public GameObject greenSignalPrefab;

	public Material playerMaterial;
	public Material greenMaterial;

	public MatchPoints matchPoints;

	[HideInInspector]
	public List<NodeElement> greenNodes = new List<NodeElement>();

	public int getNumNodes()
	{
		return greenNodes.Count;
	}

	public void addNode(NodeElement newGreenNode)
	{
		audioSource.clip = greenEffect;
		audioSource.Play ();

		newGreenNode.GetComponent<Rigidbody2D> ().Sleep ();
		greenNodes.Add(newGreenNode);

		matchPoints.UpdatePoints (greenNodes.Count);

		foreach(SkinnedMeshRenderer meshRender in newGreenNode.meshRenders)
		{
			meshRender.material = playerMaterial;
		}

		newGreenNode.BeginAttractElement(this.gameObject,Random.Range (3.0f, 5.0f),Random.Range (0.1f, 0.4f));
	}
	
	public void removeNode(Vector2 contactsDiretion)
	{
		audioSource.clip = redEffect;
		audioSource.Play ();

		if (getNumNodes () > 0) 
		{
			NodeElement removeGreenNode = greenNodes [0];
			greenNodes.RemoveAt (0);

			matchPoints.UpdatePoints (greenNodes.Count);

			foreach(SkinnedMeshRenderer meshRender in removeGreenNode.meshRenders)
			{
				meshRender.material = greenMaterial;
			}

			removeGreenNode.StopAttractElement();
			removeGreenNode.GetComponent<Rigidbody2D>().AddForce(contactsDiretion*500);
		}
		else
		{
			match.Ideath(gameObject.tag);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "green" || collision.gameObject.tag == "red" || collision.gameObject.tag == "PowerUp") 
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
				collision.gameObject.GetComponent<NodeElement>().DecreaseElement(60f,Vector3.zero);
				removeNode(collision.contacts[0].normal);
				StartCoroutine(Utility.InstantiateSignal(redSignalPrefab,gameObject));
			}
			else if (collision.gameObject.tag == "PowerUp")
			{
				Debug.Log("Tirou todos os Nodes Verdes do oponente");
				removeNode(collision.contacts[0].normal);
				removeNode(collision.contacts[1].normal);
				removeNode(collision.contacts[2].normal);
				removeNode(collision.contacts[3].normal);
			}
		}
	}

	public void DecreasePlayer(float scaleFator,Vector3 lessScale)
	{
		Decrease (scaleFator, lessScale);
	}

	public void BeginAttractPlayer(GameObject target, float minDistanceAttract, float attractSmoothTime)
	{
		BeginAttract (target, minDistanceAttract, attractSmoothTime);
	}
}
