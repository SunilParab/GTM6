using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Guns {

public class GunController : MonoBehaviour
{

    //Gun Variables
    public enum GunType {Pistol,Shotgun,Sniper}

    //Pistol Variables
    const float pistolBulletSpeed = 80;
    const float pistolBulletDamange = 20;
    const float pistolBulletLifeSpan = 0.75f;
    const float pistolBulletSize = 0.5f;
    const float pistolReloadTime = 0.25f;

    //Cannon Variables
    const float shotgunBulletSpeed = 160;
    const float shotgunBulletDamange = 10;
    const float shotgunBulletLifeSpan = 0.5f;
    const float shotgunBulletSize = 0.5f;
    const float shotgunReloadTime = 0.5f;
    const int shotgunPelletNum = 12;
    const float shotgunSpread = 0.1f;

    //Sniper Variables
    const float sniperBulletSpeed = 300;
    const float sniperBulletDamange = 100;
    const float sniperBulletLifeSpan = 1;
    const float sniperBulletSize = 1;
    const float sniperReloadTime = 1f;

    //Shooting Variables
    [SerializeField]
    public GunType curGun = GunType.Pistol;
    float reloadTime = pistolReloadTime;
    bool reloaded = true;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    bool isFriendly;

    public bool Shoot()
    {
        if (reloaded) {
            Fire();
            return true;
        } else {
            return false;
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

    public void SetGun(GunType newType) {
        switch (newType) {
            case GunType.Pistol:
                curGun = GunType.Pistol;
                reloadTime = pistolReloadTime;
                break;
            case GunType.Shotgun:
                curGun = GunType.Shotgun;
                reloadTime = shotgunReloadTime;
                break;
            case GunType.Sniper:
                curGun = GunType.Sniper;
                reloadTime = sniperReloadTime;
                break;
        }
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

            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.Normalize(bullet.transform.forward+bullet.transform.right*Random.Range(shotgunSpread,-shotgunSpread)+bullet.transform.up*Random.Range(shotgunSpread,-shotgunSpread)) *shotgunBulletSpeed;
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

    //Function used to nerf npcs
    public void DoubleReload() {
        reloadTime *= 2;
    }

}

}