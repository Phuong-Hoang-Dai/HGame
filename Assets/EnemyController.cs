using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyController : MonoBehaviour
{
    public GameObject healthBar;
    public Slider sliderbar;
    public int health = 5;
    public int maxHealth = 5;
    public Animator animator;
    private void Awake()
    {
        sliderbar.maxValue = maxHealth;
        sliderbar.minValue = 0;
        sliderbar.value = health;
    }
    private void Update()
    {
        sliderbar.value = health;
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if(health <= 0)
        {
            animator.SetBool("isDeath", true);
            healthBar.SetActive(false);
        }
    }
}
