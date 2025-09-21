using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    // private AudioSource audioSource;
    // public AudioClip clickSound;
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
            gameManager.StartGame();
        // }
    }

    // private IEnumerator WaitAndRestart()
    // {
    //     yield return new WaitForSeconds(clickSound.length);
    //     gameManager.StartGame();
    // }

}
