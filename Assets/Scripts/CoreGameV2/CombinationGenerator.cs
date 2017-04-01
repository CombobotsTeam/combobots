using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationGenerator : MonoBehaviour {

    public int MinLenght = 1;
    public int MaxLenght = 9;

    public bool RandomSize = false;
    public int FixedSize = 4;

    /* [Serializable]
     public struct Possibility
     {
         public CombinationHandler.Button Button;
         public float Percentage;

         public Possibility(CombinationHandler.Button button, int perc)
         {
             Button = button;
             Percentage = perc;
         }
     }

     public Possibility[] Possibilities = new Possibility[] {
         new Possibility(CombinationHandler.Button.BLUE, 25),
         new Possibility(CombinationHandler.Button.RED, 25),
         new Possibility(CombinationHandler.Button.GREEN, 25),
         new Possibility(CombinationHandler.Button.YELLOW, 25)
     };*/

    public List<CombinationHandler.Button> Possibilities = new List<CombinationHandler.Button> {
        CombinationHandler.Button.BLUE,
        CombinationHandler.Button.YELLOW,
        CombinationHandler.Button.GREEN,
        CombinationHandler.Button.RED
       };

    public List<CombinationHandler.Button> GetListButton()
    {
        List<CombinationHandler.Button> Result = new List<CombinationHandler.Button>();

        int Size = FixedSize;

        if (RandomSize)
            Size = UnityEngine.Random.Range(MinLenght, MaxLenght);

        for (int i = 0; i < Size; i++)
        {
            Result.Add(Possibilities[UnityEngine.Random.Range(0, Possibilities.Capacity)]);
        }
        return Result;
    }
}
