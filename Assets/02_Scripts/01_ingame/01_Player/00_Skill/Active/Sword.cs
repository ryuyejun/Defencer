using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Sword", menuName = "SO/Skills/Sword")]
public class Sword : PlayerSkillSO
{
    private int gridx;
    private int?[] gridy = new int?[3];
    private PlayerAttack player;
    private int skillIndex;

    public override bool UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool, int index, PlayerAttack caster)
    {
        base.UseSkill(firsty, stat, pool, index, caster);
        gridy[0] = (firsty + 1 <= 2) ? -(firsty + 1 - 2) : null;
        gridy[1] = -(firsty - 2);
        gridy[2] = (firsty - 1 >= -2) ? -(firsty - 1 - 2) : null;
        
        gridx = 9;

        AllyMove sword = pool.Get();
        sword.SetStat(stat, this);

        stat.AllySwordAttack(sword, gridx, gridy);

        pool.Release(sword);
        return true;
    }
}