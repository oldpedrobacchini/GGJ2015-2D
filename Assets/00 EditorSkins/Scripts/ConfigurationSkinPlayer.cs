using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class ConfigurationSkinPlayer : MonoBehaviour 
{
	public enum playerType {Player1, Player2};

	public playerType type;

	public Color ColorPlayer1;
	public Color[] ColorsTrailRendererPlayer1 = new Color[5];

	public Color ColorPlayer2;
	public Color[] ColorsTrailRendererPlayer2 = new Color[5];

	public SpriteRenderer sprite;
	public TrailRenderer trailRenderer;

	public void UpdateSkinPlayer()
	{
		if(type == playerType.Player1)
		{
			sprite.color = ColorPlayer1;

			SerializedObject so = new SerializedObject(trailRenderer);
			so.FindProperty("m_Colors.m_Color[0]").colorValue = ColorsTrailRendererPlayer1[0];
			so.FindProperty("m_Colors.m_Color[1]").colorValue = ColorsTrailRendererPlayer1[1];
			so.FindProperty("m_Colors.m_Color[2]").colorValue = ColorsTrailRendererPlayer1[2];
			so.FindProperty("m_Colors.m_Color[3]").colorValue = ColorsTrailRendererPlayer1[3];
			so.FindProperty("m_Colors.m_Color[4]").colorValue = ColorsTrailRendererPlayer1[4];
			so.ApplyModifiedProperties();
		}
		else if(type == playerType.Player2)
		{
			sprite.color = ColorPlayer2;

			SerializedObject so = new SerializedObject(trailRenderer);
			
			so.FindProperty("m_Colors.m_Color[0]").colorValue = ColorsTrailRendererPlayer2[0];
			so.FindProperty("m_Colors.m_Color[1]").colorValue = ColorsTrailRendererPlayer2[1];
			so.FindProperty("m_Colors.m_Color[2]").colorValue = ColorsTrailRendererPlayer2[2];
			so.FindProperty("m_Colors.m_Color[3]").colorValue = ColorsTrailRendererPlayer2[3];
			so.FindProperty("m_Colors.m_Color[4]").colorValue = ColorsTrailRendererPlayer2[4];
			so.ApplyModifiedProperties();
		}
	}

	public void UpdateSkinPlayer(playerType typePlayer)
	{
		type = typePlayer;
		UpdateSkinPlayer ();
	}
}
