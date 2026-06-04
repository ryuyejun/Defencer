using UnityEngine;

public class AllyMove : MonoBehaviour
{
    protected int gridx;
    protected int gridy;
    protected PlayerSkillSO SO;
    protected StateController Stat;
    private void OnEnable()
    {
        TurnManage.instance.Turn += TurnStart;
    }
    private void OnDisable()
    {
        TurnManage.instance.Turn -= TurnStart;
        Stat.AllyClearPosition(this, gridx, gridy);
    }
    
    public void SetStat(StateController _Stat, PlayerSkillSO _SO)
    {
        Stat = _Stat;
        SO = _SO;
    }

    public void Init(int startx, int starty)
    {
        gridx = startx;
        gridy = starty;
    }

    public void FinishRun()
    {
        SO.skillpool.Release(this);
    }

    public int OnHit() { return SO.dmg; }

    protected virtual void TurnStart()
    {
    }
}
