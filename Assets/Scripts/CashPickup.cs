using UnityEngine;

namespace Items {

public class CashPickup : ItemBehavior
{
    [SerializeField]
    int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        value = Random.Range(50,100);
    }

    protected override void Collect() {
        PlayerControls.PlayerController.reference.GainMoney(value);
    }

}

}