using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesInfos : MonoBehaviour {

	public string currentScene;
	public static ScenesInfos scenesInfos;

	void Awake(){
		if (scenesInfos == null) {
			DontDestroyOnLoad (gameObject);
			scenesInfos = this;
		} else if (scenesInfos != this) {
			Destroy(gameObject);
		}
	}

}
