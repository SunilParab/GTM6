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

    [SerializeField]
    bool isFriendly;

    //Gun Variables
    public enum GunType {Pistol,Cannon,Sniper}
    [SerializeField]
    public GunType curGun = GunType.Pistol;

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


    public void Shoot()
    {
        if (reloaded) {
            Fire();
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
        bullet.GetComponent<BulletController>().setup(pistolBulletDamange,isFriendly,pistolBulletLifeSpan);
        bullet.transform.localScale *= pistolBulletSize;
    }

    void CannonBullet(GameObject bullet) {
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*cannonBulletSpeed;
        bullet.GetComponent<BulletController>().setup(cannonBulletDamange,isFriendly,cannonBulletLifeSpan);
        bullet.transform.localScale *= cannonBulletSize;
    }

    void SniperBullet(GameObject bullet) {
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*sniperBulletSpeed;
        bullet.GetComponent<BulletController>().setup(sniperBulletDamange,isFriendly,sniperBulletLifeSpan);
        bullet.transform.localScale *= sniperBulletSize;
    }

}

}