using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "Boost", menuName = "Scriptable Objects/Boost")]
public class Boost : ShopItem
{
    public override void Use() {
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.GainBoost();
        }
    }
}

}