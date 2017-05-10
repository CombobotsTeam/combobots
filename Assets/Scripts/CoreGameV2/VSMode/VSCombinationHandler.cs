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

    public void AddButtonToCombinatioPlayer(Button button, int Player)
    {
        if (Player == 1)
        {
            if (CurrentCombinationPlayer1.Count <= 10)
                CurrentCombinationPlayer1.Add(button);
            else
                ResetPlayer(Player);
        }
        else
        {
            if (CurrentCombinationPlayer2.Count <= 10)
                CurrentCombinationPlayer2.Add(button);
            else
                ResetPlayer(Player);
        }

    }

    public bool CompareCombinationPlayer(List<Button> button, int Player)
    {
        List<Button> CurrentCombination = new List<Button>();
        int i = 0;
        if (Player == 1)
        {
            CurrentCombination = CurrentCombinationPlayer1;
            foreach (Button CombinationHandler in CurrentCombination)
            {
                if (i > button.Count)
                    return false;
                if (CombinationHandler != button[i])
                    return false;
                i++;
            }
        }
        else
        {
            i = button.Count - 1;
            CurrentCombination = CurrentCombinationPlayer2;
            foreach (Button CombinationHandler in CurrentCombination)
            {
                if (i < 0)
                    return false;
                if (CombinationHandler != button[i])
                    return false;
                i--;
            }
        }
        return true;
    }

    public bool IsSameCombinationPlayer(List<Button> button, int Player)
    {
        if (Player == 1)
        {
            if (CurrentCombinationPlayer1.Count != button.Count)
                return false;
        }
        else
            if (CurrentCombinationPlayer2.Count != button.Count)
                return false;
        return CompareCombinationPlayer(button, Player);
    }

    public void ResetPlayer(int Player)
    {
        if (Player == 1)
            CurrentCombinationPlayer1.Clear();
        else
            CurrentCombinationPlayer2.Clear();
    }

    public List<Button> GetCurrentCombinationPlayer(int Player)
    {
        if (Player == 1)
            return CurrentCombinationPlayer1;
        return CurrentCombinationPlayer2;
    }
}
