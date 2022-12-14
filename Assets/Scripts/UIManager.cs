using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    public AudioSource mySFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;
    public AudioClip swishoutSFX;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        //_liveSprites[CurrentPlayerLives];
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null) {
            Debug.LogError("GameManage is NULL.");
        }
    }

    public void ExitGame() {
        StartCoroutine(GameManager.ExitGameRoutine());
    }

    public void ResumeGame() {
        GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        AudioSource.PlayClipAtPoint(clickSFX, new Vector3(0, 0, -10), 0.5f);
        gm.ResumeGame();
    }

    public void BackToMainMenu() {
        GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        AudioSource.PlayClipAtPoint(clickSFX, new Vector3(0, 0, -10), 0.5f);
        gm.BackToMainMenu();
    }

    public void ClickSound() {
        mySFX.PlayOneShot(clickSFX, 0.5f);
    }

    public void HoverSound() {
        mySFX.PlayOneShot(hoverSFX, 0.75f);
    }

    public void SwishoutSound() {
        mySFX.PlayOneShot(swishoutSFX, 0.5f);
    }

    public void UpdateScore(int playerScore) {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives) {
        _LivesImage.sprite = _lifeSprites[currentLives];

        if (currentLives == 0 ) {
            GameOverSequence();
        }
    }

    public void GameOverSequence() {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        StartCoroutine(RestartFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine() {
        while(true) {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RestartFlickerRoutine() {
        while (true) {
            _restartText.text = "Press the 'R' key to restart the level";
            yield return new WaitForSeconds(0.5f);
            _restartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
