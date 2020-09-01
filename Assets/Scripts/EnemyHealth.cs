﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;

    [SerializeField] bool isDead = false;

    [SerializeField] GameObject shatterObject;

    ParticleSystem bloodSpray;
    bool hasSpawnedShatter = false;
    bool bloodHasPlayed = false;

    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bloodSpray = GetComponentInChildren<ParticleSystem>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        bloodSpray.gameObject.transform.LookAt(playerTransform);
        Die();
        SpawnShatterObject();
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0f;
            isDead = true;
        }
    }

    void SpawnShatterObject()
    {
        if (isDead && !hasSpawnedShatter && !bloodHasPlayed)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Instantiate(shatterObject, transform.position, transform.rotation);
            bloodSpray.Play();
            hasSpawnedShatter = true;
            bloodHasPlayed = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print("av");
    }

    public float GetMaxHP()
    {
        return maxHealth;
    }

    public float GetCurrentHP()
    {
        return currentHealth;
    }
}
