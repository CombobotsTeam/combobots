using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLevelText : MonoBehaviour {
	private int level = 1;
	private string LevelText = null;
	void Start () {
		
	}
	public void next()
	{
		if (level < 6)
			level++;
	}
	public void back()
	{
		if (level > 1)
			level--;
	}
	void Update () {
		LevelText = level.ToString ();
		GetComponent<Text> ().text = LevelText;
	}
}
