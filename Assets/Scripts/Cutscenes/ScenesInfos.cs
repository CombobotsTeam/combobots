using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesInfos : MonoBehaviour {

	public string currentScene;
	public static ScenesInfos scenesInfos;

	public int actualLevel;
	public int actualChapter;

	void Awake(){
		if (scenesInfos == null) {
			DontDestroyOnLoad (gameObject);
			scenesInfos = this;
		} else if (scenesInfos != this) {
			Destroy(gameObject);
		}
	}

}
