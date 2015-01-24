using UnityEngine;
using System.Collections;

public class LastSpot : MonoBehaviour {

	void Start()
	{
		Game game = FindObjectOfType<Game> ();
//		if (game == null) {
//			Instantiate(p
//		}
		gameObject.transform.position = game.getAvaliablePosition (gameObject.transform.localScale.x);
	}

	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log ("trigger");
		Player p = other.transform.parent.gameObject.GetComponent<Player> ();
		if (p.getNumNodes () == 3) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
