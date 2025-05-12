using UnityEngine;

namespace NPCs
{

public class WantedDisplay : MonoBehaviour
{

    [SerializeField]
    GameObject[] stars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive(false);
        }
    }

    public void ShowStars() {
        int i = 0;
        for (; i < WantedManager.reference.wantedStars; i++) {
            stars[i].SetActive(true);
        }

        for (; i < stars.Length; i++) {
            stars[i].SetActive(false);
        }
    }

}

}