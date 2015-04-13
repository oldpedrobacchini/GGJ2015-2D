using UnityEngine;
using System.Collections;

public class MovimentPlayer : MonoBehaviour 
{
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

			float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, newPosition.x,ref speedMagnetic.x, 0.2f);
			float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, newPosition.y,ref speedMagnetic.y, 0.2f);	

			transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
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
