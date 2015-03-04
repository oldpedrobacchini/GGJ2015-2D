using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour 
{
	GameObject _target;
	Vector2 speedMagnetic;
	float _attractSmoothTime;
	float _minDistanceAttract;

	public bool isAttract()
	{
		if (_target == null)
			return false;
		else
			return true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_target != null) 
		{
			float distanceToTarget = Vector2.Distance(_target.transform.position, gameObject.transform.position);

			if(distanceToTarget > _minDistanceAttract)
			{
				float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, _target.transform.position.x,ref speedMagnetic.x, _attractSmoothTime);
				float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, _target.transform.position.y,ref speedMagnetic.y, _attractSmoothTime);	

				transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
			}
		}
	}

	public void StopAttract()
	{
		_target = null;
	}

	public void BeginAttract(GameObject target, float minDistanceAttract, float attractSmoothTime)
	{
		_target = target;

		_minDistanceAttract = minDistanceAttract;
	
		_attractSmoothTime = attractSmoothTime;
	}
}
