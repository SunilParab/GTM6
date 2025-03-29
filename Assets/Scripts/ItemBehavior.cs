using UnityEngine;

namespace Items {

public class ItemBehavior : MonoBehaviour
{

    Transform targetToLook;

    //Find camera
    void Awake()
    {
        targetToLook = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetToLook);
        transform.rotation = Quaternion.LookRotation(-targetToLook.forward, Vector3.up);
        
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player")) {
            Collect();
            Destroy(gameObject);
        }
    }

    protected virtual void Collect() {

    }

}

}