using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "ShotgunAmmo", menuName = "Scriptable Objects/ShotgunAmmo")]
public class ShotgunAmmo : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.shotgunAmmo == PlayerController.maxShotgunAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Shotgun,20);
        }
    }
}

}