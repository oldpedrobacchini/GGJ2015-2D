using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour 
{		
	public Match match;
	public GameObject lastSignalPrefab;
	public BehaviorUpDown[] moviments;
	public GameObject giraBlackHole1;
	public GameObject giraBlackHole2;
	public GameObject giraBlackHole3;
	public float speedGiroBlackHole1= -50f;
	public float speedGiroBlackHole2 = 50f;
	public float speedGiroBlackHole3 = 50f;

	void Update () {
		giraBlackHole1.transform.Rotate (new Vector3(0,0, Time.deltaTime*speedGiroBlackHole1));
		giraBlackHole2.transform.Rotate (new Vector3(0,0, Time.deltaTime*speedGiroBlackHole2));
		giraBlackHole3.transform.Rotate (new Vector3(0,0, Time.deltaTime*speedGiroBlackHole3));
	}

	public void StopBlackHole()
	{
		foreach (BehaviorUpDown moviment in moviments)
			Destroy (moviment);
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.GetComponent<NodePlayer>() != null) 
		{
			if (other.GetComponent<NodePlayer>().getNumNodes () == 4) 
			{
				match.PlayerWin(other.tag);
				GetComponent<Collider2D>().enabled = false;
				StartCoroutine(Utility.InstantiateSignal(lastSignalPrefab,gameObject));

				foreach(NodeElement greenNode in other.GetComponent<NodePlayer>().greenNodes)
				{
					greenNode.BeginAttractElement(this.gameObject,0.0f,0.1f);
					greenNode.DecreaseElement(100f,Vector3.zero);
				}

				other.GetComponent<NodePlayer>().BeginAttractPlayer(this.gameObject,0.0f,0.1f);
				other.GetComponent<NodePlayer>().DecreasePlayer(10f,Vector3.zero);
			}
		}
	}
}
