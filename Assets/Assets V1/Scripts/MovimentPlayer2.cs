using UnityEngine;
using System.Collections;

public class MovimentPlayer2 : MonoBehaviour {

	public float velocity = 20f;
	
	// Update is called once per frame	
	void Update () {
		Vector3 newPosition = gameObject.transform.position;
		
		if (Input.GetButton("VerticalP2")) {
			if(Input.GetAxis("VerticalP2") > 0){
				newPosition.y += Time.deltaTime* velocity;
			}
			else if(Input.GetAxis("VerticalP2") < 0){
				newPosition.y -= Time.deltaTime*velocity;
			}
		}
		if (Input.GetButton("HorizontalP2")) {
			if(Input.GetAxis("HorizontalP2") > 0){
				newPosition.x += Time.deltaTime*velocity;
			}
			else if(Input.GetAxis("HorizontalP2") < 0){
				newPosition.x -= Time.deltaTime*velocity;
			}
		}
		
		gameObject.transform.position = newPosition;
	}
}
