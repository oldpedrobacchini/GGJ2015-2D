using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputForClick : MonoBehaviour 
{
	public KeyCode inputKey;
	public Button button;

	void Update()
	{
		if(Input.GetKeyDown(inputKey))
		{
			Debug.Log("Click "+inputKey);
			//button.onClick();
		}
	}
}
