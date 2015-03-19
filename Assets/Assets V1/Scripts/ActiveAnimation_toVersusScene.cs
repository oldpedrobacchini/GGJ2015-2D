using UnityEngine;
using System.Collections;

public class ActiveAnimation_toVersusScene : MonoBehaviour {
	public Animator cameraAnimator;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void VersusAnimation()
	{
		cameraAnimator.SetTrigger("ClicouButVersus");
	}
}
