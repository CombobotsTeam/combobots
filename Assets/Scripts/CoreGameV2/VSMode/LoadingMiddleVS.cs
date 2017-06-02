using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMiddleVS : MonoBehaviour {

    private VSGameManager GM; //GameManerger script 
    private Image middle;

    void Start()
    {
        GM = VSGameManager.instance;
        middle = GetComponent<Image>();
    }

    void Update()
    {
        if (GM.GetStartTime() < 3F)
            middle.color = new Color32(255, 0, 0, 255);
        else
            middle.color = new Color32(0,255,0,255);
    }
}
