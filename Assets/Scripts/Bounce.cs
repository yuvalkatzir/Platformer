using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 27f;
    [SerializeField] private AudioSource trampolineSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.tag == "Trampoline")
            {
                animator.SetTrigger("bounce");
                trampolineSound.Play();
            }
            playerController.Jump(jumpForce);
            playerController.ResetJump();
        }
    }
}
