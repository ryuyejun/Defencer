using UnityEngine;
using UnityEngine.Pool;

public abstract class PlayerSkillSO : ScriptableObject
{
    public string skillname;
    public SkillType type;
    public int dmg;
    public AllyMove skillPrefab;
    public IObjectPool<AllyMove> skillpool;
    public int cooltime;
    public int speed;
    public PlayerPerkSO[] perks = new PlayerPerkSO[3];
    protected StateController stat;

    public virtual bool UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool, int index, PlayerAttack caster)
    {
        skillpool = pool;
        for(int i = 0; i < perks.Length; i++)
            perks[i] = caster.selectedPerks[i];
        this.stat = stat;
        return true;
    }
}

public enum SkillType
{
    bullet,
    trap,
    sword
}