using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemySO SO;
    public EnemyPointerEnter pointer;
    protected int gridx;
    protected int gridy;
    protected StateController Stat; 
    private void OnEnable()
    {
        TurnManage.instance.Turn += TurnStart;
    }
    private void OnDisable()
    {
        TurnManage.instance.Turn -= TurnStart;
        Stat.EnemyClearPosition(this, gridx, gridy);
    }
    
    public void SetStat(StateController _Stat)
    {
        Stat = _Stat;
    }

    public void Init(int startx, int starty)
    {
        gridx = startx;
        gridy = starty;
    }

    protected virtual void TurnStart()
    {
    }

    public void FinishRun()
    {
        Stat.HP -= SO.damage;
        Stat.killedEnemy += 1;
        Stat.textUI.SetEnemyText();
        Stat.textUI.SetAllyHPText();
        gameObject.SetActive(false);
    }
}
