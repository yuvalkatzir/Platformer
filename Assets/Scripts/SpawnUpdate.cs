using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUpdate : MonoBehaviour
{
    [SerializeField] private SpawnPoints spawnManager;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource checkpointSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool isValid = spawnManager.UpdateSpawn(transform);
            if (isValid)
            {
                animator.SetBool("checkpoint", isValid);
                checkpointSound.Play();
            }
        }
    }
}
