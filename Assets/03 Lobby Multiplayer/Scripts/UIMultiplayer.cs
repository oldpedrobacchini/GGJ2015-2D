using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMultiplayer : MonoBehaviour 
{
	public enum typeButton {MainMenu,Client, Server};

	public typeButton type_button = typeButton.MainMenu; 

	// Update is called once per frame
	void Update () 
	{
		if(type_button == typeButton.MainMenu)
		{
			if (Network.isClient || Network.isServer)
				ActiveDesativeButton(false);
			else
				ActiveDesativeButton(true);
		}
		else if(type_button == typeButton.Client)
		{
			if (Network.isClient)
				ActiveDesativeButton(true);
			else
				ActiveDesativeButton(false);
		}
		else if(type_button == typeButton.Server)
		{
			if (Network.isServer)
				ActiveDesativeButton(true);
			else
				ActiveDesativeButton(false);
		}

	}

	void ActiveDesativeButton(bool boolean)
	{
		gameObject.GetComponent<Button>().enabled = boolean;
		gameObject.GetComponent<Image>().enabled = boolean;
		gameObject.transform.GetChild (0).gameObject.SetActive (boolean);
	}
}
