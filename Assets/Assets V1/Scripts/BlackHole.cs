using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;
	public GameObject lastSignalPrefab;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.GetComponent<Player> () != null) 
		{
			if (other.gameObject.GetComponent<Player> ().getNumNodes () == 4) 
			{
				match.finishMatch(other.gameObject.GetComponent<Player> ().tag);
				GetComponent<Collider2D>().enabled = false;
				StartCoroutine(Utility.InstantiateSignal(lastSignalPrefab,gameObject));
			}
		}
	}
}
