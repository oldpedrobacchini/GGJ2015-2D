using UnityEngine;
using System.Collections;

public class ButtonNextScene : MonoBehaviour {

	// Use this for initialization
	public void NextScene(string scene){
		Application.LoadLevel (scene);
	}
}
