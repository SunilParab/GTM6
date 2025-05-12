using System.Collections;
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

    [SerializeField]
    float maxDownTime = 5f;
    [SerializeField]
    float downTime = 5f;
    bool timeTicking = false;

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

        transform.Find("Gun").GetComponent<GunController>().InitDoubleReload();

        if (!KeepShield()) {
            Destroy(transform.Find("Shield").gameObject);
        }

        cash = Random.Range(10,50);
    }

    // Update is called once per frame
    void Update()
    {

        if (WantedManager.reference.wantedStars > 0) {

            if (timeTicking) {
                downTime = maxDownTime;
                timeTicking = false;
            }

            float playerDistance = Vector3.Distance(transform.position,PlayerControls.PlayerController.reference.transform.position);

            if (playerDistance < chaseRange) {
                nav.SetDestination(PlayerControls.PlayerController.reference.transform.position);
            } else {
                nav.SetDestination(transform.position + new Vector3(Random.Range(-movementRange, movementRange), 1.7f, Random.Range(-movementRange, movementRange)));
            }

            LayerMask wall = LayerMask.GetMask("Wall");
            if (!Physics.Linecast(transform.position,PlayerControls.PlayerController.reference.transform.position,wall) &&
                playerDistance < shootRange) {
                myGun.transform.rotation = Quaternion.LookRotation(PlayerControls.PlayerController.reference.transform.position-myGun.transform.position, Vector3.up);
                myGun.Shoot();
            }
        } else {

            if (timeTicking) {
                downTime -= Time.deltaTime;
            } else {
                downTime = Random.Range(0f,maxDownTime);
                timeTicking = true;
            }

            if (downTime <= 0) {

                if (Random.Range(0,3) >= 2) {
                    Destroy(gameObject);
                    return;
                } else {
                    downTime = Random.Range(0f,maxDownTime);
                    PickSpot();
                }
            }
        }

    }

    void PickSpot() {
        nav.SetDestination(transform.position + new Vector3(Random.Range(-movementRange, movementRange), 1.7f, Random.Range(-movementRange, movementRange)));
    }

    bool KeepShield() {
        if (WantedManager.reference.wantedStars < 4) {
            return false;
        }

        return (Random.Range(0,3)+WantedManager.reference.wantedStars) > 5;

    }

    public void SetLongLife(int lifetime) {
        downTime = lifetime;
        timeTicking = true;
        PickSpot();
    }

}

}