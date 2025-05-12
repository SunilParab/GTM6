using UnityEngine;

namespace Guns {

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Objects/Bullet")]
public class Bullet : ScriptableObject
{

    public float bulletSpeed = 0;
    public float bulletDamange = 0;
    public float bulletLifeSpan = 0;
    public float bulletSize = 0;
    public float reloadTime = 0;
    public int pelletNum = 0;
    public float spread = 0;

    public virtual void Init() {

    }

}

}