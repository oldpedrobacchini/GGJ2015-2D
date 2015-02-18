using UnityEngine;
using System.Collections;

public class pressKey : MonoBehaviour {


	// Update is called once per frame
	void Update () {
//		Debug.Log ("update");
		if (Input.anyKeyDown) {
			Application.LoadLevel ("VersusScene");		
		}
	}
}
