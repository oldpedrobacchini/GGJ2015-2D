using UnityEngine;
using System.Collections;

public class AnimationEventsLogo : MonoBehaviour 
{
	public JumpApresentation jumpApresentation;

	public void FinishAnimation () 
	{
		jumpApresentation.enabled = true;
		Destroy (gameObject);
	}
}
