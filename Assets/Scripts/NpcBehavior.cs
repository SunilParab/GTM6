using UnityEngine;

namespace NPCs {

public class NpcBehavior : MonoBehaviour
{
    [SerializeField]
    protected float health;

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
        health -= damage;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

}

}