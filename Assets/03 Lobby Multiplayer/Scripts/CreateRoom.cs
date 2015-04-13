using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour 
{
	public NetworkManager networkManager;
	public InputField roomNameField;
	public InputField roomPortField;

	// Use this for initialization
	public void StartServer () 
	{
		networkManager.StartServer (roomNameField.text, int.Parse(roomPortField.text));
	}
}
