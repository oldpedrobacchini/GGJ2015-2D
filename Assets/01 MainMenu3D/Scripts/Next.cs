using UnityEngine;
using System.Collections;

public class Next : MonoBehaviour 
{
	public void Scene(string name)
	{
		Application.LoadLevel (name);
	}
}
