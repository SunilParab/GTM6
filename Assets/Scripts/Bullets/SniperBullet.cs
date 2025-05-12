using UnityEngine;

namespace Guns {

public class SniperBullet : Bullet
{

    public override void Init() {

        bulletSpeed = 300;
        bulletDamange = 100;
        bulletLifeSpan = 1;
        bulletSize = 1;
        reloadTime = 1f;
        pelletNum = 1;
        spread = 0.1f;
    }

}

}