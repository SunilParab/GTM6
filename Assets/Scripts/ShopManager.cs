using System.Collections.Generic;
using PlayerControls;
using UnityEngine;

namespace Shop {

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopDisplay;

    [SerializeField]
    List<ShopItem> items;

    public void Awake() {
        shopDisplay = GameObject.Find("ShopUI");
        shopDisplay.SetActive(false);
    }

    public void OpenShop() {
        shopDisplay.SetActive(true);
    }

    public void CloseShop() {
        shopDisplay.SetActive(false);
    }

    public void Purchase(int index) {
        items[index].Use();
    }

}

}