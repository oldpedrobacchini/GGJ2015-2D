using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Node : MonoBehaviour 
{

	public List<SpriteRenderer> nucleos = new List<SpriteRenderer>();
	
	public void changeColor(Color color)
	{
		foreach(SpriteRenderer nucleo in nucleos)
		{
			nucleo.color = color;
		}
	}

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
