using UnityEngine;

namespace PlayerControls {

[CreateAssetMenu(fileName = "Burger", menuName = "Scriptable Objects/Burger")]
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

}