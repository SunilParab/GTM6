using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "PistolAmmo", menuName = "Scriptable Objects/PistolAmmo")]
public class PistolAmmo : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.pistolAmmo == PlayerController.maxPistolAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Pistol,100);
        }
    }
}

}