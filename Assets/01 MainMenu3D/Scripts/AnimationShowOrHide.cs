using UnityEngine;
using System.Collections;

public class AnimationShowOrHide : MonoBehaviour 
{
	public Animator animator;

	public void Show ()
	{
		animator.SetBool ("isShow", true);
	}

	public void Hide()
	{
		animator.SetBool ("isShow", false);
	}
}
