using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoloMenuUI : MonoBehaviour
{

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE objects of Title text
    public GUIAnimFREE m_StoryButton;
    public GUIAnimFREE m_ArcadeButton;
    public GUIAnimFREE m_SurvivalButton;

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

        m_StoryButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_ArcadeButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_SurvivalButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideMainButtonObjects()
    {
        yield return new WaitForSeconds(0.05f);

        // MoveOut
        m_StoryButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_ArcadeButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_SurvivalButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
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
    public void On_ButtonStory()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        //GUIAnimSystemFREE.Instance.LoadLevel("SoloMenu", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_ButtonArcade()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("prototype", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_ButtonSurvival()
    {
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        //GUIAnimSystemFREE.Instance.LoadLevel("MultiMenu", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion
}
