﻿using UnityEngine;
using System.Collections;

public class MovimentPlayer : MonoBehaviour 
{
	public float velocity = 20f;
	
	public string controlVertical = "VerticalP1";
	public string controlHorizontal = "HorizontalP1";

	Vector2 speedMagnetic;

	// Update is called once per frame	
	void Update () 
	{
		Vector3 newPosition = transform.position;
		/*
		if (Input.GetButton (controlHorizontal) && Input.GetButton (controlVertical)) 
		{
			newPosition.x = InputControl(controlHorizontal,transform.position.x);

			newPosition.y = InputControl(controlVertical,transform.position.y);

			gameObject.transform.position = newPosition;
		}
		else */
		if (Input.GetButton (controlHorizontal)) 
		{
			newPosition.x = InputControl(controlHorizontal,transform.position.x);

			transform.position = newPosition;
		}
		//else 
		if (Input.GetButton (controlVertical)) 
		{
			newPosition.y = InputControl(controlVertical,transform.position.y);

			transform.position = newPosition;
		}

		Vector2 touchPos = Vector2.zero;




		if (Input.GetMouseButton(0) && controlVertical == "VerticalP1")
		{
			touchPos = Input.mousePosition;

			newPosition = Camera.main.ScreenToWorldPoint(touchPos);

			float newPositionX = Mathf.SmoothDamp(gameObject.transform.position.x, newPosition.x,ref speedMagnetic.x, 0.2f);
			float newPositionY = Mathf.SmoothDamp(gameObject.transform.position.y, newPosition.y,ref speedMagnetic.y, 0.2f);	

			transform.position = new Vector3(newPositionX,newPositionY,transform.position.z);
		} 

//		Touch touch = Input.GetTouch (0);
//		Debug.Log (touch);
	}

	public float InputControl(string Control,float originalPosition)
	{
		if (Input.GetAxis (Control) > 0) 
		{
			originalPosition += Time.deltaTime * velocity;
		} 
		else if (Input.GetAxis (Control) < 0) 
		{
			originalPosition -= Time.deltaTime * velocity;
		}
		return originalPosition;
	}
}
