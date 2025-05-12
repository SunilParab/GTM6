using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "Shotgun", menuName = "Scriptable Objects/Shotgun")]
public class Shotgun : ShopItem
{
    public override void Use() {
        //Stop rebuy
        if (PlayerController.reference.hasShotgun) {return;}
        
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.hasShotgun = true;
        }
    }
}

}