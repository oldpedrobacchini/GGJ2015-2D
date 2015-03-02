using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LastSpot : MonoBehaviour 
{	
	public int limiteX = 24;
	public int limiteY = 12;

	public int numRedNode = 10;
	public int numGreenNode = 10;

	public GameObject redNode;
	public GameObject greenNode;
	public GameObject animaScreenWinP1;
	public GameObject animaScreenWinP2;

	public GameObject player1;
	public GameObject player2;

	public CanvasRenderer UICountdown;
	public CanvasRenderer UIPointsP1;
	public CanvasRenderer UIPointsP2;

	float CameraSmoothTime = 10f;
	
	List<Vector2> avaliablePositons = new List<Vector2>();

//	private Game game = null;

	void Start()
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

		//Seta a posicao do save zone
		gameObject.transform.position = getAvaliablePosition (30);
		GetComponent<UpDown> ().setinitialPosition (gameObject.transform.position);

		//Seta a posicao do Jogador 1
		player1.transform.position = getAvaliablePosition (25.0f);
		//Seta a posicao do Jogador 2
		player2.transform.position = getAvaliablePosition (25.0f);

		//Cria os nos vermelhos
		for (int i=0; i<numRedNode; i++) 
		{
			GameObject node = (GameObject) Instantiate (redNode);
			//Debug.Log(node.transform.GetChild(0).GetComponent<);
			node.transform.position = getAvaliablePosition(node.transform.localScale.x);
		}

		//Cria os nos verdes
		for (int i=0; i<numGreenNode; i++) 
		{
			GameObject node = (GameObject) Instantiate (greenNode);
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
			StartCoroutine (finishLevel2("Player2"));
			//animaScreenWinP2.gameObject.SetActive (true);
        }
		else if(tag == "Player2")
        {
			game.addPointPlayer1();
			StartCoroutine (finishLevel2("Player1"));
			//animaScreenWinP1.gameObject.SetActive (true);
		}
	
		//Application.LoadLevel (Application.loadedLevelName);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Game game = FindObjectOfType<Game>();
		Player p = other.gameObject.GetComponent<Player> ();
		if (p.getNumNodes () == 4) 
		{
			if(p.tag == "Player1")
			{
				game.addPointPlayer1();
				collider2D.enabled = false;
				//animaScreenWinP1.gameObject.SetActive (true);
			}
			else if(p.tag == "Player2")
			{
				game.addPointPlayer2();
				collider2D.enabled = false;
				//animaScreenWinP2.gameObject.SetActive (true);
			}
			StartCoroutine (finishLevel2(p.tag));
		}
	}

	//TODO remover esse metodo
	IEnumerator finishLevel(string tag)
	{
		player1.GetComponent<MovimentPlayer>().enabled = false;
		player2.GetComponent<MovimentPlayer>().enabled = false;

		UICountdown.transform.GetChild (0).gameObject.SetActive (false);
		UICountdown.gameObject.SetActive (true); //Desliga a tela que pisca
		int numeroPiscadas = 4;
		float tempoEntrePiscadas = 0.2f;

		for (int i=0; i<numeroPiscadas; i++) {
			yield return new WaitForSeconds (tempoEntrePiscadas);

			if(tag == "Player1")
				UICountdown.GetComponent<Image> ().color = new Color ((167f / 255f), 0 , 1f, 0.5f);
				
			else if (tag == "Player2")
				UICountdown.GetComponent<Image> ().color = new Color (0, 1, (23f / 255f), 0.5f);

			yield return new WaitForSeconds (tempoEntrePiscadas);
			UICountdown.GetComponent<Image> ().color = new Color (0, 0, 0, 0.25f);
		}
		Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator finishLevel2(string tag)
	{
		//BlackHole Attract
		player1.GetComponent<MovimentPlayer> ().enabled = false;
		player2.GetComponent<MovimentPlayer> ().enabled = false;

		if (tag == "Player1") 
		{
			animaScreenWinP1.gameObject.SetActive (true);
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player1.transform.position, CameraSmoothTime);
		}
		else if(tag == "Player2")
		{
			animaScreenWinP2.gameObject.SetActive (true);
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player2.transform.position, CameraSmoothTime);
		}

		yield return new WaitForSeconds (2.0f);

		Application.LoadLevel(Application.loadedLevel);
	}
}
