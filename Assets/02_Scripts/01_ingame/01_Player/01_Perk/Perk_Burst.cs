using UnityEngine;

[CreateAssetMenu(fileName = "Perk_Burst", menuName = "SO/Perk/Burst")]
public class Perk_Interception : PlayerPerkSO
{
    public override float OnSwordHit(EnemyMove enemy, StateController stat)
    {
        if(stat.EnemyUpdatePosition(enemy, enemy.gridx, enemy.gridy, enemy.gridx - 1, enemy.gridy))
            enemy.gridx -= 1;
        return 0.5f;
    }
}
