using Guns;
using UnityEngine;
using UnityEngine.AI;

namespace NPCs {

public class CopBehavior : NpcBehavior
{
    [SerializeField]
    GunController myGun;
    [SerializeField]
    float shootRange = 40;
    [SerializeField]
    float chaseRange = 100;

    [SerializeField]
    float movementRange = 180;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color tempColor = Random.ColorHSV();

        transform.Find("Model/Head").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Ear (1)").GetComponent<Renderer>().material.color = tempColor;

        tempColor = Random.ColorHSV();

        transform.Find("Model/Eye").GetComponent<Renderer>().material.color = tempColor;
        transform.Find("Model/Eye (1)").GetComponent<Renderer>().material.color = tempColor;

        if (Random.Range(0,2) > 0) {
            transform.Find("Gun").GetComponent<GunController>().SetGun(GunController.GunType.Shotgun);
            shootRange = 30;
        }

        transform.Find("Gun").GetComponent<GunController>().DoubleReload();

    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position,PlayerControls.PlayerController.reference.transform.position);

        if (playerDistance < chaseRange) {
            nav.SetDestination(PlayerControls.PlayerController.reference.transform.position);
        } else {
            nav.SetDestination(transform.position + new Vector3(Random.Range(-movementRange, movementRange), 1.7f, Random.Range(-movementRange, movementRange)));
        }

        LayerMask wall = LayerMask.GetMask("Wall");
        if (!Physics.Linecast(transform.position,PlayerControls.PlayerController.reference.transform.position,wall) &&
            playerDistance < shootRange) {
            myGun.Shoot();
        }
    }

}

}