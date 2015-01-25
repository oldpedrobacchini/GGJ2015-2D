using UnityEngine;
using System.Collections;


public class Node : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("trigger");
		if (other.gameObject.tag == "yellow" || other.gameObject.tag == "red") {

			Player player = gameObject.GetComponentInParent<Player> ();
			
			//		Debug.Log (other.gameObject.tag+" "+gameObject.name);
			if (other.gameObject.tag == "yellow"){
//				other.gameObject.audio.Play();
				player.addNode (gameObject);
			}
			else if (other.gameObject.tag == "red")
				player.removeNode (gameObject);

			Destroy (other.gameObject);
		}
	}
}
