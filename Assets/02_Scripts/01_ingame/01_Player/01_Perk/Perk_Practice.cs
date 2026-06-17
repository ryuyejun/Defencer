using UnityEngine;

[CreateAssetMenu(fileName = "Perk_Practice", menuName = "SO/Perk/Practice")]
public class Perk_Practice : PlayerPerkSO
{
    public override float OnBulletHit()
    {
        player.practiceStack += 1;
        return player.practiceStack / 20;
    }
    public override float OnSwordHit(EnemyMove enemy, StateController stat)
    {
        player.practiceStack += 1;
        return player.practiceStack / 20;
    }
    public override float OnTrapHit(int turn)
    {
        player.practiceStack += 1;
        return player.practiceStack / 20;
    }
}
