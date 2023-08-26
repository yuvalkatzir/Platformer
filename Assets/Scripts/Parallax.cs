using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{

    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        distance = Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
    /*public Transform player;
    public float parallaxSpeed = 0.5f;
    public bool followY = false;

    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private Vector3 initialPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the parallax offset based on player's position
        float parallaxOffset = (player.position.x - initialPosition.x) * parallaxSpeed;

        // Calculate the new position for the background
        Vector3 newPosition = initialPosition + Vector3.right * parallaxOffset;

        // Check if the background needs to repeat
        if (Mathf.Abs(newPosition.x - initialPosition.x) >= spriteWidth)
        {
            // Move the background to the starting position to repeat
            newPosition.x = initialPosition.x + Mathf.Sign(parallaxOffset) * spriteWidth;
        }

        transform.position = newPosition;
        if (followY)
        {
            transform.position = new Vector3(transform.position.x,player.position.y,transform.position.z);
        }
    }*/
}

