using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSCombinationHandler : MonoBehaviour {

    public enum Button { RED = 0, GREEN = 1, BLUE = 2, YELLOW = 3 };

    private List<Button> CurrentCombinationPlayer1 = new List<Button>();
    private List<Button> CurrentCombinationPlayer2 = new List<Button>();

    void Start()
    {
        CurrentCombinationPlayer1.Clear();
        CurrentCombinationPlayer2.Clear();
    }

    public void AddButtonToCombinatioPlayer1(Button button)
    {
        if (CurrentCombinationPlayer1.Count <= 10)
            CurrentCombinationPlayer1.Add(button);
        else
            ResetPlayer1();
    }

    public void AddButtonToCombinationPlayer2(Button button)
    {
        if (CurrentCombinationPlayer2.Count <= 10)
            CurrentCombinationPlayer2.Add(button);
        else
            ResetPlayer2();
    }

    public bool CompareCombinationPlayer1(List<Button> button)
    {
        int i = 0;
        foreach (Button CombinationHandler in CurrentCombinationPlayer1)
        {
            if (i > button.Count)
                return false;
            if (CombinationHandler != button[i])
                return false;
            i++;
        }

        return true;
    }

    public bool CompareCombinationPlayer2(List<Button> button)
    {
        int i = 1;
        foreach (Button CombinationHandler in CurrentCombinationPlayer2)
        {
            if (i > button.Count)
                return false;
            if (CombinationHandler != button[button.Count - i])
                return false;
            i++;
        }

        return true;
    }

    public bool isSameCombinationPlayer1(List<Button> button)
    {
        if (CurrentCombinationPlayer1.Count != button.Count)
            return false;
        return CompareCombinationPlayer1(button);
    }

    public bool isSameCombinationPlayer2(List<Button> button)
    {
        if (CurrentCombinationPlayer2.Count != button.Count)
            return false;
        return CompareCombinationPlayer2(button);
    }

    public void ResetPlayer1()
    {
        CurrentCombinationPlayer1.Clear();
    }

    public void ResetPlayer2()
    {
        CurrentCombinationPlayer2.Clear();
    }

    public List<Button> GetCurrentCombinationPlayer1()
    {
        return CurrentCombinationPlayer1;
    }

    public List<Button> GetCurrentCombinationPlayer2()
    {
        return CurrentCombinationPlayer2;
    }
}
