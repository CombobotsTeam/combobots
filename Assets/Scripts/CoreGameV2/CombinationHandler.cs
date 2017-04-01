using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationHandler : MonoBehaviour {
    public enum Button { RED, YELLOW, GREEN, BLUE };

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
            if (CombinationHandler != button[0])
                return false;
            i++;
        }

        return true;
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
