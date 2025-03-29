using UnityEngine;
using UnityEngine.AI;

namespace NPCs {

public class NpcBehavior : MonoBehaviour
{
    [SerializeField]
    protected float health;
    [SerializeField]
    protected NavMeshAgent nav;

    [SerializeField]
    protected float wantedValue;
    float cash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Internal Functions
    public void TakeDamage(float damage) {
        if (health <= 0) {
            return;
        }

        health -= damage;

        if (health <= 0) {
            WantedManager.reference.GainWanted(wantedValue);
            Destroy(gameObject);
        }
    }

}

}