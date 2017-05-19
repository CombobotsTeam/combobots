using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectionMenuChapterTwoUI : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Canvas
	public Canvas m_Canvas;

	// GUIAnimFREE objects of Title text
	public GUIAnimFREE m_Lvl1;
	public GUIAnimFREE m_Lvl2;
	public GUIAnimFREE m_Lvl3;
	public GUIAnimFREE m_Lvl4;
	public GUIAnimFREE m_Lvl5;
	public GUIAnimFREE m_Lvl6;
	public GUIAnimFREE m_NextChapter;
	public GUIAnimFREE m_PreviousChapter;
	private ScenesInfos scenesInfos;

	//
	private GameObject gameObjectOnScene;

	#endregion // Variables

	#region MonoBehaviour

	void Awake()
	{
		if (enabled)
		{
			// Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	void Start()
	{

		// MoveIn m_Title1 and m_Title2
		StartCoroutine(MoveMainButtonObjects());

		gameObjectOnScene = GameObject.Find("SceneInfos");
		scenesInfos = gameObjectOnScene.GetComponent<ScenesInfos> ();

		// Disable all scene switch buttons
		//GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}

	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// MoveIn/MoveOut functions
	// ########################################

	#region MoveIn/MoveOut

	// MoveIn
	IEnumerator MoveMainButtonObjects()
	{
		yield return new WaitForSeconds(0.1f);

		m_Lvl1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl2.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl3.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl4.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl5.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl6.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_NextChapter.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		m_PreviousChapter.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);

		// MoveIn m_Dialog
		//StartCoroutine(ShowDialog());
	}

	// MoveOut m_Title1 and m_Title2
	IEnumerator HideMainButtonObjects()
	{
		yield return new WaitForSeconds(0.2f);

		// MoveOut
		m_Lvl1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl2.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl3.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl4.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl5.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_Lvl6.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_NextChapter.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
		m_PreviousChapter.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
	}

	#endregion // MoveIn/MoveOut

	// ########################################
	// Enable/Disable button functions
	// ########################################

	#region Enable/Disable buttons

	// Enable/Disable all scene switch Coroutine
	IEnumerator EnableButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// Enable all scene switch buttons
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}

	// Disable all buttons for a few seconds
	IEnumerator DisableAllButtonsForSeconds(float DisableTime)
	{
		// Disable all buttons
		GUIAnimSystemFREE.Instance.EnableAllButtons(false);

		yield return new WaitForSeconds(DisableTime);

		// Enable all buttons
		GUIAnimSystemFREE.Instance.EnableAllButtons(true);
	}

	public void HideAllGUIs()
	{
		StartCoroutine(HideMainButtonObjects());
	}

	#endregion // Enable/Disable buttons

	// ########################################
	// Actions for buttons
	// ########################################

	#region ActionsButton
	public void On_Lvl1()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		scenesInfos.currentScene = "Scene01";
		GUIAnimSystemFREE.Instance.LoadLevel("CutScene", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_Lvl2()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("prototype", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_Lvl3()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("prototype", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_Lvl4()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("prototype", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_Lvl5()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("prototype", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_Lvl6()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		scenesInfos.currentScene = "Scene02";
		GUIAnimSystemFREE.Instance.LoadLevel("CutScene", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_NextChapter()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("SelectionMenuChapterThree", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	public void On_PreviousChapter()
	{
        SoundManager.instance.Play("FurtherMenu3", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
		GUIAnimSystemFREE.Instance.LoadLevel("SelectionMenuChapterOne", 1.1f);
		gameObject.SendMessage("HideAllGUIs");
	}

	#endregion
}