using UnityEngine;
using System.Collections;

public class NodeElement : Node
{
	public Animator anim;

	void Start()
	{
		anim.speed = Random.Range (1f, 2f);
		BeginUpDown ();
	}
	
	public void StopNodeElement()
	{
		StopUpDown ();
		anim.speed = 0;
	}

	//------ UpDown
	public void BeginUpDown()
	{
		if(gameObject.GetComponent<BehaviorUpDown>() == null)
		{
			BehaviorUpDown upDown = gameObject.AddComponent<BehaviorUpDown> ();
			upDown.randomBehavior();
		}
		else
		{
			Debug.LogError("Error BehaviorUpDown exist in NodeElement");
		}
	}
	
	public void StopUpDown()
	{
		Destroy(GetComponent<BehaviorUpDown>());
	}
}
