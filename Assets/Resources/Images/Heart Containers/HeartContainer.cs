using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public void SetHearts(int hearts)
    {
        hearts = Mathf.Max(0, Mathf.Min(hearts, images.Length));
        for(int i = 0; i < hearts - 1; i++)
        {
            images[i].sprite = fullHeart;
        }

        for (int i = hearts; i < images.Length; i++)
        {
            images[i].sprite = emptyHeart;
        }
    }
}
