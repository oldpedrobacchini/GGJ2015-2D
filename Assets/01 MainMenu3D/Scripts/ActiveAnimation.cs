using UnityEngine;
using System.Collections;

public class ActiveAnimation : MonoBehaviour 
{
	public Animator cameraAnimator;

	public void VersusAnimation()
	{
		cameraAnimator.SetTrigger("JumpVersus");
	}
}
