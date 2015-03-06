using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour 
{
	Vector3 initialPosition;
	bool isUp = true;

	public float range = 1.0f;
	public float speed = 1;
	public bool isRandom;

	void Start()
	{
		if (isRandom) 
		{
			range = Random.Range (1.0f, 1.5f);
			speed = Random.Range (1.0f, 1.5f);
		}

		BeginUpDown ();
	}

	public void BeginUpDown()
	{
		this.enabled = true;
		initialPosition = transform.position;
	}

	public void StopUpDown()
	{
		this.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (isUp) 
		{
			if (transform.position.y<initialPosition.y+range)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y+speed*Time.deltaTime,transform.position.z);
			}
			else
			{
				isUp = false;
			}
		}
		else
		{
			if (transform.position.y>initialPosition.y)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y-speed*Time.deltaTime,transform.position.z);
			}
			else
			{
				isUp = true;
			}
		}
	}
}
