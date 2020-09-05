using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [SerializeField] CapsuleCollider swordCollider;
    public float swordDamage = 20f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        Collider myCollider = other.contacts[0].thisCollider;
        print(myCollider);
        print(swordCollider);
        if (other.gameObject.tag == "Enemy" && myCollider == swordCollider)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(swordDamage);
        }
    }

}
