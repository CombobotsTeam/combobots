using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial_ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public CombinationHandler.Button Type;

    AudioSource audioSource;
    Tutorial_GameManager GM;
    Animator animator;

	// Use this for initialization
	void Start () {
        GM = Tutorial_GameManager.instance;
        animator = transform.GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
    public void ButtonPressed()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ButtonPressed");
        GM.ButtonPressed(Type);
        animator.SetTrigger("Pressed");
        audioSource.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
