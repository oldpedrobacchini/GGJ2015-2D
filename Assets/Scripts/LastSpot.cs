using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LastSpot : MonoBehaviour {

	public int limiteX = 24;
	public int limiteY = 12;

	public int numRedNode = 10;
	public int numGreenNode = 10;

	public GameObject redNode;
	public GameObject greenNode;

	public GameObject player1;
	public GameObject player2;

	public CanvasRenderer UICountdown;
	public CanvasRenderer UIPoints;

	List<Vector2> avaliablePositons = new List<Vector2>();

//	private Game game = null;

	void Start()
	{
		Game game = FindObjectOfType<Game>();
		if(game==null)
			game = ((GameObject) Instantiate(Resources.Load("GamePrefab", typeof(GameObject)))).GetComponent<Game>();

//		Debug.Log (game.name);

		for (int i=limiteX; i>=-limiteX; i--) {
			for (int j=limiteY; j>=-limiteY; j--) {
				avaliablePositons.Add(new Vector2(i,j));
			}
		}
		gameObject.transform.position = getAvaliablePosition (gameObject.transform.localScale.x);

		player1.transform.position = getAvaliablePosition (10.0f);
		player2.transform.position = getAvaliablePosition (10.0f);

		for (int i=0; i<numRedNode; i++) {
			GameObject node = (GameObject) Instantiate (redNode);
			node.transform.position = getAvaliablePosition(node.transform.localScale.x);
		}
		for (int i=0; i<numGreenNode; i++) {
			GameObject node = (GameObject) Instantiate (greenNode);
			node.transform.position = getAvaliablePosition(node.transform.localScale.x);
		}

//		Debug.Log (game.getPointsPlayer1 () + " x " + game.getPointsPlayer2 ());
		UIPoints.GetComponent<Text> ().text = game.getPointsPlayer1 () + " x " + game.getPointsPlayer2 ();

		StartCoroutine (getReady());
	}

	// call this function to display countdown
	IEnumerator getReady ()    
	{	
		UICountdown.GetComponentInChildren<Text> ().text = "3";
		yield return new WaitForSeconds(1.0f);
		
		UICountdown.GetComponentInChildren<Text> ().text = "2";
		yield return new WaitForSeconds(1.0f);
		
		UICountdown.GetComponentInChildren<Text> ().text = "1";
		yield return new WaitForSeconds(1.0f);
		
		UICountdown.GetComponentInChildren<Text> ().text = "GO!";    
		yield return new WaitForSeconds(0.3f);

		UICountdown.gameObject.SetActive (false);
		player1.GetComponent<MovimentPlayer> ().enabled = true;
		player2.GetComponent<MovimentPlayer> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
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

	public void Ideath(string tag)
	{
		Game game = FindObjectOfType<Game> ();
		if(tag == "Player1")
			game.addPointPlayer2();
		else if(tag == "Player2")
			game.addPointPlayer2();

		Application.LoadLevel (Application.loadedLevelName);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Game game = FindObjectOfType<Game>();
		Player p = other.transform.parent.gameObject.GetComponent<Player> ();
		if (p.getNumNodes () == 3) {

			if(p.tag == "Player1")
				game.addPointPlayer1();
			else if(p.tag == "Player2")
				game.addPointPlayer2();

			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
