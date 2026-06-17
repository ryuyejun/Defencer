using UnityEngine;

[CreateAssetMenu(fileName = "Perk_SharpenedSword", menuName = "SO/Perk/SharpenedSword")]
public class Perk_SharpenedSword : PlayerPerkSO
{
    public override float OnSwordHit(EnemyMove enemy, StateController stat)
    {
        enemy.fatal += 2;
        return 0;
    }
}
