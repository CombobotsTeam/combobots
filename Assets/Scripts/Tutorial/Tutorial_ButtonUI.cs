using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make by seonghwan
public class Tutorial_ButtonUI : MonoBehaviour {
	
	public bool bottom = false;
	public bool top = false;
	public int percentofsizeheight = 25;

	private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position
    private Tutorial_ButtonScript button;

	void Start () {
        button = GetComponent<Tutorial_ButtonScript>();

		GetComponent<RectTransform> ().sizeDelta = new Vector2(Screen.width / 4, Screen.height * percentofsizeheight / 100);

        if (top)
        {
            Position.x = Screen.width / 4 * (3 - (int)button.Type) - Screen.width /4 ;
            Position.y = Screen.height - Screen.height / 2;
        }
            
        if (bottom)
        {
            Position.x = Screen.width / 4 * (int)button.Type - Screen.width / 2;
            Position.y = 0 - Screen.height / 2;
        }
            

        
		GetComponent<RectTransform> ().localPosition = Position; 
	}
}
