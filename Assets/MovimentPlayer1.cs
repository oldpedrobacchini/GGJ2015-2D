using UnityEngine;
using System.Collections;

public class MovimentPlayer1 : MonoBehaviour {

	public float velocity = 20f;
	public bool isLocalPlayer = false;
	public Vector3 target = Vector3.zero;

	// Update is called once per frame	
	void Update () {

		if (isLocalPlayer) {
			Vector3 newPosition = gameObject.transform.position;

			if (Input.GetButton ("VerticalP1")) {
					if (Input.GetAxis ("VerticalP1") > 0) {
							newPosition.y += Time.deltaTime * velocity;
					} else if (Input.GetAxis ("VerticalP1") < 0) {
							newPosition.y -= Time.deltaTime * velocity;
					}
			}
			if (Input.GetButton ("HorizontalP1")) {
					if (Input.GetAxis ("HorizontalP1") > 0) {
							newPosition.x += Time.deltaTime * velocity;
					} else if (Input.GetAxis ("HorizontalP1") < 0) {
							newPosition.x -= Time.deltaTime * velocity;
					}
			}

			gameObject.transform.position = newPosition;
		} else {
			gameObject.transform.position = target;
		}
	}
}
