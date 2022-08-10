using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private GameObject _pauseMenuPanel;

    public AudioSource mySFX;
    public AudioClip window_upSFX;
    public AudioClip window_downSFX;


    private void Update() {
        //restart the game if R key is pressed.
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver) {
            //SceneManager.LoadScene("Game"); Strings are slower
            SceneManager.LoadScene(1); // Current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_pauseMenuPanel.activeSelf) {
                ResumeGame();
            }
            else {
                _pauseMenuPanel.SetActive(true);
                AudioSource.PlayClipAtPoint(window_upSFX, new Vector3(0, 0, -10), 0.5f);
                Time.timeScale = 0;
            }
        }
    }

    public void GameOver() {
        //Debug.Log("GameManager::GameOver() Called");
        _isGameOver = true;
    }

    public void ResumeGame() {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        AudioSource.PlayClipAtPoint(window_downSFX, new Vector3(0, 0, -10), 0.5f);
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        AudioSource.PlayClipAtPoint(window_downSFX, new Vector3(0, 0, -10), 0.5f);
        SceneManager.LoadScene(0);
    }

    public void ExitGame() {
        StartCoroutine(ExitGameRoutine());
    }

    public static IEnumerator ExitGameRoutine() {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.8f);
        Application.Quit();
    }
}
