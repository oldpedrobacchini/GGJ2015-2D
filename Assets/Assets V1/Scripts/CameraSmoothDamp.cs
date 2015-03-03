using UnityEngine;
using System.Collections;

public class CameraSmoothDamp : MonoBehaviour {

	private Vector3 _to;
	float _smoothTime;
	Vector2 speedMagneticCam;
	float speedMagneticCamSize;
	float _camZoom;

	void Start()
	{
		_camZoom = camera.orthographicSize;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs(transform.position.x - _to.x) > 0.1f || Mathf.Abs(transform.position.y - _to.y) > 0.1f)
		{
			float newCamPositionX = Mathf.SmoothDamp(transform.position.x, _to.x,ref speedMagneticCam.x, _smoothTime*Time.deltaTime);
			float newCamPositionY = Mathf.SmoothDamp(transform.position.y, _to.y,ref speedMagneticCam.y, _smoothTime*Time.deltaTime);

			transform.position = new Vector3 (newCamPositionX, newCamPositionY, Camera.main.transform.position.z);
		}

		if(Mathf.Abs(camera.orthographicSize - _camZoom) > 0.1f)
		{
			camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize,_camZoom,ref speedMagneticCamSize, _smoothTime*Time.deltaTime);
		}
	}

	public void goTo(Vector3 to,float SmoothTime,float camZoom)
	{
		_to = to;
		_smoothTime = SmoothTime;
		_camZoom = camZoom;
	}
}
