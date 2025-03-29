using UnityEngine;
using UnityEngine.AI;

namespace NPCs {

public class CopBehavior : NpcBehavior
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color tempColor = Random.ColorHSV();

        transform.Find("Model/Head").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear (1)").GetComponent<Renderer>().material.color = tempColor;

        tempColor = Random.ColorHSV();

        transform.Find("Model/Eye").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Eye (1)").GetComponent<Renderer>().material.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(PlayerControls.PlayerController.reference.transform.position);
    }

}

}