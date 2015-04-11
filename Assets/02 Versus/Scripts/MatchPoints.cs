using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchPoints : MonoBehaviour 
{
	public Match match;
	private Image image;
	private Animator animator;
	private Color defaultColor;

	void Start()
	{
		image = GetComponent<Image> ();
		animator = GetComponent<Animator> ();
		defaultColor = image.color;
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
			image.color = defaultColor;
		}
	}
}
