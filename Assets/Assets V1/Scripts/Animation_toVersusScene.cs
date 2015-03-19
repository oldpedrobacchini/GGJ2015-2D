using UnityEngine;
using System.Collections;

public class Animation_toVersusScene : MonoBehaviour {
	public Animator cameraAnimator;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("1")){
			cameraAnimator.SetBool("ClicouVersus", true);
		}
	}
}
