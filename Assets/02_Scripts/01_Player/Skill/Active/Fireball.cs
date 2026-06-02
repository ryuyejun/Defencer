using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Fireball", menuName = "SO/Skills/Fireball")]
public class Fireball : PlayerSkillSO
{
    private int gridx;
    private int gridy;
    public override void UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool)
    {
        base.UseSkill(firsty, stat, pool);
        gridy = firsty;
        gridx = 9;
        AllyMove fireBullet = pool.Get();
        if(stat.InitAlly(fireBullet, gridx, -(firsty - 2)))
        {
            fireBullet.SetStat(stat, this);
            fireBullet.Init(gridx, -(firsty - 2));
            fireBullet.transform.position = new Vector3(35f - gridx * 6f, 5f, gridy * 6f);
        }
        else
        {
            pool.Release(fireBullet);
        }
    }
}
