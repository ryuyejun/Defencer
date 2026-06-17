using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Trap", menuName = "SO/Skills/Trap")]
public class Trap : PlayerSkillSO
{
    private int gridx;
    private int gridy;
    private PlayerAttack player;
    private int skillIndex;

    public override bool UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool, int index, PlayerAttack caster)
    {
        base.UseSkill(firsty, stat, pool, index, caster);
        stat.SelectTile(SetTile);
        player = caster;
        skillIndex = index;

        return true;
    }

    private void SetTile(Vector2 target)
    {
        gridx = (int)target.x;
        gridy = (int)target.y;
        SetTrap();
    }

    private void SetTrap()
    {
        AllyMove trap = skillpool.Get();
        trap.SetStat(stat, this);
        if(stat.InitTrap(trap, gridx + 1, gridy))
        {
            trap.Init(gridx, gridy);
            trap.transform.position = new Vector3(35f - ((gridx + 1) * 6f), 2f, 12 - gridy * 6f);
        }
        else
        {
            Debug.Log("설치실패");
            skillpool.Release(trap);
            player.SetCool(skillIndex);
        }
    }

}
