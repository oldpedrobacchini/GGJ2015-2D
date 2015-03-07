using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;
	public GameObject lastSignalPrefab;
	public UpDown[] moviments;

	public void StopBlackHole()
	{
		foreach (UpDown moviment in moviments)
			moviment.StopUpDown ();
	}

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
					greenNode.GetComponent<IncreaseDecrease>().BeginDecrease(100f);
				}

				other.GetComponent<Attract>().BeginAttract(this.gameObject,0.0f,0.1f);
				other.GetComponent<IncreaseDecrease>().BeginDecrease(10f);
			}
		}
	}
}
