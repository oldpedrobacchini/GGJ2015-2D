using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Match : MonoBehaviour 
{
	public bool isDebug = false;
	public GameObject debugPrefab;

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

	bool isFinish = false;

	public AudioClip CountdownEffect;
	public AudioClip perfectEffect;

	public GameObject redSignalPrefab;

	private Game game = null;
	
	void Awake()
	{
		//Busca o GameObject Game
		game = FindObjectOfType<Game>();

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
		BlackHole.transform.position = getAvaliablePosition (30f,30f);
		
		//Seta a posicao do Jogador 1
		player1.transform.position = getAvaliablePosition (30f,30f);
		//Seta a posicao do Jogador 2
		player2.transform.position = getAvaliablePosition (30f,30f);
		
		//Cria os nos vermelhos
		for (int i=0; i<numRedNode; i++) 
			Instantiate (redNodePrefab,getAvaliablePosition(10f,10f),Quaternion.identity);
		
		//Cria os nos verdes
		for (int i=0; i<numGreenNode; i++) 
			Instantiate (greenNodePrefab,getAvaliablePosition(10f,10f),Quaternion.identity);

		if(isDebug)
		{
			foreach(Vector2 avaliablePositon in avaliablePositons)
			{
				Vector3 newPosition = new Vector3(avaliablePositon.x,avaliablePositon.y,0);
				Instantiate(debugPrefab,newPosition,Quaternion.identity);
			}
        }
		
		//Seta o valor dos pontos atuais dos jogadores
		UIPointsP1.GetComponent<Text> ().text = game.getPointsPlayer1 ();
		UIPointsP2.GetComponent<Text> ().text = game.getPointsPlayer2 ();
		
		StartCoroutine (getReady());
	}
	
	// Chamar essa funcao para mostrar a contagem regressiva no comeco do jogo
	IEnumerator getReady ()    
	{	
		GetComponent<AudioSource>().clip = CountdownEffect;

		UICountdown.GetComponentInChildren<Text> ().text = "3";
		GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds(1.0f);
		
		UICountdown.GetComponentInChildren<Text> ().text = "2";
		GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds(1.0f);
		
		UICountdown.GetComponentInChildren<Text> ().text = "1";
		GetComponent<AudioSource>().Play ();
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
	
	public Vector3 getAvaliablePosition(float width,float height)
	{
		
		//Debug.Log (avaliable.Count);
		int newPostionAvaliable = Random.Range (0, avaliablePositons.Count - 1);
		
		//Debug.Log (newPostionAvaliable);
		Vector2 newAvaliable = avaliablePositons[newPostionAvaliable];
		
		int removeX = (int) Mathf.Ceil (width / 10f);
		int removeY = (int) Mathf.Ceil (height / 10f);
		
		int startRow = ((int)newAvaliable.x - removeX);
		if (startRow < -limiteX)
			startRow = -limiteX;
		
		int endRow = ((int)newAvaliable.x + removeX);
		if (endRow > limiteX)
			endRow = limiteX;
		
		while(startRow<=endRow)
		{
			int startCol = ((int)newAvaliable.y - removeY);
			if (startCol < -limiteY)
				startCol = -limiteY;
			
			int endCol = ((int)newAvaliable.y + removeY);
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
		if(tag == "Player1")
		{
			game.addPointPlayer2();
			StartCoroutine (finishLevel("Player2"));
			winPlayer2.transform.position = new Vector3(player2.transform.position.x+10f,player2.transform.position.y,player2.transform.position.z);                       
			winPlayer2.gameObject.SetActive (true);
		}
		else if(tag == "Player2")
		{
			game.addPointPlayer1();
			StartCoroutine (finishLevel("Player1"));
			winPlayer1.transform.position = new Vector3(player1.transform.position.x-10f,player1.transform.position.y,player1.transform.position.z);                       
			winPlayer1.gameObject.SetActive (true);
		}
		BlackHole.GetComponent<BlackHole>().StopBlackHole();
	}

	public void finishMatch(string player)
	{
		if (player == "Player1") 
		{
			game.addPointPlayer1 ();
			winPlayer1.gameObject.SetActive (true);
			//Perfect
			if(player2.GetComponent<NodePlayer>().getNumNodes() == 0)
			{
				GetComponent<AudioSource>().clip = perfectEffect;
				GetComponent<AudioSource>().Play ();
				winPlayer1.GetComponentInChildren<Text> ().text = "PERFECT";
				winPlayer1.GetComponentInChildren<Text> ().fontSize = 66;
			}
		} else if (player == "Player2") 
		{
			game.addPointPlayer2 ();
			winPlayer2.gameObject.SetActive (true);
			//Perfect
			if(player1.GetComponent<NodePlayer>().getNumNodes() == 0)
			{
				GetComponent<AudioSource>().clip = perfectEffect;
				GetComponent<AudioSource>().Play ();
				winPlayer2.GetComponentInChildren<Text> ().text = "PERFECT";
				winPlayer2.GetComponentInChildren<Text> ().fontSize = 66;
			}
		}
		StartCoroutine (finishLevel("BlackHole"));
	}

	IEnumerator finishLevel(string tag)
	{
		isFinish = true;
		//BlackHole Attract
		player1.GetComponent<MovimentPlayer> ().enabled = false;
		player2.GetComponent<MovimentPlayer> ().enabled = false;

		foreach (NodeElement Node in GameObject.FindObjectsOfType<NodeElement>()) 
			Node.StopNodeElement ();
		
		if (tag == "Player1") 
		{
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player1.transform.position, cameraSmoothTime, 11);
		}
		else if(tag == "Player2")
		{
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(player2.transform.position, cameraSmoothTime, 11);
		}
		else if(tag == "BlackHole")
		{
			Camera.main.GetComponent<CameraSmoothDamp>().goTo(BlackHole.transform.position,cameraSmoothTime, 11);
		}
		
		yield return new WaitForSeconds (2.0f);
		
		Application.LoadLevel(Application.loadedLevel);
	}

	public IEnumerator repositionRed(NodeElement redNode)
	{
		yield return new WaitForSeconds (2.0f);
		//Todo arrumar bug dos objetos continuarem a serem criados mesmo depois que o jogo termina
		// arrumado com essa variavel porem ver uma forma melhor
		if(!isFinish)
		{
			redNode.transform.position = getAvaliablePosition(10f,10f);

			StartCoroutine(Utility.InstantiateSignal(redSignalPrefab,redNode.gameObject));
			yield return new WaitForSeconds (0.3f);
			StartCoroutine(Utility.InstantiateSignal(redSignalPrefab,redNode.gameObject));
			yield return new WaitForSeconds (0.3f);

			redNode.Increase (10f, redNodePrefab.transform.localScale);
			redNode.BeginUpDown();
		}
	}
}
