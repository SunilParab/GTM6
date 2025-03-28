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