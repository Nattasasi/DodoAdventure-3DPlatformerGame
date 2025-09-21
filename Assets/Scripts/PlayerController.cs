using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KeyInput keyInput;
    public Animator characterAnimator;
    private GameManager gameManager;
    public GameObject respawnPoint;
    public float threshold;
    public float speed = 2.0f;
    public Transform orientation;
    public PlayerHealth playerHealth;

    private AudioSource playerAudio;
    public AudioClip grassWalkSound;
    public AudioClip stoneWalkSound;
    public AudioClip metalWalkSound;
    public AudioClip woodWalkSound;
    public AudioClip genericWalkSound;
    float horizontalInput;
    float verticalInput;
    Vector3 movemDir;
    Rigidbody rb;
    private string currentGroundTag = "ground";

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        if (playerAudio != null)
        {
            playerAudio.loop = false;
        }
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        keyInput = GetComponent<KeyInput>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0 || horizontalInput != 0)
        {
            MovePlayer();

            if (keyInput.isGrounded)
            {
                PlayGroundSound();
            }
        }
        else
        {
            characterAnimator.SetBool("isRunningForward", false);
            characterAnimator.SetBool("isRunningFast", false);
            if (playerAudio != null && playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
        }

        if (transform.position.y < threshold)
        {
            playerHealth.TakeDamage(1);
            if (playerHealth.currentHealth > 1)
            {
                gameManager.ResetGame();
            }
        }
    }

    private void PlayGroundSound()
    {
        AudioClip clipToPlay = null;
        switch (currentGroundTag)
        {
            case "grassground":
                clipToPlay = grassWalkSound;
                break;
            case "woodground":
                clipToPlay = woodWalkSound;
                break;
            case "metalground":
                clipToPlay = metalWalkSound;
                break;
            case "stoneground":
                clipToPlay = stoneWalkSound;
                break;
            case "ground":
                clipToPlay = genericWalkSound;
                break;
            default:
                clipToPlay = genericWalkSound;
                break;
        }
        if (playerAudio != null && clipToPlay != null)
        {
            playerAudio.volume = 0.7f; // 30% lower than default
            if (playerAudio.clip != clipToPlay || !playerAudio.isPlaying)
            {
                playerAudio.loop = true;
                playerAudio.clip = clipToPlay;
                playerAudio.Play();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Only update ground tag if player is standing on a valid ground
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("grassground") || collision.gameObject.CompareTag("woodground") || collision.gameObject.CompareTag("metalground") || collision.gameObject.CompareTag("stoneground"))
        {
            currentGroundTag = collision.gameObject.tag;
        }
    }

    void FixedUpdate()
    {

    }

    public void MovePlayer()
    {
        // Check ground type and play sound

        movemDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterAnimator.SetBool("isRunningForward", false);
            characterAnimator.SetBool("isRunningFast", true);
            currentSpeed *= 2;
        }
        else
        {
            characterAnimator.SetBool("isRunningForward", true);
            characterAnimator.SetBool("isRunningFast", false);
        }

        transform.position += movemDir.normalized * currentSpeed * Time.deltaTime;


    }

    public void BouncePlayer(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        // if (playerAudio != null)
        // {
        //     playerAudio.loop = false;
        // }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hazard"))
        {
            playerHealth.TakeDamage(1);

        }


    }

}
