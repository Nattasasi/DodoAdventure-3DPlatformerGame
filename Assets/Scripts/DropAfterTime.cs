using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAfterTime : MonoBehaviour
{
    [Header("Drop Settings")]
    public float dropDelay = 5f; // Time in seconds before object starts dropping
    public float dropSpeed = 10f; // Speed at which object falls
    public float destroyYPosition = -10f; // Y position where object gets destroyed
    
    private bool playerContacted = false;
    private bool isDropping = false;
    private float contactTimer = 0f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Initially keep the object stationary
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is in contact, increment timer
        if (playerContacted && !isDropping)
        {
            contactTimer += Time.deltaTime;
            
            // Start dropping after delay
            if (contactTimer >= dropDelay)
            {
                StartDropping();
            }
        }
        
        // If dropping, check if object should be destroyed
        if (isDropping && transform.position.y <= destroyYPosition)
        {
            Destroy(gameObject);
        }
    }
    
    private void StartDropping()
    {
        isDropping = true;
        rb.isKinematic = false;
        rb.velocity = Vector3.down * dropSpeed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerContacted = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContacted = true;
        }
    }
}
