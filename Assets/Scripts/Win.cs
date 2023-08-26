using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private AudioSource winSound;
    public static bool won = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!won)
            {
                winSound.Play();
                Invoke("CompleteLevel", 1f);
            }
            won = true;
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene("End Scene");
    }
}
