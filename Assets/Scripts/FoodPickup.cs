using UnityEngine;

namespace Items {

public class FoodPickup : ItemBehavior
{
    [SerializeField]
    int value = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    protected override void Collect() {
        PlayerControls.PlayerController.reference.Heal(value);
    }

}

}