using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//make by seonghwan
//pls set RectTransform point bottom and left
public class UISystem : MonoBehaviour {
	private float sW = Screen.width;
	private float sH = Screen.height;
	//position
	public Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position
	public bool top = false;
	public bool bottom = false;
	public bool left = false;
	public bool right = false;
	public bool widthcenter = false;
	public int percentofwidthpercent = 0;
	public int percentofheightpercent = 0;
	//size
	public bool widthfullSCreen = false;
	public bool heigthfullScreen = false;
	public int percentwidth = 0;
	public int percentheight = 0;
	public Vector2 Size = new Vector2(0.0f,0.0f);

	void Start (){
		if (widthfullSCreen)
			Size.x = sW;//get screen width
		else
			Size.x = sW * percentwidth / 100;
		if (heigthfullScreen)
			Size.y =sH;//get screen height
		else
			Size.y = sH * percentheight / 100;
		GetComponent<RectTransform> ().sizeDelta = Size;

		if (top)
			Position.y = sH - Size.y - ( sH * percentofheightpercent / 100);
		if (bottom)
			Position.y = 0 + (sH * percentofheightpercent / 100);
		if (left)
			Position.x = 0 + (sW * percentofwidthpercent /100);
		if (right)
			Position.x = sW - Size.x- (sW * percentofwidthpercent /100);
		if(widthcenter)
			Position.x = (sW -Size.x)/2;
		GetComponent<RectTransform> ().position = Position; 
	}
}
