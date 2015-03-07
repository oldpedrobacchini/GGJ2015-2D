using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
	public Animator anim;

	void Start()
	{
		anim.speed = Random.Range (1f, 2f);
	}

	public void StopNode()
	{
		GetComponent<UpDown>().StopUpDown();
		anim.speed = 0;
	}
}
