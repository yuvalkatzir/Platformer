using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoHeadCollision : MonoBehaviour
{
    [SerializeField] private RinoController rinoController;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            animator.SetTrigger("hit_wall");
            animator.SetBool("detection", false);
            rinoController.RinoCrash();
        }
    }
}
