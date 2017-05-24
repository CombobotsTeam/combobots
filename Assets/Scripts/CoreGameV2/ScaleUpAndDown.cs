using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpAndDown : MonoBehaviour {


	public float duration;
	public float scale;

	private bool isPlaying;
	private float t = 0;
	private Vector3 initScale;
	private Vector3 expectedScale;

	// Use this for initialization
	void Start ()
	{
		initScale = gameObject.transform.localScale;
		expectedScale = new Vector3 (initScale.x * scale, initScale.y * scale, 1);
		isPlaying = false;
	}

	public void	Play()
	{
		isPlaying = true;
		t = 0;
		gameObject.transform.localScale = initScale;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isPlaying == true)
		{
			t += Time.deltaTime / duration;

			if (t < 0.5)
				gameObject.transform.localScale = Vector3.Lerp (initScale, expectedScale, t * 2);
			else
				gameObject.transform.localScale = Vector3.Lerp (expectedScale, initScale, (t - 0.5f) * 2);
		}

		if (t >= 1)
			isPlaying = false;

	}
}
