using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;

    [SerializeField] float hitDistance = 2f;
    [SerializeField] float hitDamage = 25f;
    [SerializeField] float hitForce = 3f;
    [SerializeField] int currentlyEquippedWeapon;
    [SerializeField] AudioClip[] punchSound;
    Animator anim;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        ChangeWeapon();
        ShowCorrectWeapon(currentlyEquippedWeapon);
        ThrowPunch();
    }


    public void ShowCorrectWeapon(int weaponID)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(i == weaponID);
    }
    private void ThrowPunch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentlyEquippedWeapon == 0)
        {
            anim.SetTrigger("attack");
        }
    }

    void PunchAnimEvent()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        foreach (Collider c in Physics.OverlapSphere(weapons[0].transform.position, hitDistance, mask))
        {
            c.gameObject.GetComponent<EnemyHealth>().TakeDamage(hitDamage);
            source.PlayOneShot(punchSound[Random.Range(0, punchSound.Length)]);
            if (Physics.Raycast(transform.position, c.transform.position - transform.position, out hit, mask))
            {
                c.GetComponent<Rigidbody>().AddForce(-hit.normal * hitForce, ForceMode.Impulse);
            }
        }
    }
    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentlyEquippedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentlyEquippedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentlyEquippedWeapon = 2;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(weapons[0].transform.position, hitDistance);
    }
    public int GetEquippedWeapon()
    {
        return currentlyEquippedWeapon;
    }

}
