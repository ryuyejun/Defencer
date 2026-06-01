using UnityEngine;
using UnityEngine.Pool;

public abstract class PlayerSkillSO : ScriptableObject
{
    public int dmg;
    public AllyMove skillPrefab;
    public IObjectPool<AllyMove> skillpool;
    protected int cooltime;
    public int speed;

    public virtual void UseSkill(int firsty, StateController stat, IObjectPool<AllyMove> pool)
    {
        skillpool = pool;
    }
    public abstract void TurnStart();
}