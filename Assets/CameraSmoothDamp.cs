using UnityEngine;
using System.Collections;

public class CameraSmoothDamp : MonoBehaviour {

	private Vector3 _to;
	float _smoothTime;
	Vector2 speedMagneticCam;
	float speedMagneticCamSize;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs(transform.position.x - _to.x) > 0.1f)
		{
			float newCamPositionX = Mathf.SmoothDamp(transform.position.x, _to.x,ref speedMagneticCam.x, _smoothTime*Time.deltaTime);
			float newCamPositionY = Mathf.SmoothDamp(transform.position.y, _to.y,ref speedMagneticCam.y, _smoothTime*Time.deltaTime);

			transform.position = new Vector3 (newCamPositionX, newCamPositionY, Camera.main.transform.position.z);

			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize,11,ref speedMagneticCamSize, _smoothTime*Time.deltaTime);
		}
	}

	public void goTo(Vector3 to,float SmoothTime)
	{
		_to = to;
		_smoothTime = SmoothTime;
	}
}
