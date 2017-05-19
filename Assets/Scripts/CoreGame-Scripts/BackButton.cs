using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

	void Start () {
		
	}

	public void Back()
	{
		SceneManager.LoadScene (2);
		//Application.LoadLevel(2);
	}
}
