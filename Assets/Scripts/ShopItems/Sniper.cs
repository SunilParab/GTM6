using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "Sniper", menuName = "Scriptable Objects/Sniper")]
public class Sniper: ShopItem
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