using UnityEngine;
using System.Collections;

public class GiroContorno : MonoBehaviour {
	//public GameObject giraContorno;
	public float speedGiro = 500f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (new Vector3(0,0, Time.deltaTime*speedGiro));
	}
}
