using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
	public Animator anim;

	public void StopNode()
	{
		GetComponent<UpDown>().StopUpDown();
	}
}
