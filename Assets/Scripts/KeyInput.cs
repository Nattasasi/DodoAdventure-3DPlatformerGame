using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyInput : MonoBehaviour
{
    public Animator characterAnimator;
    // Start is called before the first frame update
    public float speed = 2.0f;
    public bool isGrounded = true;
    public bool isClimbing = false;
    public float jumpForce = 6f;

    private GameManager gameManager;
    private PlayerController pc;
    private AudioSource audioSource;
    public AudioClip climbSound;
    public AudioClip jumpSound;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterAnimator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {

        // Jump only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterAnimator.SetTrigger("JumpTrigger");
            isGrounded = false;
            PlaySound(jumpSound);
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        // Climb ladder
        if (isClimbing)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Keep rotation frozen
            // rb.velocity = Vector3.zero; // Freeze physics movement
            // rb.constraints = RigidbodyConstraints.FreezePosition;

            Vector3 climbDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) climbDirection += Vector3.up;
            if (Input.GetKey(KeyCode.S)) climbDirection += Vector3.down;
            if (Input.GetKey(KeyCode.A)) climbDirection += Vector3.left;
            if (Input.GetKey(KeyCode.D)) climbDirection += Vector3.right;

            if (climbDirection != Vector3.zero)
            {
                // rb.constraints = RigidbodyConstraints.None; // Unfreeze to allow movement
                transform.Translate(climbDirection.normalized * Time.deltaTime * speed);
                rb.constraints = RigidbodyConstraints.FreezePosition; // Freeze again after movement
                rb.constraints = RigidbodyConstraints.FreezeRotation; // Keep rotation frozen
            }
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            // rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Keep rotation frozen
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("woodground") || collision.gameObject.CompareTag("grassground") || collision.gameObject.CompareTag("stoneground") || collision.gameObject.CompareTag("metalground") || collision.gameObject.CompareTag("platform") || collision.gameObject.CompareTag("movingplatform") || collision.gameObject.CompareTag("hazzard"))
        {
            isGrounded = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = true;
            PlaySound(climbSound);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            isClimbing = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 1, ForceMode.Impulse); // Add upward force when exiting ladder
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            StopSound(climbSound);
        }
    }

    // Example usage for playing sounds:
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            if (clip == jumpSound)
            {
                audioSource.loop = false;
                audioSource.PlayOneShot(jumpSound);
            }
            else
            {
                audioSource.loop = true;
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    void StopSound(AudioClip clip)
    { 
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Stop(); 
        }
    }

}
