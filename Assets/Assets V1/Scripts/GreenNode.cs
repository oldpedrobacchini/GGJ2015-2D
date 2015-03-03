using UnityEngine;
using System.Collections;

public class GreenNode : MonoBehaviour 
{
	GameObject _target;
	Vector2 speedMagnetic;
	float attractSmoothTime;
	float minDistanceAttract;

	public bool isAttract()
	{
		if (_target == null)
			return false;
		else
			return true;
	}

	void Start()
	{
		GetComponent<UpDown> ().range = Random.Range (1.0f, 2.0f);
		GetComponent<UpDown> ().velocity = Random.Range (1.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_target != null) 
		{
			float distanceToTarget = Vector2.Distance(_target.transform.position, gameObject.transform.position);

			if(distanceToTarget > minDistanceAttract)
			{
				float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, _target.transform.position.x,ref speedMagnetic.x, attractSmoothTime);
				float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, _target.transform.position.y,ref speedMagnetic.y, attractSmoothTime);	

				transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
			}
		}
	}

	public void UnAttract()
	{
		_target = null;
		this.GetComponent<UpDown> ().BeginUpDown ();
	}

	public void Attract(GameObject target)
	{
		_target = target;
		minDistanceAttract = Random.Range (3.0f, 5.0f);
		attractSmoothTime = Random.Range(0.1f,0.4f);
		this.GetComponent<UpDown> ().StopUpDown ();
	}
}
