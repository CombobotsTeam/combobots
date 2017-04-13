using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

	void Start () {
		
	}

	public void Back()
	{
		Application.LoadLevel(2);
	}
}
