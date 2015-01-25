using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public AudioClip finishLevel;

	int pointsPlayer1 = 0;
	int pointsPlayer2 = 0;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public void addPointPlayer1()
	{
		pointsPlayer1 ++;
	}

	public void addPointPlayer2()
	{
		pointsPlayer2 ++;
	}

	public string getPointsPlayer1()
	{
		return pointsPlayer1.ToString();
	}

	public string getPointsPlayer2()
	{
		return pointsPlayer2.ToString();
	}
}