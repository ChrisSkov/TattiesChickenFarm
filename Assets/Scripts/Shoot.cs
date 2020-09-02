using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoot : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] AudioClip gunSound = null;
    [SerializeField] AudioClip reloadSound = null;
    [Header("Gun variables")]
    [SerializeField] int maxAmmo;
    [SerializeField] int currentAmmo;
    [SerializeField] float damage = 10f;


    GameObject[] shotgunShells;

    AudioSource source;

    bool canShoot = true;
    ParticleSystem pellets;
    Animator anim;

    EnemyHealth enemyHealth;

    HitEnemy hitEnemy;

    // Start is called before the first frame update
    void Start()
    {

        shotgunShells = GameObject.FindGameObjectsWithTag("ShotgunShell");
        maxAmmo = shotgunShells.Length;
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetTrigger("slash");
        }


    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("reload");
            foreach (GameObject shell in shotgunShells)
            {
                shell.SetActive(true);
            }
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
        source.PlayOneShot(gunSound);
        shotgunShells[maxAmmo - currentAmmo - 1].SetActive(false);
    }
}
