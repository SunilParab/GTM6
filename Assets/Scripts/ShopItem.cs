using UnityEngine;

namespace PlayerControls {

public class ShopItem : ScriptableObject
{
    [SerializeField]
    protected int cost  = 0;

    public virtual void Use() {
        
    }
}

}