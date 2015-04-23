using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkView))]
public class ReadyButtonPlayer1 : MonoBehaviour 
{
	NetworkView _networkView;

	void Start()
	{
		_networkView = GetComponent<NetworkView> ();
	}

	public void SetActive(bool isActive)
	{
		_networkView.RPC ("SetActiveNetwork", RPCMode.AllBuffered, isActive);
	}

	[RPC]
	void SetActiveNetwork(bool isActive)
	{
		if(GetComponent<Button>().enabled)
		{
			if(isActive)
			{
				GetComponent<Button>().interactable = true;
			}
			else
			{
				GetComponent<Button>().interactable = false;
			}
		}
		else
		{
			if(isActive)
			{
				GetComponent<Image> ().color = Color.white;
			}
			else
			{
				GetComponent<Image> ().color = new Color (200f/255f, 200f/255f, 200f/255f, 128f/255f);
			}
		}
	}
}
