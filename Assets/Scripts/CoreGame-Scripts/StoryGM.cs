using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by seonghwan
//no include enemy information
//just send data to story game playing scene
public class StoryGM : MonoBehaviour {
	public int SelectWave = 0;

	void Start () {
		Application.DontDestroyOnLoad (this.gameObject);
	}
	public void SetInformation(int Wave)
	{
		SelectWave = Wave;
	}
	public int GetWave()
	{
		return SelectWave;
	}

}
