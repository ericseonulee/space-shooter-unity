using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource mySFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;
    public AudioClip swishoutSFX;

    public void LoadGame() {
        StartCoroutine(LoadGameRoutine());
    }

    public void ExitGame() {
        StartCoroutine(ExitGameRoutine());
    }

    public void HoverSound() {
        mySFX.PlayOneShot(hoverSFX, 0.5f);
    }

    public void ClickSound() {
        mySFX.PlayOneShot(clickSFX, 0.5f);
    }

    public void SwishoutSound() {
        mySFX.PlayOneShot(swishoutSFX, 0.5f);
    }

    IEnumerator LoadGameRoutine() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1); //game scene
    }

    IEnumerator ExitGameRoutine() {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
