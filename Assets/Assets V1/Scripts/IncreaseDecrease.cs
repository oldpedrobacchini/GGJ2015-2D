using UnityEngine;
using System.Collections;

public class IncreaseDecrease : MonoBehaviour 
{
	Vector3 initialScale;
	float _scaleFator;

	// Use this for initialization
	void Start () 
	{
		initialScale = transform.localScale;
	}

	void Update()
	{
		Vector3 newScale = new Vector3 (transform.localScale.x + _scaleFator *Time.deltaTime, 
		                                transform.localScale.y + _scaleFator *Time.deltaTime, 
		                                transform.localScale.z + _scaleFator *Time.deltaTime);

		if (newScale.x < 0 || newScale.y < 0 || newScale.z < 0 ) 
		{
			newScale = Vector3.zero;
			this.enabled = false;
		}

		transform.localScale = newScale;
	}

	public void BeginIncrease()
	{
		this.enabled = true;
	}


	public void BeginDecrease(float scaleFator)
	{
		this.enabled = true;
		_scaleFator = -1*scaleFator;
	}
}
