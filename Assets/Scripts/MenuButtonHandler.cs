using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] private AudioSource selectSound;
    private float delay = 0.1f;
    public void Play()
    {
        selectSound.Play();
        Invoke("StartGame", delay);
    }

    public void Options()
    {
        selectSound.Play();
    }

    public void Exit()
    {
        selectSound.Play();
        Invoke("Quit", delay);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Stage 1-1");
    }
}