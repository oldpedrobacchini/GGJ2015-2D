using UnityEngine;
using System.Collections;

public class NodeElement : Node
{
	public Animator anim;

	private bool inBoundary = true;

	public SkinnedMeshRenderer[] meshRenders;

	public void ExitBoundary()
	{
		inBoundary = false;

		if(!isAttract())
		{
			DecreaseElement(60f,Vector3.zero);
		}
	}

	public void EnterBoundary()
	{
		inBoundary = true;
	}

	void Start()
	{
		if(anim!=null)
			anim.speed = Random.Range (1f, 2f);
		BeginUpDown ();
	}
	
	public void StopNodeElement()
	{
		if(isUpDown())
			StopUpDown ();

		anim.speed = 0;
	}

	//------ UpDown
	private void BeginUpDown()
	{
		if(gameObject.GetComponent<BehaviorUpDown>() == null)
		{
			BehaviorUpDown upDown = gameObject.AddComponent<BehaviorUpDown> ();
			upDown.randomBehavior();
		}
		else
		{
			Debug.LogError("Error BeginUpDown() failed because BehaviorUpDown exist in NodeElement");
		}
	}

	public bool isUpDown()
	{
		if(gameObject.GetComponent<BehaviorUpDown>() == null)
			return false;
		else
			return true;
	}
	
	private void StopUpDown()
	{
		if(gameObject.GetComponent<BehaviorUpDown>() != null)
		{
			Destroy(GetComponent<BehaviorUpDown>());
		}
		else
		{
			Debug.LogError("Error StopUpDown() failed because BehaviorUpDown dont exist in NodeElement");
		}
	}

	public void BeginAttractElement(GameObject target, float minDistanceAttract, float attractSmoothTime)
	{
		BeginAttract (target, minDistanceAttract, attractSmoothTime);
		if(isUpDown())
			StopUpDown();
	}
	
	public void StopAttractElement()
	{
		StopAttract ();
		BeginUpDown ();

		if(!inBoundary)
			DecreaseElement(60f,Vector3.zero);
	}

	public void DecreaseElement(float scaleFator,Vector3 lessScale)
	{
		Decrease (scaleFator, lessScale);

		if(isUpDown())
			StopUpDown();

		BehaviorIncreaseDecrease incDec = gameObject.GetComponent<BehaviorIncreaseDecrease> ();

		incDec.onFinish = delegate() {
			Match match = FindObjectOfType<Match>();
			StartCoroutine(match.repositionNode((NodeElement)this));
		};
	}

	public void IncreaseElement(float scaleFator,Vector3 lessScale)
	{
		Increase (scaleFator, lessScale);
		BeginUpDown ();
	}
}
