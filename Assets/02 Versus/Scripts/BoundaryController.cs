using UnityEngine;
using System.Collections;

public class BoundaryController : MonoBehaviour 
{
	void OnTriggerExit2D(Collider2D collision) 
	{
		if(collision.gameObject.GetComponent<NodeElement>() != null)
		{
			collision.gameObject.GetComponent<NodeElement>().ExitBoundary();
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision) 
	{
		if(collision.gameObject.GetComponent<NodeElement>() != null)
		{
			collision.gameObject.GetComponent<NodeElement>().EnterBoundary();
		}
	}
}

