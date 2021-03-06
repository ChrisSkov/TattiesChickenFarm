﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0f;
        }
    }
}
