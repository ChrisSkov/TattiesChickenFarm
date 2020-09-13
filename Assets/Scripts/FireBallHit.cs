using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallHit : MonoBehaviour
{

    float damage;
    float destroyTimeAfterHit;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject, destroyTimeAfterHit);

    }


    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetDestroyAfterHit(float time)
    {
        destroyTimeAfterHit = time;
    }
}
