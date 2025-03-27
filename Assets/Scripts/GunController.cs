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
    public enum GunType {Pistol,Shotgun,Sniper}
    public GunType curGun = GunType.Pistol;

    //Pistol Variables
    const float pistolBulletSpeed = 80;
    const float pistolBulletDamange = 20;
    const float pistolBulletLifeSpan = 3;
    const float pistolBulletSize = 0.5f;

    //Cannon Variables
    const float shotgunBulletSpeed = 160;
    const float shotgunBulletDamange = 10;
    const float shotgunBulletLifeSpan = 2;
    const float shotgunBulletSize = 0.5f;
    const int shotgunPelletNum = 12;
    const float shotgunSpread = 0.1f;

    //Sniper Variables
    const float sniperBulletSpeed = 300;
    const float sniperBulletDamange = 100;
    const float sniperBulletLifeSpan = 2;
    const float sniperBulletSize = 1;


    public void Shoot()
    {
        if (reloaded) {
            Fire();
        }

    }


    void Fire() {
        switch (curGun) {
            case GunType.Pistol:
                PistolBullet();
                break;
            case GunType.Shotgun:
                ShotgunShot();
                break;
            case GunType.Sniper:
                SniperBullet();
                break;
        }

        reloaded = false;
        Invoke("Reload",reloadTime);
    }

    void Reload() {
        reloaded = true;
    }

    void PistolBullet() {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*pistolBulletSpeed;
        bullet.GetComponent<BulletController>().setup(pistolBulletDamange,isFriendly,pistolBulletLifeSpan);
        bullet.transform.localScale *= pistolBulletSize;
    }

    void ShotgunShot() {

        for (int i = 0; i < shotgunPelletNum; i++) {
            GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.Normalize(bullet.transform.forward+new Vector3(Random.Range(shotgunSpread,-shotgunSpread),Random.Range(shotgunSpread,-shotgunSpread),0)) *shotgunBulletSpeed;
            bullet.GetComponent<BulletController>().setup(shotgunBulletDamange,isFriendly,shotgunBulletLifeSpan);
            bullet.transform.localScale *= shotgunBulletSize;
        }
    }

    void SniperBullet() {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*sniperBulletSpeed;
        bullet.GetComponent<BulletController>().setup(sniperBulletDamange,isFriendly,sniperBulletLifeSpan);
        bullet.transform.localScale *= sniperBulletSize;
    }

}

}