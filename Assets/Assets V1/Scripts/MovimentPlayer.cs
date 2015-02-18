using UnityEngine;
using System.Collections;

public class MovimentPlayer : MonoBehaviour {

	public float velocity = 20f;
	
	public string controlVertical = "VerticalP1";
	public string controlHorizontal = "HorizontalP1";

	// Update is called once per frame	
	void Update () 
	{
		Vector3 newPosition = gameObject.transform.position;
		
		if (Input.GetButton (controlVertical)) {
			if (Input.GetAxis (controlVertical) > 0) {
				newPosition.y += Time.deltaTime * velocity;
			} else if (Input.GetAxis (controlVertical) < 0) {
				newPosition.y -= Time.deltaTime * velocity;
			}
		}
		if (Input.GetButton (controlHorizontal)) {
			if (Input.GetAxis (controlHorizontal) > 0) {
				newPosition.x += Time.deltaTime * velocity;
			} else if (Input.GetAxis (controlHorizontal) < 0) {
				newPosition.x -= Time.deltaTime * velocity;
			}
		}
		gameObject.transform.position = newPosition;
	}
}
