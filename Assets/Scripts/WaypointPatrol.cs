using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    private Animator animator;
    private bool isMovingForward;
    private bool isMovingPlatform = false;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[currentWaypointIndex].position;
        if (gameObject.CompareTag("Moving Platform"))
        {
            isMovingPlatform = true;
            isMovingForward = false;
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < .01f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            if (isMovingPlatform)
            {
                isMovingForward = !isMovingForward;
                animator.SetBool("MoveForward", isMovingForward);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position,
            Time.deltaTime * speed);
    }
}