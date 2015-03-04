using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;
	public GameObject lastSignalPrefab;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.GetComponent<Player>() != null) 
		{
			if (other.GetComponent<Player>().getNumNodes () == 4) 
			{
				match.finishMatch(other.gameObject.GetComponent<Player> ().tag);
				GetComponent<Collider2D>().enabled = false;
				StartCoroutine(Utility.InstantiateSignal(lastSignalPrefab,gameObject));

				foreach(GameObject greenNode in other.GetComponent<Player>().greenNodes)
				{
					greenNode.GetComponent<Attract>().BeginAttract(this.gameObject,0.0f,0.1f);
				}

				other.GetComponent<Attract>().BeginAttract(this.gameObject,0.0f,0.1f);
			}
		}
	}
}
