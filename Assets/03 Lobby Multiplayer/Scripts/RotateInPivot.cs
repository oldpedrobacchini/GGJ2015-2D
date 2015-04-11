using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateInPivot : MonoBehaviour 
{
	public float rotSpeed = 300.0f;

	void Update () 
	{
		transform.Rotate (Vector3.back, rotSpeed * Time.deltaTime);
	}
}
