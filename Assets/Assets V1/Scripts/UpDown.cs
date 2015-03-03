using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour {

	public float range = 1.0f;
	public float velocity = 1;
	Vector3 initialPosition;
	bool isUp = true;

	void Start()
	{
		BeginUpDown ();
	}

	public void BeginUpDown()
	{
		this.enabled = true;
		initialPosition = transform.position;
		range = Random.Range (1.0f, 1.5f);
		velocity = Random.Range (1.0f, 1.5f);
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
				transform.position = new Vector3(transform.position.x,transform.position.y+velocity*Time.deltaTime,transform.position.z);
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
				transform.position = new Vector3(transform.position.x,transform.position.y-velocity*Time.deltaTime,transform.position.z);
			}
			else
			{
				isUp = true;
			}
		}
	}
}
