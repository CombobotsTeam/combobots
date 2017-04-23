using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryStartButton : MonoBehaviour {

	void Start () {
		
	}
	public void Onclick()
	{
		SceneManager.LoadScene (1);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
