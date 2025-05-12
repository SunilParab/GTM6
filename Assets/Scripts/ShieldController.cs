using NPCs;
using UnityEngine;

public class ShieldController : MonoBehaviour, EnemyTarget
{

    [SerializeField]
    protected float health = 100;

    //Damage Functions
    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
