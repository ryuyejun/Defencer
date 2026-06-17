using UnityEngine;

[CreateAssetMenu(fileName = "Perk_Causticity", menuName = "SO/Perk/Causticity")]
public class Perk_Causticity : PlayerPerkSO
{
    public override void OnTrapHit(EnemyMove enemy, int dmg)
    {
        enemy.decayPower += dmg;
        enemy.decayCount += 2;
    }
}
