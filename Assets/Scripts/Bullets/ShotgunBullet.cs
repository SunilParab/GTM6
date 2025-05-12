using UnityEngine;

namespace Guns {

public class ShotgunBullet : Bullet
{

    public override void Init() {

        bulletSpeed = 160;
        bulletDamange = 10;
        bulletLifeSpan = 0.5f;
        bulletSize = 0.5f;
        reloadTime = 0.5f;
        pelletNum = 12;
        spread = 0.1f;
    }

}

}