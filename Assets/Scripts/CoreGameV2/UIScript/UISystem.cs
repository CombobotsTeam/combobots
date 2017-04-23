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
	private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position

    public bool top = false;
	public bool bottom = false;
	public bool left = false;
	public bool right = false;
	public bool widthcenter = false;
	public float PercentWidthPosition = 0;
	public float PercentHeightPosition = 0;
	//size
	public bool WidthFullScreen = false;
	public bool HeigthFullScreen = false;
	public float WidthPercentage = 0;
	public float HeightPercentage = 0;

    public bool KeepSameRatio = false;

	private Vector2 Size = new Vector2(0.0f,0.0f);
    private bool _isInit = false;

	void Start (){
		if (WidthFullScreen)
			Size.x = sW;//get screen width
		else
			Size.x = sW * WidthPercentage / 100;
		if (HeigthFullScreen)
			Size.y =sH;//get screen height
		else
			Size.y = sH * HeightPercentage / 100;

        if (KeepSameRatio && Size.x < Size.y)
            Size.y = Size.x;
        else if (KeepSameRatio && Size.x > Size.y)
            Size.x = Size.y;

		GetComponent<RectTransform> ().sizeDelta = Size;
		if (top)
			Position.y = sH - Size.y - ( sH * PercentHeightPosition / 100) - sH / 2;
		if (bottom)
			Position.y = 0 + (sH * PercentHeightPosition / 100) - sH / 2;
		if (left)
			Position.x = 0 + (sW * PercentWidthPosition / 100) - sW / 2;
		if (right)
			Position.x = sW - Size.x- (sW * PercentWidthPosition / 100) - sW / 2;
		if(widthcenter)
			Position.x = (sW -Size.x)/2 - sW / 2;

		GetComponent<RectTransform> ().localPosition = Position;
    }

    private void Update()
    {
        if (!_isInit)
        {
            Start();
            _isInit = false;
        }
    }
}
