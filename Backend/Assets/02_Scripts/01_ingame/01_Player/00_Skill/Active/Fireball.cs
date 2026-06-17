using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Fireball", menuName = "SO/Skills/Fireball")]
public class Fireball : PlayerSkillSO
{
    private int gridx;
    private int gridy;
    public override bool UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool, int index, PlayerAttack caster)
    {
        base.UseSkill(firsty, stat, pool, index, caster);
        gridy = firsty;
        gridx = 9;
        AllyMove fireBullet = pool.Get();
        fireBullet.SetStat(stat, this);
        if(stat.InitAlly(fireBullet, gridx, -(firsty - 2)))
        {
            fireBullet.Init(gridx, -(firsty - 2));
            fireBullet.transform.position = new Vector3(35f - (gridx + 1) * 6f, 5f, gridy * 6f);
            return true;
        }
        else
        {
            pool.Release(fireBullet);
            return false;
        }
    }
}
