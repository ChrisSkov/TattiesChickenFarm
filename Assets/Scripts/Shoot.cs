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

    [SerializeField] GameObject bulletSocket;
    [SerializeField] GameObject shellHolder;

    [SerializeField] ParticleSystem pellets;

    GameObject[] shotgunShells;

    AudioSource source;

    public bool canShoot = true;

    Animator anim;

    EnemyHealth enemyHealth;

    HitEnemy hitEnemy;

    PlayerCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<PlayerCombat>();
        shotgunShells = GameObject.FindGameObjectsWithTag("ShotgunShell");
        maxAmmo = shotgunShells.Length;
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        currentAmmo = maxAmmo;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        hitEnemy = pellets.GetComponent<HitEnemy>();
        hitEnemy.SetDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (combat.GetEquippedWeapon() == 1)
        {
            FireShot();
            Reload();
            shellHolder.SetActive(true);
        }
        else{
            shellHolder.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0 && canShoot)
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
        Instantiate(pellets, bulletSocket.transform.position, bulletSocket.transform.rotation);
        source.PlayOneShot(gunSound);
        shotgunShells[maxAmmo - currentAmmo - 1].SetActive(false);
    }
}
