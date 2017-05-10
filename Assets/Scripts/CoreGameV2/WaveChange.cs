using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveChange : MonoBehaviour {

    Text t;
    string newText;

	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void changeWave(float delay, string text)
    {
        newText = text;
        gameObject.SetActive(true);
        StartCoroutine("change", delay);
    }

    protected IEnumerator change(float delay)
    {
        t.text = newText;
        while (delay >= 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
