﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRules : MonoBehaviour {

	[HideInInspector]
	public int playerHealth = 3;
	[HideInInspector]
    public int playerAttack = 1;
	public GameObject EndGameScreen;
	public Vector3 facingLeftScale;
	public Vector3 facingRightScale;
	public Camera camera;
	[HideInInspector]
	public bool timerStarter = false;
	public float timer = 5f;
	public GameObject healthBar;
	public Image bar3;
	public Image bar2;
	public Image bar1;
	[HideInInspector]
	public bool stunned = false;
	float stunTimer = 3f;
	public GameObject sword;
	public string secondaryWeapon = "Bow";
	
	void Start() {
		facingLeftScale = transform.localScale;
		facingRightScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
	void Update() {
		if (stunned == true) {
			if (stunTimer > 0) {
				stunTimer -= Time.deltaTime;
			} else {
				stunTimer = 3f;
				stunned = false;
			}
		}
		if (timerStarter == true) {
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				timer = 5f;
				timerStarter = false;
				healthBar.SetActive(false);
			}
		}
		// shows a number of hearts corresponding to the player's health
		if (healthBar.activeSelf == true) {
			switch(playerHealth) {
			case 3:
				bar3.color = new Color32(212, 0, 0, 255);
				bar2.color = new Color32(212, 0, 0, 255);
				bar1.color = new Color32(212, 0, 0, 255);
			break;

			case 2:
				bar3.color = new Color32(212, 0, 0, 0);
				bar2.color = new Color32(212, 0, 0, 255);
				bar1.color = new Color32(212, 0, 0, 255);
			break;

			case 1:
				bar3.color = new Color32(212, 0, 0, 0);
				bar2.color = new Color32(212, 0, 0, 0);
				bar1.color = new Color32(212, 0, 0, 255);
			break;

			case 0:
				bar3.color = new Color32(212, 0, 0, 0);
				bar2.color = new Color32(212, 0, 0, 0);
				bar1.color = new Color32(212, 0, 0, 0);
			break;
			}
		}
		// if the player is dead show the EndGameScreen (GAME OVER Screen)
		if (playerHealth <= 0) {
			EndGameScreen.SetActive(true);
		}
		Vector3 MousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
		//Curser on Left
        if (transform.position.x > MousePosition.x) {
			transform.localScale = facingLeftScale;
		//Curser on Right
		} else if(transform.position.x < MousePosition.x) {
			transform.localScale = facingRightScale;
		}
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name != "Medival Sword") {
			//if the player is hit start the timer
			timerStarter = true;
			timer = 5f;
			healthBar.SetActive(true);
		}
	}
	public void DisableDodgingVariable() {
		sword.GetComponent<CombatScript>().dodging = false;
	}
}
