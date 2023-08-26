using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float distance = 10f;
    [SerializeField] private WaypointPatrol waypointPatrol;
    private RaycastHit2D ray;
    private bool collidingPlayer = false;

    private void FixedUpdate()
    {
        int playerLayerMask = LayerMask.GetMask("Player");
        int groundLayerMask = LayerMask.GetMask("Ground");

        Vector2 rayDirection = waypointPatrol.isMovingForward ? transform.right : -transform.right;

        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, rayDirection, distance, playerLayerMask);
        RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, rayDirection, distance, groundLayerMask);

        if (obstacleHit.collider != null && obstacleHit.collider.CompareTag("Wall"))
        {
            Debug.DrawRay(transform.position, rayDirection * distance, Color.red);
            //Debug.Log("Ray hit an obstacle (wall)");
            // Handle the case when the ray hits a wall
        }
        else if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            Debug.DrawRay(transform.position, rayDirection * distance, Color.white);
            animator.SetBool("detection",true);
            waypointPatrol.InstantWayPoint(10f);

            //Debug.Log("Ray hit the player");
            // Handle the case when the ray hits the player
        }
        else
        {
            Debug.DrawRay(transform.position, rayDirection * distance, Color.black);
            //Debug.Log("Did not hit anything");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (collidingPlayer)
            {
                animator.SetTrigger("hit_wall");
                animator.SetBool("detection", false);
                if (collision.isTrigger)
                {
                    collision.enabled = false;
                }
                playerHealth.TakeDamage();
                RinoCrash();
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collidingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingPlayer = false;
        }
    }

    public void RinoCrash()
    {
        waypointPatrol.HitObstacle();
    }
}