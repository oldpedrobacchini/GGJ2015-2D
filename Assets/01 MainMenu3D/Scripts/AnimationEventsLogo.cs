using UnityEngine;
using System.Collections;

public class AnimationEventsLogo : MonoBehaviour 
{
	public CameraController cameraController;

	public void FinishAnimation () 
	{
		cameraController.enabled = true;
		Destroy (gameObject);
	}
}
