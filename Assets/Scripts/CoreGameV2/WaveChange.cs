using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveChange : MonoBehaviour {

    Text 	t;
    string 	newText;

	// Use this for initialization
	void Awake ()
	{
        t = GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void changeWave(float delay, string text)
    {
        newText = text;
		gameObject.SetActive(true);

		t.text = "";

		if (delay < 1.5f)
			delay = 1.5f;

        StartCoroutine("change", delay);
    }

    protected IEnumerator change(float delay)
    {
		yield return new WaitForSeconds (0.5f);

        t.text = newText;

		yield return new WaitForSeconds (delay - 1.0f);

        gameObject.SetActive(false);
    }
}
