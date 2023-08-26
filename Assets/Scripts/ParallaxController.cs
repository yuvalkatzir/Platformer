using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backGrounds;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    [Range(0.01f,0.05f)]
    public float parallaxSpeed;
    void Start()
    {
        //cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backGrounds = new GameObject[backCount];

        for(int i = 0; i < backCount; i++)
        {
            backGrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backGrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for(int i = 0; i < backCount; i++)
        {
            if (backGrounds[i].transform.position.z - cam.position.z > farthestBack)
            {
                farthestBack= backGrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backGrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;

        for(int i = 0; i < backGrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
