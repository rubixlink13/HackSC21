﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    private float health = 1.0f;
    private bool dangerZone = true;
    private bool stilIn = false;
    // Start is called before the first frame update
    void Start()
    {
      healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
      healthBar.value = health;
      if(health <= 0f){
        AsyncOperation operation = SceneManager.LoadSceneAsync("DeathScene", LoadSceneMode.Single);
      }
    }

    IEnumerator decHealth(){
      dangerZone = false;
      yield return new WaitForSeconds(1F);
      dangerZone = true;
      if(stilIn){
        health -= 0.1f;
      }
    }

    void OnTriggerStay(Collider collision){
      if (collision.gameObject.tag == "CovidField" && dangerZone){
        stilIn = true;
        StartCoroutine(decHealth());
      }

      if (collision.gameObject.tag == "Pill" && health != 1.0f) {
        health += 0.2f;
        if (health > 1.0f) {
          health = 1.0f;
        }
        Destroy(collision.gameObject);
      }

      
    }

    void OnTriggerExit(Collider collision){
      if (collision.gameObject.tag == "CovidField"){
        stilIn = false;
      }
    }
}
