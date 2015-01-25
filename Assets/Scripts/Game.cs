using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public AudioClip finishLevel;

	List<float> avaliableX = new List<float>();
	List<float> avaliableY = new List<float>();




	public void ResetData()
	{
		avaliableX.Clear ();
		avaliableY.Clear ();
		for (float i=-24.0f; i<=24.0f; i+=0.1f) {
			avaliableX.Add(i);
		}
		for (float i=-12.0f; i<=12.0f; i+=0.1f) {
			avaliableY.Add(i);
		}
	}

	void Awake() {
		ResetData ();
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {

	}

	public Vector3 getAvaliablePosition(float radius)
	{
//		for (int i=0; i<avaliableX.Count; i++)
//			Debug.Log (avaliableX [i]);

		return new Vector3 (getAvaliable(ref avaliableX,radius), getAvaliable(ref avaliableY,radius), 0);
	}

	float getAvaliable(ref List<float> avaliable, float radius){
//		Debug.Log (avaliable.Count);
		int newPostionAvaliable = Random.Range (0, avaliable.Count - 1);
//		Debug.Log (newPostionAvaliable);
		float newAvaliable = avaliable[newPostionAvaliable];
		
		int remove = ((int)(radius/2))+1;
		
		int startRemove = newPostionAvaliable - remove;
		if (startRemove < 0)
			startRemove = 0;
		
		int rangeRemove = remove * 2;
		if (startRemove + rangeRemove > avaliable.Count)
			rangeRemove = avaliable.Count - startRemove;
		
//		Debug.Log (avaliable.Count);
//		Debug.Log (radius);
//		Debug.Log (remove);
		
		avaliable.RemoveRange (startRemove, rangeRemove);
		
//		Debug.Log (avaliable.Count);
//		Debug.Log ("///////////////////////////////////////");
//		for (int i=0; i<avaliableX.Count; i++)
//			Debug.Log (avaliableX [i]);
		
		return newAvaliable;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			ResetData();
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
