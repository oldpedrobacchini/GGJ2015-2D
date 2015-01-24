using UnityEngine;
using System.Collections;

public class LastSpot : MonoBehaviour {

	int numRedNode = 4;
	int numGreenNode = 4;

	public GameObject redNode;
	public GameObject greenNode;

	void Start()
	{
		Game game = FindObjectOfType<Game> ();
		if (game == null) {
			//Instantiate(p
			GameObject instance = (GameObject) Instantiate(Resources.Load<GameObject>("GamePrefab"));
			game = instance.GetComponent<Game>();
		}
		gameObject.transform.position = game.getAvaliablePosition (gameObject.transform.localScale.x);


		for (int i=0; i<numRedNode; i++) {
			GameObject node = (GameObject) Instantiate (redNode);
			node.transform.position = game.getAvaliablePosition(node.transform.localScale.x);
		}
		for (int i=0; i<numGreenNode; i++) {
			GameObject node = (GameObject) Instantiate (greenNode);
			node.transform.position = game.getAvaliablePosition(node.transform.localScale.x);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log ("trigger");
		Player p = other.transform.parent.gameObject.GetComponent<Player> ();
		if (p.getNumNodes () == 3) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
