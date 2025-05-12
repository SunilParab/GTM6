using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }


}
