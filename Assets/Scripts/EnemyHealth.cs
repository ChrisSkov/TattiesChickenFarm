using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100f;

    [SerializeField]
    float currentHealth;

    [SerializeField] bool isDead = false;

    [SerializeField] GameObject shatterObject;

    [SerializeField] float destroyTime = 1f;

    [SerializeField] ParticleSystem bloodSpray;
    Transform particleHolder;


    bool hasSpawnedShatter = false;

    bool bloodHasPlayed = false;

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        particleHolder = transform.GetChild(1).GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        particleHolder.transform.LookAt(playerTransform);
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
            bloodHasPlayed = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(bloodSpray, particleHolder.position, particleHolder.rotation);
            Instantiate(shatterObject, transform.position, transform.rotation);
            hasSpawnedShatter = true;
            DespawnEnemy();
        }
    }

    void DespawnEnemy()
    {
        Destroy(gameObject, destroyTime);
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
