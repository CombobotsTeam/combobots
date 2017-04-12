using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public CombinationHandler.Button Type;

    AudioSource audioSource;
    GameManager GM;
    Animator animator;

	// Use this for initialization
	void Start () {
        GM = GameManager.instance;
        animator = transform.GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
    public void ButtonPressed()
    {
        Debug.Log("ButtonPressed");
        GM.ButtonPressed(Type);
        animator.SetTrigger("Pressed");
        audioSource.Play();
    }
}
