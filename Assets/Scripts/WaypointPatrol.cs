using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Transform[] tempWayPoints;
    [SerializeField] private float speed = 2f;
    private float currentSpeed = 2f;
    [SerializeField] private float delay = 0f;
    private Animator animator;
    public bool isMovingForward;
    private bool isMovingPlatform = false;
    private bool isEnemy = false;
    private bool isDelayed = false;
    private int currentWaypointIndex = 0;
    private bool isInstantWaypoint = false;

    private void Start()
    {
        transform.position = waypoints[currentWaypointIndex].position;
        if (gameObject.CompareTag("Moving Platform"))
        {
            isMovingPlatform = true;
            isMovingForward = false;
            animator = GetComponent<Animator>();
        }

        if (gameObject.CompareTag("Rino"))
        {
            isEnemy = true;
            isMovingForward = false;
            animator = GetComponent<Animator>();
            animator.SetTrigger("patrol");
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < .01f && !isDelayed)
        {
            if (isInstantWaypoint)
            {
                ReloadPoints();
            }
            if (isEnemy)
            {
                animator.SetTrigger("idle");
                animator.SetBool("detection", false);
            }
            if(delay > 0)
            {
                currentSpeed = 0;
                isDelayed = true;
            }
            Invoke("NextWaypoint", delay);
        }

        if (isEnemy)
        {
            waypoints[currentWaypointIndex].position = new Vector2(waypoints[currentWaypointIndex].position.x, transform.position.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position,
                Time.deltaTime * currentSpeed);
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        isDelayed = false;
        currentSpeed = speed;
        if (isMovingPlatform)
        {
            isMovingForward = !isMovingForward;
            animator.SetBool("MoveForward", isMovingForward);
        }
        else if (isEnemy)
        {
            isMovingForward = !isMovingForward;
            animator.SetTrigger("patrol");
            transform.localScale = new Vector3(isMovingForward ? -1 : 1, 1, 1) ;
        }
    }

    public void HitObstacle()
    {
        isDelayed = true;
        currentSpeed = 0;
        Vector2 moveTo = new Vector2(transform.position.x + (isMovingForward ? -0.1f : 0.1f), transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, moveTo, 1f);
        if (isInstantWaypoint)
        {
            ReloadPoints();
        }
        animator.SetTrigger("idle");
        Invoke("NextWaypoint", 2f);
    }

    public void InstantWayPoint(float sprintSpeed)
    {
        //tempWayPoints = waypoints;
        currentWaypointIndex = 0;
        waypoints[0].position = new Vector2(transform.position.x + (isMovingForward ? 1f : -1f) * 10, transform.position.y);
        currentSpeed = sprintSpeed;
    }

    private void ReloadPoints()
    {
        //waypoints = tempWayPoints;
        currentSpeed = speed;
        isInstantWaypoint = false;
    }
}