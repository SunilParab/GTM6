using UnityEngine;

namespace PlayerControls {

public class ShopItem : ScriptableObject
{
    [SerializeField]
    protected int cost  = 0;

    public virtual void Use() {
        
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Burger", order = 0)]
public class Burger : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.health == PlayerController.reference.maxHealth) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.health = PlayerController.reference.maxHealth;
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Dumbell", order = 1)]
public class Dumbell : ShopItem
{
    public override void Use() {
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.maxHealth += 50;
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Boost", order = 2)]
public class Boost : ShopItem
{
    public override void Use() {
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainBoost();
        }
    }
}


[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/PistolAmmo", order = 4)]
public class PistolAmmoRefill : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.pistolAmmo == PlayerController.maxPistolAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Pistol,100);
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShotgunAmmo", order = 3)]
public class ShotgunAmmoRefill : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.shotgunAmmo == PlayerController.maxShotgunAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Shotgun,20);
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/SniperAmmo", order = 5)]
public class SniperAmmoRefill : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.sniperAmmo == PlayerController.maxSniperAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Sniper,10);
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shotgun", order = 6)]
public class ShotgunAmmo : ShopItem
{
    public override void Use() {
        //Stop rebuy
        if (PlayerController.reference.hasShotgun) {return;}
        
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.hasShotgun = true;
        }
    }
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Sniper", order = 7)]
public class SniperAmmo: ShopItem
{
    public override void Use() {
        //Stop rebuy
        if (PlayerController.reference.hasSniper) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.hasSniper = true;
        }
    }
}

}