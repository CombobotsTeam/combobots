using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE objects of Title text
    public GUIAnimFREE m_SoloButton;
    public GUIAnimFREE m_MultiButton;
    public GUIAnimFREE m_SettingsButton;

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

        // Disable all scene switch buttons
        //GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
        SoundManager.instance.PlayerMusic("MusicMenu");
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

        m_SoloButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_MultiButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_SettingsButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);

        // MoveIn m_Dialog
        //StartCoroutine(ShowDialog());
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideMainButtonObjects()
    {
        yield return new WaitForSeconds(0.2f);

        // MoveOut
        m_SoloButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_MultiButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_SettingsButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
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
    public void On_ButtonSolo()
    {
        SoundManager.instance.Play("FurtherMenu1", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("SoloMenu", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_ButtonMulti()
    {
        SoundManager.instance.Play("FurtherMenu1", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("MultiMenu", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    public void On_ButtonSettings()
    {
        SoundManager.instance.Play("FurtherMenu1", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("MultiMenu", 1.1f);
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion
}