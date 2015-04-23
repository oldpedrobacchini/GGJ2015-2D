using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchServerMultiplayer : MonoBehaviour 
{
	public int limiteX = 20;
	public int limiteYSup = 9;
	public int limiteYInf = 12;

	public MatchMultiplayer match;

	List<Vector2> avaliablePositons = new List<Vector2>();

	private Game game = null;

	void Start () 
	{
		//Busca o GameObject Game
		game = FindObjectOfType<Game>();
		
		//Se nao encontrar cria o objeto
		if(game==null)
			game = ((GameObject) Instantiate(Resources.Load("GamePrefab", typeof(GameObject)))).GetComponent<Game>();
		
		//Cria a lista de posicoes disponiveis para posicionar os elemntos do jogo
		for (int i=limiteX; i>=-limiteX; i--) 
		{
			for (int j=limiteYSup; j>=-limiteYInf; j--) 
			{
				avaliablePositons.Add(new Vector2(i,j));
			}
		}

		match.positionBlackHole (getAvaliablePosition (30f, 30f));
		match.player.transform.position = getAvaliablePosition (30f, 30f);
		match.positionPlayer2 (getAvaliablePosition (30f, 30f));
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
			if (startCol < -limiteYInf)
				startCol = -limiteYInf;
			
			int endCol = ((int)newAvaliable.y + removeY);
			if (endCol > limiteYSup)
				endCol = limiteYSup;
			
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
}
