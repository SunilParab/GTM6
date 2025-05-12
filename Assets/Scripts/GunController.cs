using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Guns {

public class GunController : MonoBehaviour
{

    //Gun Variables
    public enum GunType {Pistol,Shotgun,Sniper}

    Bullet myBullet;

    //Shooting Variables
    [SerializeField]
    public GunType curGun = GunType.Pistol;
    float reloadTime;
    bool reloaded = true;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    bool isFriendly;

    public void Init() {
        SetGun(curGun);
        reloadTime = myBullet.reloadTime;
    }

    //Function used to nerf npcs
    public void InitDoubleReload() {
        SetGun(curGun);
        reloadTime = myBullet.reloadTime;
        reloadTime *= 2;
    }

    public bool Shoot() {
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
                myBullet = ScriptableObject.CreateInstance<PistolBullet>();
                myBullet.Init();
                reloadTime = myBullet.reloadTime;
                break;
            case GunType.Shotgun:
                curGun = GunType.Shotgun;
                myBullet = ScriptableObject.CreateInstance<ShotgunBullet>();
                myBullet.Init();
                reloadTime = myBullet.reloadTime;
                break;
            case GunType.Sniper:
                curGun = GunType.Sniper;
                myBullet = ScriptableObject.CreateInstance<SniperBullet>();
                myBullet.Init();
                reloadTime = myBullet.reloadTime;
                break;
        }
    }

    void Reload() {
        reloaded = true;
    }

    void PistolBullet() {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*myBullet.bulletSpeed;
        bullet.GetComponent<BulletController>().setup(myBullet.bulletDamange,isFriendly,myBullet.bulletLifeSpan);
        bullet.transform.localScale *= myBullet.bulletSize;
    }

    void ShotgunShot() {

        for (int i = 0; i < myBullet.pelletNum; i++) {
            GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.Normalize(bullet.transform.forward+bullet.transform.right*Random.Range(myBullet.spread,-myBullet.spread)+bullet.transform.up*Random.Range(myBullet.spread,-myBullet.spread)) * myBullet.bulletSpeed;
            bullet.GetComponent<BulletController>().setup(myBullet.bulletDamange,isFriendly,myBullet.bulletLifeSpan);
            bullet.transform.localScale *= myBullet.bulletSize;
        }
    }

    void SniperBullet() {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward*myBullet.bulletSpeed;
        bullet.GetComponent<BulletController>().setup(myBullet.bulletDamange,isFriendly,myBullet.bulletLifeSpan);
        bullet.transform.localScale *= myBullet.bulletSize;
    }

}

}