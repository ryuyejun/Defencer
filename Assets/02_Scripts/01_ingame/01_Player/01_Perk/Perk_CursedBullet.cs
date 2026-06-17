using UnityEngine;

[CreateAssetMenu(fileName = "Perk_CursedBullet", menuName = "SO/Perk/CursedBullet")]
public class Perk_CursedBullet : PlayerPerkSO
{

    public override float OnBulletHit(EnemyMove enemy)
    {
        if(enemy.decayPower >= 1 || enemy.fatal >= 1 || enemy.decayCount >= 1)
            return 0.60f;
        return 0f;
    }
}
