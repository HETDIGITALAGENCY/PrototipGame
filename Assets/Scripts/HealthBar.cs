using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    
    public Slider HelthSlider;
    public float maxhealth = 100f;
    public float Health;
   // public Slider EaseHealthSlider;
  //  public float LerpSpeed = 0.05f;
        
    void Start()
    {
        Health = maxhealth;

    }
    
    void Update()
    {
        if (HelthSlider.value != Health)
        {
            HelthSlider.value = Health;
        }

      //  if (HelthSlider.value != EaseHealthSlider.value)
        //{
          //  EaseHealthSlider.value = Math.Lerp(EaseHealthSlider.value, Health, LerpSpeed);
        //}

        /*void TakeDamage(float damage)
        {
            Health -= damage;   // Bu kodun burda iþi yok take damage kodlarý damageý alacak objenin scriptine yazýlýr. Emirhan
        }*/
        
    }
}
