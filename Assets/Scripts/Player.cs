using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    // Public
    public float forceMultiplier = 3f;
    public float maxVelocity = 3f;
    public ParticleSystem deathParticles;
    

    // Private
    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

   

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    
    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        float horizontalInput = 0;

        if (Input.GetMouseButton(0))
        {
            var center = Screen.width / 2;
            var mousePosition = Input.mousePosition;


            if (mousePosition.x > center)
            {
                horizontalInput = 1;
            } else if (mousePosition.x < center)
            {
                horizontalInput = -1;
            }
        } 
        else
        {
             horizontalInput = Input.GetAxis("Horizontal");
        }


        

        if (rb.velocity.magnitude <= maxVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime ,0,0));
        }

        
    }

    private void OnEnable() {
        transform.position = new Vector3(0, 0.75f, 0);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        
    }


    private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Hazard"))
            {
                GameManager.Instance.GameOver();

                
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                cinemachineImpulseSource.GenerateImpulse();

            }
        }
}
