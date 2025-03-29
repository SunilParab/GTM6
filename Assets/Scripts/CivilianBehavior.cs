using UnityEngine;
using UnityEngine.AI;

namespace NPCs {

public class CivilianBehavior : NpcBehavior
{

    [SerializeField]
    float movementRange = 180;
    [SerializeField]
    float timer = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("PickSpot",0,timer);

        Color tempColor = Random.ColorHSV();

        transform.Find("Model/Head").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear (1)").GetComponent<Renderer>().material.color = tempColor;

        tempColor = Random.ColorHSV();

        transform.Find("Model/Eye").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Eye (1)").GetComponent<Renderer>().material.color = tempColor;
        //Shirt
        transform.Find("Model/Capsule").GetComponent<Renderer>().material.color = Random.ColorHSV();

        cash = Random.Range(1,30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickSpot() {
        nav.SetDestination(transform.position + new Vector3(Random.Range(-movementRange, movementRange), 1.7f, Random.Range(-movementRange, movementRange)));
    }

}

}