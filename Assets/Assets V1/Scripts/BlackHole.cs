using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.GetComponent<Player> () != null) 
		{
			if (other.gameObject.GetComponent<Player> ().getNumNodes () == 4) 
			{
				match.finishMatch(other.gameObject.GetComponent<Player> ().tag);
			}
		}
	}
}
