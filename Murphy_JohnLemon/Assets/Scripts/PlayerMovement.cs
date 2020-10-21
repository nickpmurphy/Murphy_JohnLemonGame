using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    // instantiators
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    public GameObject projectilePrefab;
    public Transform shotSpawn;
    public float shotSpeed = 10f;

    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource>();
        SetCountText();

    }

    void SetCountText()
    {
      GameEnding ge = new GameEnding();
      countText.text = "Time Left: " + ge.timeLeft.ToString();
    }

    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject projectile = Instantiate(projectilePrefab, shotSpawn.transform.position, projectilePrefab.transform.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.velocity = transform.forward * shotSpeed;
      }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // get x and y axis' for player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // set movement, normalize keeps vector in same direction
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        // combines vertical and horizontal bools to check if one or both are true, setting the bool isWalking
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        // if player is walking, play ambient sound
        if(isWalking)
        {
          if(!m_AudioSource.isPlaying)
          {
            m_AudioSource.Play();
          }
        }
        else
        {
          m_AudioSource.Stop ();
        }

        // initial variables to rotate player
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);


    }

    void OnAnimatorMove ()
    {
      // calls to move player
      m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
      m_Rigidbody.MoveRotation(m_Rotation);
    }
}
