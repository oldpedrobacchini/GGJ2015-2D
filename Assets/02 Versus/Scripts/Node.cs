using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
	//---------- Attract
	protected void BeginAttract(GameObject target, float minDistanceAttract, float attractSmoothTime)
	{
		BehaviorAttract att = gameObject.AddComponent<BehaviorAttract> ();
		att.BeginAttract (target, minDistanceAttract, attractSmoothTime);
	}

	protected void StopAttract()
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
	protected void Increase(float scaleFator, Vector3 majorScale)
	{
		if(GetComponent<BehaviorIncreaseDecrease>() == null)
		{
			BehaviorIncreaseDecrease incDec = gameObject.AddComponent<BehaviorIncreaseDecrease> ();
			incDec.Increase (scaleFator, majorScale);

			incDec.onFinish = delegate() {
				gameObject.GetComponent<Collider2D>().enabled = true;
			};
		}
		else
		{
			Debug.LogError("Error Increase() failed because BehaviorIncreaseDecrease exist in Node");
		}
	}

	protected void Decrease(float scaleFator,Vector3 lessScale)
	{
		if(GetComponent<BehaviorIncreaseDecrease>() == null)
		{
			BehaviorIncreaseDecrease incDec = gameObject.AddComponent<BehaviorIncreaseDecrease> ();
			incDec.Decrease (scaleFator, lessScale);
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
		else
		{
			Debug.LogError("Error Decrease() failed because BehaviorIncreaseDecrease exist in Node");
		}
	}
}
