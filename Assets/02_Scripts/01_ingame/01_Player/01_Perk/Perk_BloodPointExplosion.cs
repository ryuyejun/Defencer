using UnityEngine;

[CreateAssetMenu(fileName = "Perk_BloodPointExplosion", menuName = "SO/Perk/BloodPointExplosion")]
public class Perk_BloodPointExplosion : PlayerPerkSO
{
    public override float OnBulletHit()
    {
        if(player.bloodPointStack >= 3)
            return 0.75f;
        player.bloodPointStack += 1;
        return 0;
    }
    public override float OnSwordHit(EnemyMove enemy, StateController stat)
    {
        if(player.bloodPointStack >= 3)
            return 0.75f;
        player.bloodPointStack += 1;
        return 0;
    }
    public override float OnTrapHit(int turn)
    {
        if(player.bloodPointStack >= 3)
            return 0.75f;
        player.bloodPointStack += 1;
        return 0;
    }
}
