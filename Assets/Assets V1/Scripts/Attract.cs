using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour 
{
	GameObject _target;
	Vector2 speedMagnetic;
	float attractSmoothTime;

	void Start()
	{
		attractSmoothTime = Random.Range(0.1f,0.4f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_target != null) 
		{
			float distanceToTarget = Vector2.Distance(_target.transform.position, gameObject.transform.position);

			if(distanceToTarget > 3)
			{
				float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, _target.transform.position.x,ref speedMagnetic.x, attractSmoothTime);
				float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, _target.transform.position.y,ref speedMagnetic.y, attractSmoothTime);	

				transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
			}
		}
	}

	public void Follow(GameObject target)
	{
		_target = target;
	}
}
