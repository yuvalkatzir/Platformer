using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private SpawnPoints spawn;
    [SerializeField] private HeartContainer heartContainer;
    [SerializeField] private AudioSource deathSound;
    public static int healthPoints;

    private void Start()
    {
        healthPoints = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if(rb.bodyType == RigidbodyType2D.Dynamic)
                TakeDamage();
            deathSound.Play();
        }
    }
    public void TakeDamage()
    {
        healthPoints = Mathf.Max(healthPoints-1,0);
        animator.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        heartContainer.SetHearts(healthPoints);
        //Debug.Log(healthPoints == 0 ? "Dead.. bleh xP" : "I'm hit !!!"); //working
    }

    private void Revive()
    {
        if (healthPoints != 0)
        {
            spawn.Respawn();
            animator.SetTrigger("revive");
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            SceneManager.LoadScene("End Scene");
        }
    }
}
