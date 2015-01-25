using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game : MonoBehaviour {

	public AudioClip finishLevel;

	List<Vector2> avaliablePositons = new List<Vector2>();

	public int limiteX = 24;
	public int limiteY = 12;

	private string countdown = "";   

	private bool showCountdown = false;

	public void ResetData()
	{
//		Debug.Log ("reset");
		avaliablePositons.Clear();
		for (int i=limiteX; i>=-limiteX; i--) {
			for (int j=limiteY; j>=-limiteY; j--) {
				avaliablePositons.Add(new Vector2(i,j));
			}
		}
		//Debug.Log (avaliablePositons.Count);
	}

	void Awake() {
		ResetData ();
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (getReady ());
	}
	
	// call this function to display countdown
	IEnumerator getReady ()    
	{
		showCountdown = true;    
		
		countdown = "3";    
		yield return new WaitForSeconds(1.0f);
		
		countdown = "2";    
		yield return new WaitForSeconds(1.0f);
		
		countdown = "1";    
		yield return new WaitForSeconds(1.0f);
		
		countdown = "GO";    
		yield return new WaitForSeconds(1.0f);
		
		showCountdown = false;
		countdown = "";  
	}

	public Vector3 getAvaliablePosition(float radius)
	{

//		Debug.Log (avaliable.Count);
		int newPostionAvaliable = Random.Range (0, avaliablePositons.Count - 1);
		
//		Debug.Log (newPostionAvaliable);
		Vector2 newAvaliable = avaliablePositons[newPostionAvaliable];
		
		int remove = (int) Mathf.Ceil(radius/10.0f);
		
		int startRow = ((int)newAvaliable.x - remove);
		if (startRow < -limiteX)
			startRow = -limiteX;
		
		int endRow = ((int)newAvaliable.x + remove);
		if (endRow > limiteX)
			endRow = limiteX;
		
		while(startRow<=endRow)
		{
			int startCol = ((int)newAvaliable.y - remove);
			if (startCol < -limiteY)
				startCol = -limiteY;
			
			int endCol = ((int)newAvaliable.y + remove);
			if (endCol > limiteY)
				endCol = limiteY;
			
			while(startCol<=endCol)
			{
				Vector2 delete = new Vector2(startRow,startCol);
				if(!avaliablePositons.Remove(delete))
				{
					//Debug.LogError("Erro ao remover o item central");
					//Debug.Log (remove);
					//Debug.Log (newAvaliable.x + " " + newAvaliable.y);
					//Debug.Log (startRow+","+startCol);
					//Debug.Log ("//////");
				}
				startCol++;
			}
			startRow++;
		}

		return new Vector3 (newAvaliable.x,newAvaliable.y, 0);
	}

	void OnGUI ()
	{
		if (showCountdown)
		{    
			GUI.skin.label.fontSize = 60;
			// display countdown    
			GUI.color = Color.black;  

			GUI.Label(new Rect (Screen.width / 2 - 90, Screen.height / 2, 180, 140), countdown);
		}    
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			ResetData();
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
