using UnityEngine;
using System.Collections;

public class JumpApresentation : MonoBehaviour 
{
	public Animator cameraAnimator;
	public Animator tituloAnimator;

	public GameObject[] destroyGameobjects;

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			if(cameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "CameraNomes")
			{
				cameraAnimator.SetTrigger("JumpMenu");
				tituloAnimator.SetTrigger("JumpMenu");

				foreach(GameObject destroyGameobject in destroyGameobjects)
					Destroy(destroyGameobject);

				GameObject[] guis = GameObject.FindGameObjectsWithTag ("GUI");

				guis[0].GetComponent<Animator>().enabled = true;
				guis[1].GetComponent<Animator>().enabled = true;
			}
		}
	}
}
