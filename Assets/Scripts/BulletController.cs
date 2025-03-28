using System.Runtime.ExceptionServices;
using UnityEngine;

namespace Guns {

public class BulletController : MonoBehaviour
{

    float damage;
    bool friendly;
    float lifespan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("maxRange",lifespan);
    }

    public void setup(float damage, bool friendly, float lifespan) {
        this.damage = damage;
        this.friendly = friendly;
        this.lifespan = lifespan;
    }

    public bool getFriendly() {
        return friendly;
    }

    public float hit() {
        Destroy(gameObject);
        return damage;
    }

    void maxRange() {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {Debug.Log("detect");

        if (friendly) {

            if (collision.gameObject.CompareTag("Player")) {
                return;
            }

            if (collision.gameObject.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<NPCs.NpcBehavior>().TakeDamage(damage);Debug.Log("damage");
            }
            
        } else {

            if (collision.gameObject.CompareTag("Enemy")) {
                return;
            }

            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.GetComponent<PlayerControls.PlayerController>().TakeDamage(damage);
            }

        }Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }

}

}