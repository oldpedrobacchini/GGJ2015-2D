using UnityEngine;
using System.Collections;

public class pressKey : MonoBehaviour 
{
	public Animator cameraAnimator;
	public Animator tituloAnimator;

	public GameObject[] desactiveGameobjects;

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			if(cameraAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "CamIntroAnima_Nomes")
			{
				cameraAnimator.SetTrigger("Jump");
				tituloAnimator.SetTrigger("Jump");

				foreach(GameObject desactiveGameobject in desactiveGameobjects)
					desactiveGameobject.SetActive(false);

				GameObject[] guis = GameObject.FindGameObjectsWithTag ("GUI");

				guis[0].GetComponent<Animator>().enabled = true;
				guis[1].GetComponent<Animator>().enabled = true;
			}
		}
	}
}
