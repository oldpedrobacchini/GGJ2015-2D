using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ConfigurationSkinPlayer))]
public class ConfigurationSkinPlayerEditor : Editor 
{
	public SerializedProperty sprite;
	public SerializedProperty trailRenderer;

	void OnEnable()
	{
		sprite = serializedObject.FindProperty ("sprite");
		trailRenderer = serializedObject.FindProperty("trailRenderer");
	}

	public override void OnInspectorGUI()
	{
		ConfigurationSkinPlayer myTarget = (ConfigurationSkinPlayer)target;

		myTarget.type = (ConfigurationSkinPlayer.playerType)EditorGUILayout.EnumPopup ("Type", myTarget.type);

		if(myTarget.type == ConfigurationSkinPlayer.playerType.Player1)
		{
			myTarget.ColorPlayer1 = EditorGUILayout.ColorField("Color",myTarget.ColorPlayer1);

			for(int i=0; i<myTarget.ColorsTrailRendererPlayer1.Length;i++)
			{
				myTarget.ColorsTrailRendererPlayer1[i] = EditorGUILayout.ColorField("Color["+i+"]",myTarget.ColorsTrailRendererPlayer1[i]);
			}
		}
		else if(myTarget.type == ConfigurationSkinPlayer.playerType.Player2)
		{
			myTarget.ColorPlayer2 = EditorGUILayout.ColorField("Color",myTarget.ColorPlayer2);
			
			for(int i=0; i<myTarget.ColorsTrailRendererPlayer2.Length;i++)
			{
				myTarget.ColorsTrailRendererPlayer2[i] = EditorGUILayout.ColorField("Color["+i+"]",myTarget.ColorsTrailRendererPlayer2[i]);
			}
		}

		bool allowSceneObjects = !EditorUtility.IsPersistent(target);

		sprite.objectReferenceValue = EditorGUILayout.ObjectField ("Sprite",sprite.objectReferenceValue, typeof(SpriteRenderer),allowSceneObjects);
		trailRenderer.objectReferenceValue = EditorGUILayout.ObjectField ("Trail Renderer",trailRenderer.objectReferenceValue, typeof(TrailRenderer),allowSceneObjects);

		if (GUI.changed)
		{
			myTarget.UpdateSkinPlayer ();
			EditorUtility.SetDirty(myTarget);
		}

		serializedObject.ApplyModifiedProperties();
	}
}