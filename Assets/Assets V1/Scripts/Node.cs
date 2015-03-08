using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
	//---------- Attract
	public void BeginAttract(GameObject target, float minDistanceAttract, float attractSmoothTime)
	{
		BehaviorAttract att = gameObject.AddComponent<BehaviorAttract> ();
		att.BeginAttract (target, minDistanceAttract, attractSmoothTime);
	}

	public void StopAttract()
	{
		Destroy (GetComponent<BehaviorAttract> ());
	}

	public bool isAttract()
	{
		if(GetComponent<BehaviorAttract>()!=null)
			return GetComponent<BehaviorAttract>().isAttract();
		else
			return false;
	}

	//---------- IncreaseDecrease
	public void Increase(float scaleFator, Vector3 majorScale)
	{
		BehaviorIncreaseDecrease incDec = gameObject.AddComponent<BehaviorIncreaseDecrease> ();
		incDec.Increase (scaleFator, majorScale);
	}

	public void Decrease(float scaleFator,Vector3 lessScale)
	{
		BehaviorIncreaseDecrease incDec = gameObject.AddComponent<BehaviorIncreaseDecrease> ();
		incDec.Decrease (scaleFator, lessScale);
		if(tag == "red")
		{
			incDec.onFinish = delegate() {
				Match match = FindObjectOfType<Match>();
				StartCoroutine(match.newRed(this));
			};
		}
	}
}
