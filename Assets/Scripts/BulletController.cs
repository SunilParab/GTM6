using System.Runtime.ExceptionServices;
using UnityEngine;

namespace Guns {

public class BulletController : MonoBehaviour
{

    float damage;
    bool friendly;
    float lifespan;

    Vector3 lastPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("maxRange",lifespan);
        lastPos = transform.position;
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

    void LateUpdate()
    {
        LayerMask wall = ~LayerMask.GetMask(new string[] {"Bullet","Car"});
        RaycastHit hit;

        if (Physics.Linecast(lastPos,transform.position,out hit,wall)) {
            Collided(hit.collider);
        }
        lastPos = transform.position;
    }

    void Collided(Collider collision)
    {

        if (friendly) {

            if (collision.gameObject.CompareTag("Player")) {
                return;
            }

            if (collision.gameObject.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<NPCs.EnemyTarget>().TakeDamage(damage);
                Destroy(gameObject);
            }
            
        } else {

            if (collision.gameObject.CompareTag("Enemy")) {
                return;
            }

            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.GetComponent<PlayerControls.PlayerController>().TakeDamage(damage);
                Destroy(gameObject);
            }

        }

        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }

}

}