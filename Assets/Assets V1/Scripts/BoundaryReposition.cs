using UnityEngine;
using System.Collections;

public class BoundaryReposition : MonoBehaviour 
{
	void OnTriggerExit2D(Collider2D collision) 
	{
		if (collision.gameObject.tag == "green" || collision.gameObject.tag == "red") 
		{
			if(!collision.gameObject.GetComponent<NodeElement>().isAttract())
			{
				collision.gameObject.GetComponent<NodeElement>().DecreaseElement(60f,Vector3.zero);
			}
		}
	}
}
