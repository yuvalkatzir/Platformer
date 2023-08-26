using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public static int tokens;
    private Dictionary<string, int> tokenMap;
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioSource collectSound;

    private void Start()
    {
        tokens = 0;
        tokenMap = new Dictionary<string, int>();
        tokenMap["Apple"] = 10;
        tokenMap["Banana"] = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string fruitName in tokenMap.Keys)
        {
            if (collision.gameObject.CompareTag(fruitName))
            {
                int fruitValue = tokenMap[fruitName];
                Animator tokenAnimator = collision.gameObject.GetComponent<Animator>();

                if (tokenAnimator != null)
                {
                    tokenAnimator.SetTrigger("collect");

                    // Wait for the animation to complete and then destroy the token
                    StartCoroutine(WaitForAnimationAndDestroy(collision.gameObject));
                }
                else
                {
                    Destroy(collision.gameObject);
                }

                tokens += fruitValue;
                text.text = "Score: "+ tokens;
                collectSound.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (string fruitName in tokenMap.Keys)
        {
            if (collision.gameObject.CompareTag(fruitName))
            {
                collision.enabled = false;
            }
        }
    }

    private System.Collections.IEnumerator WaitForAnimationAndDestroy(GameObject token)
    {
        // Wait for the animation to complete
        yield return new WaitForSeconds(token.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        Destroy(token);
    }
}