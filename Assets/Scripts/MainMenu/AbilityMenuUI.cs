using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilityMenuUI : MonoBehaviour
{

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE objects of Title text
    public GUIAnimFREE m_UpgradePawnButton;
    public GUIAnimFREE m_UpgradeBishopButton;
    public GUIAnimFREE m_UpgradeKnightButton;
    public GUIAnimFREE m_UpgradeRookButton;
    public GUIAnimFREE m_UpgradeQueenButton;
    public GUIAnimFREE m_UpgradeKingButton;
    public GUIAnimFREE m_BackButton;


    public GameObject[] f_Upgradebutton;
    public GameObject[] f_UseButton;

    public GameObject[] f_LvlText;

    public Text gold;

    public Color SelectedColor;
    public Color NormalColor;

    public int[] UpgradeLevel = new int[4] {250, 750, 1000, 1500};

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
        UpdateButtonStatusAll();
    }

    void UpdateButtonStatus(int index)
    {
        GameData d = PersistantData.instance.data;

        gold.text = d.Gold.ToString();

        if (d.CurrentPowerUp[index] >= 4) {
            Text t = f_Upgradebutton[index].transform.FindChild("Text").gameObject.GetComponent<Text>();
            t.text = "MAX";
        } else
        {
            Text t = f_Upgradebutton[index].transform.FindChild("Text").gameObject.GetComponent<Text>();
            t.text = UpgradeLevel[d.CurrentPowerUp[index]].ToString();
            if (d.CurrentPowerUp[index] > 0)
                f_LvlText[index].GetComponent<Text>().text = "Lvl." + d.CurrentPowerUp[index].ToString();
            else
            {
                f_LvlText[index].GetComponent<Text>().text = "Lvl. 0"; 
            }
        }

        if (d.CurrentPowerUp[index] >= 4 || UpgradeLevel[d.CurrentPowerUp[index]] > d.Gold)
        {
            f_Upgradebutton[index].GetComponent<Button>().interactable = false;
        }

        if ((int)d.PowerUpUsing == index && d.PowerUpUsing != GameData.PowerUp.NONE)
        {
            f_UseButton[index].GetComponent<Button>().interactable = false;
            Text t = f_UseButton[index].transform.FindChild("Text").gameObject.GetComponent<Text>();
            f_UseButton[index].GetComponent<Image>().color = SelectedColor;
            t.text = "Selected";
        }
        else if (d.CurrentPowerUp[index] == 0)
        {
            f_UseButton[index].GetComponent<Button>().interactable = false;
            Text e = f_UseButton[index].transform.FindChild("Text").gameObject.GetComponent<Text>();
            f_UseButton[index].GetComponent<Image>().color = NormalColor;
            e.text = "Unavailable";
        }
        else
        {
            f_UseButton[index].GetComponent<Button>().interactable = true;
            Text t = f_UseButton[index].transform.FindChild("Text").gameObject.GetComponent<Text>();
            f_UseButton[index].GetComponent<Image>().color = NormalColor;
            t.text = "Use";
        }

        //TODO Disable when story is not advanced enought
    }

    void UpdateButtonStatusAll()
    {
        for (int i = 0; i < 6; i++)
            UpdateButtonStatus(i);
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

        m_UpgradePawnButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeBishopButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeKnightButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeRookButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeQueenButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeKingButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_BackButton.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);

        GUIAnimFREE tmp;
        for (int i = 0; i < f_UseButton.Length; i++)
        {
            tmp = f_UseButton[i].GetComponent<GUIAnimFREE>();
            tmp.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        }
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideMainButtonObjects()
    {
        yield return new WaitForSeconds(0.05f);

        // MoveOut
        m_UpgradePawnButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeBishopButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeKnightButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeRookButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeQueenButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_UpgradeKingButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_BackButton.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);

        GUIAnimFREE tmp;
        for (int i = 0; i < f_UseButton.Length; i++)
        {
            tmp = f_UseButton[i].GetComponent<GUIAnimFREE>();
            tmp.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        }
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

    private void ButtonExecute(int i)
    {
        GameData d = PersistantData.instance.data;
        // Max Level
        if (d.CurrentPowerUp[i] == 4)
        {
            Debug.Log("Max level for " + i);
            return;
        }

        if (f_Upgradebutton[i].GetComponent<Button>().interactable)
        {
            d.Gold -= UpgradeLevel[d.CurrentPowerUp[i]];
            d.CurrentPowerUp[i] += 1;
            PersistantData.instance.Save();
            Debug.Log("Bought ! " + i);
        }
        else
        {
            Debug.Log("Not bought");
        }

        UpdateButtonStatusAll();
    }

    public void On_UpgradePawn()
    {
        Debug.Log("On Upgrade Pawn");
        ButtonExecute((int)GameData.PowerUp.PAWN);
    }

    public void On_UpgradeBishop()
    {
        ButtonExecute((int)GameData.PowerUp.BISHOP);
    }

    public void On_UpgradeRook()
    {
        ButtonExecute((int)GameData.PowerUp.ROOK);
    }

    public void On_UpgradeKnight()
    {
        ButtonExecute((int)GameData.PowerUp.KNIGHT);
    }

    public void On_UpgradeQueen()
    {
        ButtonExecute((int)GameData.PowerUp.QUEEN);
    }

    public void On_UpgradeKing()
    {
        ButtonExecute((int)GameData.PowerUp.KING);
    }

    private void ButtonUseExecute(int i)
    {
        GameData d = PersistantData.instance.data;

        d.PowerUpUsing = (GameData.PowerUp)i;

        PersistantData.instance.Save();
        UpdateButtonStatusAll();
    }

    public void On_UsePawn()
    {
        ButtonUseExecute((int)GameData.PowerUp.PAWN);
    }

    public void On_UseBishop()
    {
        ButtonUseExecute((int)GameData.PowerUp.BISHOP);
    }

    public void On_UseRook()
    {
        ButtonUseExecute((int)GameData.PowerUp.ROOK);
    }

    public void On_UseKnight()
    {
        ButtonUseExecute((int)GameData.PowerUp.KNIGHT);
    }

    public void On_UseQueen()
    {
        ButtonUseExecute((int)GameData.PowerUp.QUEEN);
    }

    public void On_UseKing()
    {
        ButtonUseExecute((int)GameData.PowerUp.KING);
    }

    public void On_ButtonBack()
    {
        SoundManager.instance.Play("BackMenu2", false);
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
        GUIAnimSystemFREE.Instance.LoadLevel("SoloMenu", 0.5f);
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion
}
