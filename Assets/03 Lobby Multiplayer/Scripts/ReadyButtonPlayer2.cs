using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkView))]
public class ReadyButtonPlayer2 : MonoBehaviour 
{
	public enum state {ready,cancel};

	state myState = state.ready;
	string LabelReady;
	NetworkView _networkView;

	public Text LabelButton;
	public string LabelCancel;
	public ReadyButtonPlayer1 readyButtonPlayer1; 

	void Start()
	{
		LabelReady = LabelButton.text;
		_networkView = GetComponent<NetworkView> ();
	}

	public state getMyState()
	{
		return myState;
	}

	public void OnClick()
	{
		if (myState == state.ready) 
		{
			myState = state.cancel;
			_networkView.RPC ("ClickNetwork", RPCMode.AllBuffered, 1);
		}
		else if(myState == state.cancel)
		{
			myState = state.ready;
			_networkView.RPC ("ClickNetwork", RPCMode.AllBuffered, 2);
		}
	}

	[RPC]
	void ClickNetwork(int currentState)
	{
		if(currentState == 1)
		{
			LabelButton.text = LabelCancel;
			
			if(GetComponent<Button>().enabled)
			{
				readyButtonPlayer1.SetActive(true);
			}
			
		}
		else if(currentState == 2)
		{
			LabelButton.text = LabelReady;
			
			if(GetComponent<Button>().enabled)
			{
				readyButtonPlayer1.SetActive(false);
			}
		}
	}
}
