using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make by seonghwan
public class ButtonUI : MonoBehaviour {
	
	public bool bottom = false;
	public bool top = false;
	public int percentofsizeheight = 25;

	private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position
    private ButtonScript button;

	void Start () {
        button = GetComponent<ButtonScript>();
		GetComponent<RectTransform> ().sizeDelta = new Vector2(Screen.width / 4, Screen.height * percentofsizeheight / 100);

		if (top)
			Position.y = Screen.height - ( Screen.height * percentofsizeheight / 100);
		if (bottom)
			Position.y = 0;
		Position.x = Screen.width / 4 * (int)button.Type;
		GetComponent<RectTransform> ().position = Position; 
	}
}
