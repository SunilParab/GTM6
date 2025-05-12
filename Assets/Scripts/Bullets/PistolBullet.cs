using UnityEngine;

namespace Guns {

class PistolBullet : Bullet
{

    public override void Init() {

        bulletSpeed = 80;
        bulletDamange = 20;
        bulletLifeSpan = 0.75f;
        bulletSize = 0.5f;
        reloadTime = 0.25f;
        pelletNum = 1;
        spread = 0;
    }

}

}