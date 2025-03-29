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
    protected float cash;

    bool dead = false;

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
        health -= damage;

        if (health <= 0 && !dead) {

            dead = true;

            Die();
        }
    }

    void Die() {
        WantedManager.reference.GainWanted(wantedValue);
        PlayerControls.PlayerController.reference.money += cash;
        Destroy(gameObject);
    }

}

}