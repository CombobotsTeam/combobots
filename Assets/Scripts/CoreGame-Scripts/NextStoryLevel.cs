using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStoryLevel : MonoBehaviour {
	public StoryLevelText ST;

	public void OnClick()
	{
		ST.next ();
	}
	void Start () {
		
	}
	void Update () {
		
	}
}
