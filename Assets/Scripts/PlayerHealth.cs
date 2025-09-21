using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private GameManager gameManager;
    public int currentHealth;
    private AudioSource playerAudio;
    public AudioClip damagedSound;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player took damage. Current health: " + currentHealth);
        playerAudio.PlayOneShot(damagedSound, 1.0f);
    }
}
