using UnityEngine;

// 퍽들의 부모 SO기 때문에 CreateAssetMenu를 쓰지 않음
public class PlayerPerkSO : ScriptableObject
{
    public string perkname; // 이름
    public string perkdisc;
    protected PlayerAttack player;

    public void EquipPerk(PlayerAttack caster)
    {
        player = caster;
    }

    public virtual float OnBulletHit()
    {
        return 0;
    }

    public virtual float OnBulletHit(EnemyMove enemy)
    {
        return 0;
    }

    public virtual void OnBulletMissHit()
    {
    }

    public virtual float OnSwordHit(EnemyMove enemy, StateController stat)
    {
        return 0;
    }

    public virtual float OnTrapHit(int turn)
    {
        return 0;
    }

    public virtual void OnTrapHit(EnemyMove enemy, int dmg)
    {

    }

    public virtual void EnemyDie()
    {

    }

    public virtual void TurnStart()
    {

    }
}
