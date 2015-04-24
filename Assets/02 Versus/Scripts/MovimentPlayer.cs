using UnityEngine;
using System.Collections;

public class MovimentPlayer : MonoBehaviour 
{
	public AudioSource audioSource;

	public float speed = 20f;
	
	public string controlVertical = "VerticalP1";
	public string controlHorizontal = "HorizontalP1";

	Vector2 speedMagnetic;

	// Update is called once per frame	
	void Update () 
	{
		Vector3 newPosition = transform.position;

		if (Input.GetButton (controlHorizontal)) 
		{
			newPosition.x = InputControl(controlHorizontal,transform.position.x);

			transform.position = newPosition;
		}
		if (Input.GetButton (controlVertical)) 
		{
			newPosition.y = InputControl(controlVertical,transform.position.y);

			transform.position = newPosition;
		}

		if ((Input.GetMouseButton(0) || Input.touchCount > 0) && controlVertical == "VerticalP1")
		{
			Vector2 touchPos = Vector2.zero;

			if(Input.GetMouseButton(0))
			{
				touchPos = Input.mousePosition;
			}

			if(Input.touchCount > 0)
			{
				touchPos = Input.GetTouch(0).position;
			}

			newPosition = Camera.main.ScreenToWorldPoint(touchPos);

//			Debug.Log(Vector3.Distance(gameObject.transform.position,newPosition));

			if(Vector3.Distance(gameObject.transform.position,newPosition) > 10.1)
			{
				if(!audioSource.isPlaying)
					audioSource.Play ();
			}
			else
			{
				audioSource.Stop();
			}

			float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, newPosition.x,ref speedMagnetic.x, 0.2f);
			float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, newPosition.y,ref speedMagnetic.y, 0.2f);	

			transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
		}
		else
		{
			audioSource.Stop();
		}
	}

	public float InputControl(string Control,float originalPosition)
	{
		if (Input.GetAxis (Control) > 0) 
		{
			originalPosition += Time.deltaTime * speed;
		} 
		else if (Input.GetAxis (Control) < 0) 
		{
			originalPosition -= Time.deltaTime * speed;
		}
		return originalPosition;
	}
}
