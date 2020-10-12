using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    // instantiators
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;


    void OnTriggerEnter(Collider other)
    {
      // if collider comes in contact with main player
      if(other.transform == player)
      {
        m_IsPlayerInRange = true;
      }
    }

    void OnTriggerExit(Collider other)
    {
      // if collider comes in contact with main player
      if(other.transform == player)
      {
        m_IsPlayerInRange = false;
      }
    }


    void Update()
    {
      if(m_IsPlayerInRange)
      {
        // if collider sees player, change direction point ray at player
        Vector3 direction = player.position - transform.position + Vector3.up;
        Ray ray = new Ray (transform.position, direction);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit))
        {
          if(raycastHit.collider.transform == player)
          {
            gameEnding.CaughtPlayer();
          }
        }
      }
    }
}
