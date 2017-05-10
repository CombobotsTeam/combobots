using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiMenuUI : MonoBehaviour {

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE objects of Title text
    public GUIAnimFREE m_VersusButton;
    public GUIAnimFREE m_CoopButton;
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

        m_VersusButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_CoopButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_BackButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideMainButtonObjects()
    {
        yield return new WaitForSeconds(0.05f);

        // MoveOut
        m_VersusButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_CoopButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
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
    public void On_Versus()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("prototype_versus_mode", 1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_Coop()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        //GUIAnimSystemFREE.Instance.LoadLevel("CoopLevel", 1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_ButtonBack()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("MainMenu", 1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion
}
