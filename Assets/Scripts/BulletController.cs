using System.Runtime.ExceptionServices;
using UnityEngine;

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

}