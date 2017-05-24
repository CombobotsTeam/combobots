using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour {

	public float duration;

	private float t = 0;
	private Vector3 newPosition;
	private Vector3 p0;
	private Vector3 p1;
	private Vector3 p2;
	private GameObject GoldInfo;
	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameManager.instance;

		newPosition = new Vector3 (0,0, 1);

		GoldInfo = GameObject.Find ("Coins(Clone)");

		p0 = gameObject.transform.position;
		p2 = GoldInfo.transform.position;

		p1 = new Vector3 (p2.x, p0.y, 1);
	}

	public void Play(float delay)
	{
		StartCoroutine ("StartAnim", delay);
	}

	IEnumerator StartAnim(float delay)
	{
		yield return new WaitForSeconds(delay);

		while (t < 1)
		{
			t += Time.deltaTime / duration;

			newPosition.x = (((1 - t) * (1 - t)) * p0.x) + ((2 * t) * (1 - t) * p1.x) + ((t * t) * p2.x);
			newPosition.y = (((1 - t) * (1 - t)) * p0.y) + ((2 * t) * (1 - t) * p1.y) + ((t * t) * p2.y);

			gameObject.transform.position = newPosition;
			yield return null;
		}

		gm.AddGold (5);
		GoldInfo.GetComponent<ScaleUpAndDown> ().Play();
		DestroyImmediate(gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
