using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    public float bounceForce = 10f;
    private AudioSource bounceAudio;
    public AudioClip bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        bounceAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {      
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.BouncePlayer(bounceForce);
                bounceAudio.PlayOneShot(bounceSound, 1.0f);
            }
        }
    }
}
