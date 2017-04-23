using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationHandler : MonoBehaviour {
    public enum Button { RED = 0, GREEN = 1, BLUE = 2, YELLOW = 3};

    List<Button> CurrentCombination = new List<Button>();

    void Start()
    {
        CurrentCombination.Clear();
    }

    public void AddButtonToCombination(Button button)
    {
        if (CurrentCombination.Count <= 10)
            CurrentCombination.Add(button);
        else
            Reset();
    }

    public bool CompareCombination(List<Button> button)
    {
        int i = 0;
        foreach (Button CombinationHandler in CurrentCombination)
        {
            if (i > button.Count)
                return false;
            if (CombinationHandler != button[i])
                return false;
            i++;
        }

        return true;
    }

    public bool isSameCombination(List<Button> button)
    {
        if (CurrentCombination.Count != button.Count)
            return false;
        return CompareCombination(button);
    }

    public void Reset()
    {
        CurrentCombination.Clear();
    }   

    public List<Button> GetCurrentCombination()
    {
        return CurrentCombination;
    }
}
