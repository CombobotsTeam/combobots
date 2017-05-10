﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogScript : MonoBehaviour {

	// Use this for initialization

	public UnityEngine.UI.Text textName;
	public UnityEngine.UI.Text textDialog;
	public UnityEngine.UI.Image leftSprite;
	public UnityEngine.UI.Image rightSprite;
	public UnityEngine.RectTransform leftSpriteSize;
	public UnityEngine.RectTransform rightSpriteSize;
	public UnityEngine.UI.Image background;
	private UnityEngine.UI.Image bubble;
	private GameObject gameObjectOnScene;
	private ScenesInfos scenesInfos;
	private string[,] dialogs;
	private string[,] sprites;
	private ConfigurationFile config;
	private int i = 0;
	private int j = 0;
	private bool touchScreen = true;
	private UnityEngine.Color FinnColor;
	private UnityEngine.Color WhitePawnColor;
	private UnityEngine.Color BlackPawnColor;
	private Sprite sprite;
	private bool nextDialog = false;

	void Start () {
		config = gameObject.GetComponent<ConfigurationFile>();
		dialogs = config.cutscenesDialog;
		sprites = config.cutscenesSprite;

		gameObjectOnScene = GameObject.Find ("SceneInfos");
		scenesInfos = gameObjectOnScene.GetComponent<ScenesInfos>();

		while (dialogs [i, 0] != scenesInfos.currentScene)
			i++;
		i++;
		FinnColor = new Color (51.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
		WhitePawnColor = new Color (1.0f, 1.0f, 1.0f);
		BlackPawnColor = new Color (0.0f, 0.0f, 0.0f);
		if (dialogs [i, 0] == "Finn")
			textName.color = FinnColor;
		else if (dialogs [i, 0] == "WhitePawn")
			textName.color = WhitePawnColor;
		else if (dialogs[i, 0] == "BlackPawn")
			textName.color = BlackPawnColor;
		if (dialogs [i, 0] == "") {
			textDialog.color = new Color (1.0f, 1.0f, 1.0f);
		}
		textName.text = dialogs[i, 0];
		textDialog.text = dialogs[i, 1];

		while (sprites [j, 0] != scenesInfos.currentScene) {
			j++;
		}
		if (scenesInfos.currentScene == "Intro" || scenesInfos.currentScene == "Intro_2") {
			bubble = gameObject.GetComponent<Image> ();
			bubble.enabled = false;
		}
		sprite = Resources.Load<Sprite> ("Backgrounds/" + sprites[j, 3]);
		background.sprite = sprite;
		sprite = Resources.Load<Sprite> ("Sprites/CutScenes/" + sprites[j, 1]);
		leftSprite.sprite = sprite;
		if (sprites [j, 1] == "Finn") {
			leftSpriteSize.sizeDelta = new Vector2 (294, 634);
			leftSpriteSize.anchoredPosition = new Vector3 (115, -99, 0);
		} else if (sprites [j, 1] == "WhitePawn") {
			leftSpriteSize.sizeDelta = new Vector2 (168, 274);
			leftSpriteSize.anchoredPosition = new Vector3 (115, 150, 0);
		} else if (sprites [j, 1] == "BlackPawn") {
			leftSpriteSize.sizeDelta = new Vector2 (347, 347);
			leftSpriteSize.anchoredPosition = new Vector3 (115, 100, 0);
		}
		sprite = Resources.Load<Sprite>("Sprites/CutScenes/" + sprites[j, 2]);
		rightSprite.sprite = sprite;
		if (sprites [j, 2] == "Finn") {
			rightSpriteSize.sizeDelta = new Vector2 (294, 634);
			rightSpriteSize.anchoredPosition = new Vector3 (-115, -99, 0);
		} else if (sprites [j, 2] == "WhitePawn") {
			rightSpriteSize.sizeDelta = new Vector2 (168, 274);
			rightSpriteSize.anchoredPosition = new Vector3 (-115, 150, 0);
		} else if (sprites [j, 2] == "BlackPawn") {
			rightSpriteSize.sizeDelta = new Vector2 (347, 347);
			rightSpriteSize.anchoredPosition = new Vector3 (-115, 100, 0);
		}
	}
	
	// Update is called once per frame
	void Update (){
		int nbTouches = Input.touchCount;

        if (Input.GetMouseButtonDown (0)) {
			i++;
			if (dialogs [i, 0] == "end") {
				if (scenesInfos.currentScene == "Intro") {
					scenesInfos.currentScene = "Intro_2";
					SceneManager.LoadScene ("CutScene");
				} else if (scenesInfos.currentScene == "Intro_2") {
					//SceneManager.LoadScene ("MainMenu");
				} else {
					//remplacer par le nom de la scene ou le combat a lieu
					SceneManager.LoadScene ("Game");
				}
			} else {
				if (dialogs [i, 0] == "Finn")
					textName.color = FinnColor;
				else if (dialogs [i, 0] == "WhitePawn")
					textName.color = WhitePawnColor;
				else if (dialogs[i, 0] == "BlackPawn")
					textName.color = BlackPawnColor;
				textName.text = dialogs [i, 0];
				textDialog.text = dialogs [i, 1];
			}

		} else {
		if (nbTouches > 0 && touchScreen == true) {
			print (nbTouches + " touch(es) detected");

			for (int j = 0; j < nbTouches; j++)
				;
			touchScreen = false;
			nextDialog = true;
		} else if (nbTouches > 0)
			touchScreen = false;
		else if (nextDialog == true) {
			i++;
			if (dialogs [i, 0] == "end") {
				if (scenesInfos.currentScene == "Intro") {
					scenesInfos.currentScene = "Intro_2";
					SceneManager.LoadScene ("CutScene");
				} else if (scenesInfos.currentScene == "Intro_2") {
					//SceneManager.LoadScene ("MainMenu");
				} else {
					//Remplacer par le nom de la scene ou le combat a lieu
					SceneManager.LoadScene ("Game");
				}
			} else {
				if (dialogs [i, 0] == "Finn")
					textName.color = FinnColor;
				else if (dialogs [i, 0] == "WhitePawn")
					textName.color = WhitePawnColor;
				else if (dialogs[i, 0] == "BlackPawn")
					textName.color = BlackPawnColor;
				textName.text = dialogs [i, 0];
				textDialog.text = dialogs [i, 1];
			}
			touchScreen = true;
			nextDialog = false;
		} else {
			touchScreen = true;
			nextDialog = false;
		}
		}
	}
}