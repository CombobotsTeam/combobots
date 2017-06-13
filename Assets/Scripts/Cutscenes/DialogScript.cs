using System.Collections;
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
	private PersistantData persistantData;
	private string[,] dialogs;
	private string[,] sprites;
	private ConfigurationFile config;
	private int i = 0;
	private int j = 0;
	private bool touchScreen = true;
	private UnityEngine.Color FinnColor;
	private UnityEngine.Color WhitePawnColor;
	private UnityEngine.Color BlackPawnColor;
	private UnityEngine.Color BlackRookColor;
	private UnityEngine.Color BlackKnightColor;
	private UnityEngine.Color BlackBishopColor;

	private Sprite sprite;
	private bool nextDialog = false;

	void Start () {
		config = gameObject.GetComponent<ConfigurationFile>();
		dialogs = config.cutscenesDialog;
		sprites = config.cutscenesSprite;

		SoundManager.instance.PlayerMusic("Cutscene");
		gameObjectOnScene = GameObject.Find ("SceneInfos");
		scenesInfos = gameObjectOnScene.GetComponent<ScenesInfos>();

		while (dialogs [i, 0] != scenesInfos.currentScene)
			i++;
		i++;
		FinnColor = new Color (51.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
		WhitePawnColor = new Color (1.0f, 1.0f, 1.0f);
		BlackPawnColor = new Color (0.0f, 0.0f, 0.0f);
		BlackBishopColor = new Color (0.0f, 0.0f, 0.0f);
		BlackRookColor = new Color (0.0f, 0.0f, 0.0f);
		BlackKnightColor = new Color (0.0f, 0.0f, 0.0f);
		if (dialogs [i, 0] == "Finn")
			textName.color = FinnColor;
		else if (dialogs [i, 0] == "WhitePawn")
			textName.color = WhitePawnColor;
		else if (dialogs[i, 0] == "BlackPawn")
			textName.color = BlackPawnColor;
		else if (dialogs[i, 0] == "BlackBishop")
			textName.color = BlackBishopColor;
		else if (dialogs[i, 0] == "BlackRook")
			textName.color = BlackRookColor;
		else if (dialogs[i, 0] == "BlackKnight")
			textName.color = BlackKnightColor;
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
		} else if (sprites [j, 1] == "BlackBishop") {
			leftSpriteSize.sizeDelta = new Vector2 (234, 369);
			leftSpriteSize.anchoredPosition = new Vector3 (115, 100, 0);
		} else if (sprites [j, 1] == "BlackRook") {
			leftSpriteSize.sizeDelta = new Vector2 (319, 404);
			leftSpriteSize.anchoredPosition = new Vector3 (115, 100, 0);
		} else if (sprites [j, 1] == "BlackKnight") {
			leftSpriteSize.sizeDelta = new Vector2 (498, 369);
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
		} else if (sprites [j, 2] == "BlackBishop") {
			rightSpriteSize.sizeDelta = new Vector2 (234, 369);
			rightSpriteSize.anchoredPosition = new Vector3 (-115, 100, 0);
		} else if (sprites [j, 2] == "BlackRook") {
			rightSpriteSize.sizeDelta = new Vector2 (319, 404);
			rightSpriteSize.anchoredPosition = new Vector3 (-115, 100, 0);
		} else if (sprites [j, 2] == "BlackKnight") {
			rightSpriteSize.sizeDelta = new Vector2 (498, 369);
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
					SoundManager.instance.PlayerMusic("MusicMenu");
					levelUp ();
					SceneManager.LoadScene ("SelectionMenuChapterOne");
				} else {
					//remplacer par le nom de la scene ou le combat a lieu
					SceneManager.LoadScene ("prototype");
				}
			} else {
				if (dialogs [i, 0] == "Finn")
					textName.color = FinnColor;
				else if (dialogs [i, 0] == "WhitePawn")
					textName.color = WhitePawnColor;
				else if (dialogs[i, 0] == "BlackPawn")
					textName.color = BlackPawnColor;
				else if (dialogs[i, 0] == "BlackBishop")
					textName.color = BlackBishopColor;
				else if (dialogs[i, 0] == "BlackRook")
					textName.color = BlackRookColor;
				else if (dialogs[i, 0] == "BlackKnight")
					textName.color = BlackKnightColor;
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
						levelUp ();
						SoundManager.instance.PlayerMusic("MusicMenu");
					SceneManager.LoadScene ("SelectionMenuChapterOne");
				} else {
					//Remplacer par le nom de la scene ou le combat a lieu
					SceneManager.LoadScene ("prototype");
				}
			} else {
				if (dialogs [i, 0] == "Finn")
					textName.color = FinnColor;
				else if (dialogs [i, 0] == "WhitePawn")
					textName.color = WhitePawnColor;
				else if (dialogs[i, 0] == "BlackPawn")
					textName.color = BlackPawnColor;
				else if (dialogs[i, 0] == "BlackBishop")
					textName.color = BlackBishopColor;
				else if (dialogs[i, 0] == "BlackRook")
					textName.color = BlackRookColor;
				else if (dialogs[i, 0] == "BlackKnight")
					textName.color = BlackKnightColor;
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

	private void levelUp(){
		GameData d = PersistantData.instance.data;

		if (d.Story == 0) {
			d.Story = 1;
			gameObjectOnScene = GameObject.Find ("PersistantData");
			persistantData = gameObjectOnScene.GetComponent<PersistantData>();
			persistantData.Save ();
		}
	}
}