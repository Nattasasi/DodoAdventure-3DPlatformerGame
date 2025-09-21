using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    // public AudioClip clickSound;
    // private AudioSource audioSource;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        // audioSource = GetComponent<AudioSource>();
    }
    void OnButtonClicked()
    {
        // if (audioSource != null && clickSound != null)
        // {
            // audioSource.PlayOneShot(clickSound);
            // StartCoroutine(WaitAndRestart());
            gameManager.RestartGame();
        // }
    }

    // private IEnumerator WaitAndRestart()
    // {
    //     yield return new WaitForSeconds(clickSound.length);
    //     gameManager.RestartGame();
    // }
        
}
