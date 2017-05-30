using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpAndFallAnimation : MonoBehaviour {

	public float minAngle = 65;
	public float maxAngle = 115;
	public float duration = 2;

	private float time = 0;
	private TextMeshPro text;
	private float botLimit;

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<TextMeshPro> ();

		RectTransform redLineRect = GameObject.Find("RedLine").GetComponent<RectTransform>();

		Vector3[] redLineCorners = new Vector3[4];
		redLineRect.GetWorldCorners (redLineCorners);
		botLimit = redLineCorners [1].y;

		// Security for value
		if (minAngle > maxAngle) {
			minAngle = maxAngle;
			Debug.LogWarning ("minAngle was bigger than maxAngle in : " + name);
		}

		if (maxAngle < minAngle) {
			maxAngle = minAngle;
			Debug.LogWarning ("maxAngle was smaller than minAngle in : " + name);
		}

		// Security to avoid outside throwing
		Vector3 maxCorner = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		Vector3 minCorner = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));

		float midX = minCorner.x + ((maxCorner.x - minCorner.x) / 2);

		if (gameObject.transform.position.x < midX)
			maxAngle = 90;
		if (gameObject.transform.position.x > midX)
			minAngle = 90;

		// Random Angle
		float angle = Random.Range (minAngle, maxAngle);

		float x = Mathf.Cos(Mathf.Deg2Rad * angle);
		float y = Mathf.Sin(Mathf.Deg2Rad * angle);

		Vector2 force = new Vector2 (x, y);

		force *= 30000;

		GetComponent<Rigidbody2D> ().AddForce (force);


	}

	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.position.y <= botLimit)
			DestroyImmediate(gameObject);

		// Fade out animation
		time += Time.deltaTime;

        text.alpha = Mathf.Lerp(1, 0, time / duration);


        if (time / duration >= 1) {
			DestroyImmediate(gameObject);
		}
	}
}
