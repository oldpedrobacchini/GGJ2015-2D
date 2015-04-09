using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour 
{
	public NetworkManager networkManager;
	public InputField inputField;

	// Use this for initialization
	public void StartServer () 
	{
		networkManager.StartServer (inputField.text);
	}
}
