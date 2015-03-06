using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Match : MonoBehaviour 
{

	public int limiteX = 24;
	public int limiteY = 12;
	
	public int numRedNode = 10;
	public int numGreenNode = 10;
	
	public GameObject redNodePrefab;
	public GameObject greenNodePrefab;
	public GameObject winPlayer1;
	public GameObject winPlayer2;
	
	public GameObject player1;
	public GameObject player2;

	public GameObject BlackHole;
	
	public CanvasRenderer UICountdown;
	public CanvasRenderer UIPointsP1;
	public CanvasRenderer UIPointsP2;
	
	float cameraSmoothTime = 10f;
	
	List<Vector2> avaliablePositons = new List<Vector2>();
	
	//	private Game game = null;
	
	void Awake()
	{
		//Busca o GameObject Game
		Game game = FindObjectOfType<Game>();
		//Se nao encontrar cria o objeto
		if(game==null)
			game = ((GameObject) Instantiate(Resources.Load("GamePrefab", typeof(GameObject)))).GetComponent<Game>();
		
		//Cria a lista de posicoes disponiveis para posicionar os elemntos do jogo
		for (int i=limiteX; i>=-limiteX; i--) 
		{
			for (int j=limiteY; j>=-limiteY; j--) 
			{
				avaliablePositons.Add(new Vector2(i,j));
			}
		}
		
		//Seta a posicao do black hole 
		BlackHole.transform.position = getAvaliablePosition (40);
		
		//Seta a posicao do Jogador 1
		player1.transform.position = getAvaliablePosition (25.0f);
		//Seta a posicao do Jogador 2
		player2.transform.position = getAvaliablePosition (25.0f);
		
		//Cria os nos vermelhos
		for (int i=0; i<numRedNode; i++) 
		{
			GameObject node = (GameObject) Instantiate (redNodePrefab);
			node.transform.position = getAvaliablePosition(node.transform.localScale.x);
		}
		
		//Cria os nos verdes
		for (int i=0; i<numGreenNode; i++) 
		{
			GameObject node = (GameObject) Instantiate (greenNodePrefab);
			node.transform.position = getAvaliablePosition(node.transform.localScale.x);
		}
		
		//Seta o valor dos pontos atuais dos jogadores
		UIPointsP1.GetComponent<Text> ().text = game.getPointsPlayer1 ();
		UIPointsP2.GetComponent<Text> ().text = game.getPointsPlayer2 ();
		
		StartCoroutine (getReady());
	}
	
	// Chamar essa funcao para mostrar a contagem regressiva no comeco do jogo
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
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
	public Vector3 getAvaliablePosition(float radius)
	{
		
		//Debug.Log (avaliable.Count);
		int newPostionAvaliable = Random.Range (0, avaliablePositons.Count - 1);
		
		//Debug.Log (newPostionAvaliable);
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
		{
			game.addPointPlayer2();
			StartCoroutine (finishLevel("Player2"));
		}
		else if(tag == "Player2")
		{
			game.addPointPlayer1();
			StartCoroutine (finishLevel("Player1"));
		}
	}

	public void finishMatch(string player)
	{
		Game game = FindObjectOfType<Game> ();
		if (player == "Player1") 
		{
			game.addPointPlayer1 ();
		} else if (player == "Player2") 
		{
			game.addPointPlayer2 ();
		}
		StartCoroutine (finishLevel(player));
	}

	IEnumerator finishLevel(string tag)
	{
		//BlackHole Attract
		player1.GetComponent<MovimentPlayer> ().enabled = false;
		player2.GetComponent<MovimentPlayer> ().enabled = false;

//		foreach (GameObject grenNode in GameObject.FindGameObjectsWithTag ("green")) 
//		{
//			grenNode.GetComponent<UpDown>().StopUpDown();
//		}
//
//		foreach (GameObject redNode in GameObject.FindGameObjectsWithTag ("red")) 
//		{
//			redNode.GetComponent<UpDown>().StopUpDown();
//		}
		
		if (tag == "Player1") 
		{
			winPlayer1.transform.position = new Vector3(player1.transform.position.x-10f,player1.transform.position.y,player1.transform.position.z);                       
			winPlayer1.gameObject.SetActive (true);
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player1.transform.position, cameraSmoothTime, 11);
		}
		else if(tag == "Player2")
		{
			winPlayer2.transform.position = new Vector3(player2.transform.position.x+10f,player2.transform.position.y,player2.transform.position.z);                       
			winPlayer2.gameObject.SetActive (true);
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player2.transform.position, cameraSmoothTime, 11);
		}
		
		yield return new WaitForSeconds (2.0f);
		
		Application.LoadLevel(Application.loadedLevel);
	}
}
