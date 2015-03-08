using UnityEngine;
using System.Collections;

public class BehaviorUpDown : MonoBehaviour 
{
	Vector3 initialPosition;
	bool isUp = true;

	public float range = 1.0f;
	public float speed = 1;

	void Start()
	{
		initialPosition = transform.position;
	}

	public void randomBehavior()
	{
		range = Random.Range (0.2f, 0.8f);
		speed = Random.Range (1.0f, 1.5f);
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
			if (transform.position.y>initialPosition.y-range)
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
