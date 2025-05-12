using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "Dumbell", menuName = "Scriptable Objects/Dumbell")]
public class Dumbell : ShopItem
{
    public override void Use() {
        if (PlayerController.reference.SpendMoney(cost)) {
            PlayerController.reference.maxHealth += 50;
        }
    }
}

}