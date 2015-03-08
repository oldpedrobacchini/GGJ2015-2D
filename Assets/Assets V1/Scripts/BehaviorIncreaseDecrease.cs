using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorIncreaseDecrease : MonoBehaviour 
{
	float _scaleFator;

	Vector3 _lessScale;
	Vector3 _majorScale;

	bool isIncrease = false;
	bool isDecrease = false;

	public delegate void callback();
	public callback onFinish;

	void Update()
	{
		if(isIncrease || isDecrease)
		{
			Vector3 newScale = new Vector3 (transform.localScale.x + _scaleFator *Time.deltaTime, 
			                                transform.localScale.y + _scaleFator *Time.deltaTime, 
			                                transform.localScale.z + _scaleFator *Time.deltaTime);

			if(isDecrease)
			{
				if (newScale.x < _lessScale.x)
				{
					newScale = _lessScale;
					if(onFinish!=null)
						onFinish();
					Destroy(this);
				}
			}

			if(isIncrease)
			{
				if (newScale.x > _majorScale.x)
				{
					newScale = _majorScale;
					if(onFinish!=null)
						onFinish();
					Destroy(this);
				}
			}

			transform.localScale = newScale;
		}
	}

	public void Increase(float scaleFator, Vector3 majorScale)
	{
		_scaleFator = scaleFator;
		_majorScale = majorScale;
		isIncrease = true;
	}


	public void Decrease(float scaleFator,Vector3 lessScale)
	{
		_scaleFator = -1*scaleFator;
		_lessScale = lessScale;
		isDecrease = true;
	}
}
