using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace PlayerControls {

public class GunController : MonoBehaviour
{

    //Shooting Variables
    [SerializeField]
    float reloadTime = 0.5f;
    bool reloaded = true;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Image reloadImage;

    //Input Systems
    InputAction attackAction;

    //Gun Variables
    enum GunType {Pistol,Cannon,Sniper}
    [SerializeField]
    GunType curGun = GunType.Pistol;

    //Pistol Variables
    float pistolBulletSpeed = 40;
    float pistolBulletDamange = 10;
    float pistolBulletLifeSpan = 4;
    float pistolBulletSize = 1;

    //Cannon Variables
    float cannonBulletSpeed = 10;
    float cannonBulletDamange = 20;
    float cannonBulletLifeSpan = 10;
    float cannonBulletSize = 5;

    //Sniper Variables
    float sniperBulletSpeed = 200;
    float sniperBulletDamange = 40;
    float sniperBulletLifeSpan = 3;
    float sniperBulletSize = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.triggered && reloaded) {
            Fire();
        }

        if (Input.GetKey(KeyCode.Alpha1)) {
            curGun = GunType.Pistol;
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            curGun = GunType.Cannon;
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            curGun = GunType.Sniper;
        }

    }


    void Fire() {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
        switch (curGun) {
            case GunType.Pistol:
                PistolBullet(bullet);
                break;
            case GunType.Cannon:
                CannonBullet(bullet);
                break;
            case GunType.Sniper:
                SniperBullet(bullet);
                break;
        }

        reloaded = false;
        //reloadImage.color = Color.red;
        Invoke("Reload",reloadTime);
    }

    void Reload() {
        reloaded = true;
        //reloadImage.color = Color.green;
    }

    void PistolBullet(GameObject bullet) {
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*pistolBulletSpeed;
        bullet.GetComponent<BulletController>().setup(pistolBulletDamange,true,pistolBulletLifeSpan);
        bullet.transform.localScale *= pistolBulletSize;
    }

    void CannonBullet(GameObject bullet) {
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*cannonBulletSpeed;
        bullet.GetComponent<BulletController>().setup(cannonBulletDamange,true,cannonBulletLifeSpan);
        bullet.transform.localScale *= cannonBulletSize;
    }

    void SniperBullet(GameObject bullet) {
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*sniperBulletSpeed;
        bullet.GetComponent<BulletController>().setup(sniperBulletDamange,true,sniperBulletLifeSpan);
        bullet.transform.localScale *= sniperBulletSize;
    }

}

}