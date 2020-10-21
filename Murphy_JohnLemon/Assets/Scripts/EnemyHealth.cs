using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 2;
    // Start is called before the first frame update

        public void TakeDamage(int damageAmount)
        {
          health -= damageAmount;

          if (health <= 0)
          {
            Destroy(gameObject);
          }
        }

}
