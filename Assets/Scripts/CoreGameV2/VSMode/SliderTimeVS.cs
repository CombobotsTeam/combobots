using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimeVS : MonoBehaviour {

    private VSGameManager GM; //GameManerger script 
    private Slider slider;

    void Start()
    {
        GM = VSGameManager.instance;
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = GM.GetStartTime();
    }
}
