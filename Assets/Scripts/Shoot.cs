using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoot : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] AudioClip[] gunSounds = new AudioClip[2];
    [SerializeField] AudioClip pumpShotgunSound = null;
    [SerializeField] AudioClip reloadSound = null;
    [Header("Gun variables")]
    [SerializeField] float maxAmmo = 2;
    [SerializeField] float currentAmmo;
    [SerializeField] float damage = 10f;
    [Header("UI objects")]
    [SerializeField] GameObject shotgunShell1;
    [SerializeField] GameObject shotgunShell2;
    AudioSource source;

    bool canShoot = true;
    ParticleSystem pellets;
    Animator anim;

    EnemyHealth enemyHealth;

    HitEnemy hitEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        currentAmmo = maxAmmo;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        pellets = GetComponentInChildren<ParticleSystem>();
        hitEnemy = pellets.GetComponent<HitEnemy>();
        hitEnemy.SetDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        FireShot();
        Reload();
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetTrigger("slash");
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("reload");
        }
    }

    private void FireShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo >= 1 && canShoot)
        {
            anim.SetTrigger("shoot");
        }
    }



    // Reload Anim Event
    void ReloadGunAnim()
    {
        currentAmmo = maxAmmo;
        source.PlayOneShot(reloadSound);
        shotgunShell1.SetActive(true);
        shotgunShell2.SetActive(true);
    }

    // Cock Gun anim
    void PlayReloadSound()
    {
        source.PlayOneShot(pumpShotgunSound);
    }

    void WeCanShoot()
    {
        canShoot = true;
    }

    void WeCannotShoot()
    {
        canShoot = false;
    }
    //anim event
    void PlayPelletParticles()
    {
        currentAmmo--;
        pellets.Play();
        source.PlayOneShot(gunSounds[Random.Range(0, gunSounds.Length)]);
        if (currentAmmo == 1)
        {
            shotgunShell1.SetActive(false);
        }
        else if (currentAmmo == 0)
        {
            shotgunShell1.SetActive(false);
            shotgunShell2.SetActive(false);

        }
    }
}
