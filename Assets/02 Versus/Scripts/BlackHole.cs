using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;
	public GameObject lastSignalPrefab;
	public BehaviorUpDown[] moviments;

	public void StopBlackHole()
	{
		foreach (BehaviorUpDown moviment in moviments)
			Destroy (moviment);
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.GetComponent<NodePlayer>() != null) 
		{
			if (other.GetComponent<NodePlayer>().getNumNodes () == 4) 
			{
				match.finishMatch(other.tag);
				GetComponent<Collider2D>().enabled = false;
				StartCoroutine(Utility.InstantiateSignal(lastSignalPrefab,gameObject));

				foreach(NodeElement greenNode in other.GetComponent<NodePlayer>().greenNodes)
				{
					greenNode.BeginAttractElement(this.gameObject,0.0f,0.1f);
					greenNode.DecreaseElement(100f,Vector3.zero);
				}

				other.GetComponent<NodePlayer>().BeginAttractPlayer(this.gameObject,0.0f,0.1f);
				other.GetComponent<NodePlayer>().DecreasePlayer(10f,Vector3.zero);
			}
		}
	}
}
