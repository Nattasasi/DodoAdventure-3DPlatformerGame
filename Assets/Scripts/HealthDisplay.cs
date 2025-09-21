using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
    private int health;
    // public int maxHealth;
    public Sprite heart;
    public Image[] hearts;
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            health = playerHealth.currentHealth;
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].enabled = true;
                    hearts[i].sprite = heart;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }
}
