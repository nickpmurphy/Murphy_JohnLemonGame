using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
      if(!other.isTrigger)
      {
        if (other.gameObject.CompareTag("Enemy"))
        {
          EnemyHealth eHealth = other.gameObject.GetComponent<EnemyHealth>();

          if (eHealth != null)
          {
            eHealth.TakeDamage(1);
          }
        }
        Destroy(gameObject);
      }
    }
}
