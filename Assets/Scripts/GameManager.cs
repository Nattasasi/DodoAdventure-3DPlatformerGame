using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI keyCountText;
    public bool isGameActive;
    public bool isGameWon;
    public GameObject titleScreen;
    public GameObject loadingMessage;
    public GameObject gameWonMessage;
    public GameObject gameOverMessage;
    public GameObject restartButton;
    public PlayerHealth playerHealth;
    private int keyCount = 0;
    public int keyNeeded;
    private AudioSource gameAudio;
    public AudioClip gameOverSound;
    public AudioClip gameWinSound;
    // public AudioClip backgroundMusic;
    public AudioClip levelCompleteSound;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        if (gameAudio != null)
        {
            gameAudio.loop = false;
        }

        loadingMessage.SetActive(false);
        gameWonMessage.SetActive(false);
        gameOverMessage.SetActive(false);
        restartButton.SetActive(false);
        //debugging purpose, remove later
        // titleScreen.SetActive(false);

        // Restore keyCount if it was saved
        if (PlayerPrefs.HasKey("TempKeyCount"))
        {
            keyCount = PlayerPrefs.GetInt("TempKeyCount");
            UpdateKeyCount(0); // Update UI
            PlayerPrefs.DeleteKey("TempKeyCount"); // Clean up
        }
        if (PlayerPrefs.HasKey("Reseted"))
        {
            isGameActive = true;
            titleScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerPrefs.DeleteKey("Reseted"); // Clean up
        }
        if (PlayerPrefs.HasKey("Health"))
        {
            int savedHealth = PlayerPrefs.GetInt("Health");
            playerHealth.currentHealth = savedHealth;
            PlayerPrefs.DeleteKey("Health"); // Clean up
        }
    }
    public void StartGame()
    {
        keyCount = 0;
        UpdateKeyCount(0);
        isGameActive = true;
        titleScreen.SetActive(false);
        loadingMessage.SetActive(false);
        gameWonMessage.SetActive(false);
        gameOverMessage.SetActive(false);
        restartButton.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameAudio.PlayOneShot(clickSound);

        // Only reset health to max if this is a new game, not a reset
        if (!PlayerPrefs.HasKey("Reseted"))
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
    }

    public void RestartGame()
    {
        if (isGameWon)
        {
            // Reload first scene
            SceneManager.LoadScene(0);
        }
        else
        {
            // Reload current scene
            gameAudio.PlayOneShot(clickSound);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
        
    }

    public void ResetGame()
    {
        // Save keyCount before reloading
        Debug.Log("Game is resetting, saving key count and health.");
        PlayerPrefs.SetInt("TempKeyCount", keyCount);
        PlayerPrefs.SetInt("Reseted", 1);
        PlayerPrefs.SetInt("Health", playerHealth.currentHealth);
        PlayerPrefs.Save();

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && keyCount == keyNeeded)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                // Not the last scene, delayed load next scene
                StartCoroutine(LoadNextScene());
                loadingMessage.SetActive(true);
                
            }
            else
            {
                // Last scene, show game won message
                gameAudio.PlayOneShot(gameWinSound);
                isGameWon = true;
                gameWonMessage.SetActive(true);
                titleScreen.SetActive(true);
                restartButton.SetActive(true);
                isGameActive = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (!isGameActive)
        {
            Time.timeScale = 0;
            if (gameAudio != null && gameAudio.isPlaying)
            {
                gameAudio.Stop();
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        gameAudio.PlayOneShot(levelCompleteSound, 0.1f);
        yield return new WaitForSeconds(4f); // Wait for 4 seconds
        PlayerPrefs.SetInt("Reseted", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateKeyCount(int count)
    {
        keyCount += count;
        keyCountText.text = $"Keys: {keyCount.ToString()}/{keyNeeded.ToString()}";
    }

    public void GameOver()
    {
        gameAudio.PlayOneShot(gameOverSound);
        gameOverMessage.SetActive(true);
        titleScreen.SetActive(true);
        restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isGameActive = false;
    }

    // Example usage for playing sounds:
    void PlayGameSound(AudioClip clip)
    {
        if (gameAudio != null && clip != null)
        {
            gameAudio.loop = false;
            gameAudio.PlayOneShot(clip);
        }
    }
}
