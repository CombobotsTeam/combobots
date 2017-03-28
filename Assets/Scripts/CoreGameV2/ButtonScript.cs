using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public CombinationHandler.Button Type;


    GameManager GM;

	// Use this for initialization
	void Start () {
        GM = GameManager.instance;
	}
	
    public void ButtonPressed()
    {
        GM.ButtonPressed(Type);
    }
}
