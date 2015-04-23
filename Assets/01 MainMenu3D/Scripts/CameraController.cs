using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Animator cameraAnimator;
	public Animator tituloAnimator;

	public GameObject[] destroyGameobjects;
	private string sceneName;

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

	public void InToScren(string sceneName)
	{
		cameraAnimator.SetTrigger("InToScren");
		this.sceneName = sceneName;
	}

	public void InToAbout(bool isAbout)
	{
		cameraAnimator.SetBool("InToAbout",isAbout);
	}

	void OnInToScreenFinish()
	{
		Application.LoadLevel (sceneName);
	}
}
