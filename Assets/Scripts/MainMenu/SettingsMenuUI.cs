using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuUI : MonoBehaviour {

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE objects of Title text
    public GUIAnimFREE m_resetDataButton;
    public GUIAnimFREE m_addGoldButton;
    public GUIAnimFREE m_addStoryButton;
    public GUIAnimFREE m_BackButton;

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
        StartCoroutine(MoveMainButtonObjects());
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
        yield return new WaitForSeconds(0.01f);

        m_resetDataButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_addGoldButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_addStoryButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_BackButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideMainButtonObjects()
    {
        yield return new WaitForSeconds(0.05f);

        // MoveOut
        m_resetDataButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_addGoldButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_addStoryButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_BackButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
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
    public void On_Reset()
    {
        SoundManager.instance.Play("BackMenu2", false);
        PersistantData.instance.DestroySave();
        PersistantData.instance.Load();
    }

    public void On_AddGold()
    {
        SoundManager.instance.Play("BackMenu2", false);
        PersistantData.instance.data.Gold += 100;
        PersistantData.instance.Save();
    }

    public void On_AddStory()
    {
        SoundManager.instance.Play("BackMenu2", false);
        PersistantData.instance.data.Story += 1;
        PersistantData.instance.Save();
    }

    public void On_ButtonBack()
    {
        SoundManager.instance.Play("BackMenu2", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("MainMenu", 1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion
}