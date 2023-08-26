using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private HeartContainer heartContainer;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private TMP_Text winText;

    private void Awake()
    {
        heartContainer.SetHearts(Health.healthPoints);
        Destroy(Win.won ? loseText : winText);
        score.text = "Final Score: " + ItemCollector.tokens;
    }
}
