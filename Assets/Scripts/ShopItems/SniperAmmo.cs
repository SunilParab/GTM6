using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "SniperAmmo", menuName = "Scriptable Objects/SniperAmmo")]
public class SniperAmmo : ShopItem
{
    public override void Use() {
        //Stop overbuy
        if (PlayerController.reference.sniperAmmo == PlayerController.maxSniperAmmo) {return;}

        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainAmmo(Guns.GunController.GunType.Sniper,10);
        }
    }
}

}