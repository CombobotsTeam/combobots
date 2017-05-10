using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpStart : MonoBehaviour {

	public ScenesInfos scenesInfos;
	private bool touchScreen = true;
	private string nextScene;
	private bool nextDialog = false;
	// Use this for initialization
	void Start () {
		
	}
		
	
	// Update is called once per frame
	void Update () {
		int nbTouches = Input.touchCount;

		//a retirer pour mettre sur tel
		if (Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("CutScene");
		}

		if (nbTouches > 0 && touchScreen == true) {
			print (nbTouches + " touch(es) detected");

			for (int j = 0; j < nbTouches; j++)
				;
			touchScreen = false;
			nextDialog = true;
		} else if (nbTouches > 0)
			touchScreen = false;
		else if (nextDialog == true) {
			SceneManager.LoadScene ("CutScene");
			touchScreen = true;
			nextDialog = false;
		} else {
			touchScreen = true;
			nextDialog = false;
		}
	}

		//if (Input.GetMouseButtonDown (0)) {
		//	SceneManager.LoadScene ("CutScene");
		//}else {
			/*if (nbTouches > 0 && touchScreen == true) {
				print (nbTouches + " touch(es) detected");

				for (int j = 0; j < nbTouches; j++)
					;
				i++;
				touchScreen = false;

				SceneManager.LoadScene ("CutScene");
			} else if (nbTouches > 0)
				touchScreen = false;
			else
				touchScreen = true;
		}*/
	//}
}
