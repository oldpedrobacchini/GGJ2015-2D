using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyButtonPlayer2 : MonoBehaviour 
{
	public enum state {ready,cancel};

	state myState = state.ready;

	private string LabelReady;

	public Text LabelButton;
	public string LabelCancel;

	public ReadyButtonPlayer1 readyButtonPlayer1;

	void Start()
	{
		LabelReady = LabelButton.text;
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
			GetComponent<NetworkView> ().RPC ("ClickNetwork", RPCMode.AllBuffered, 1);
		}
		else if(myState == state.cancel)
		{
			myState = state.ready;
			GetComponent<NetworkView> ().RPC ("ClickNetwork", RPCMode.AllBuffered, 2);
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
