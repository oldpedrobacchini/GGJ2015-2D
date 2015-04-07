using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchPoints : MonoBehaviour 
{
	public Match match;
	private Image image;
	private Animator animator;

	void Start()
	{
		image = GetComponent<Image> ();
		animator = GetComponent<Animator> ();
	}

	public void UpdatePoints(int points)
	{
		image.sprite = match.HUDSprites [points];

		if(points == Game.MAX_NUMBER_GREEN)
		{
			animator.enabled = true;
		}
		else
		{
			animator.enabled = false;
		}
	}
}
