using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemySO SO;
    public EnemyPointerEnter pointer;
    public int gridx;
    public int gridy;
    protected StateController Stat; 
    [SerializeField] private int currenthp;

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
        currenthp = SO.maxhp;
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

    public void GetHit(int dmg)
    {
        currenthp -= dmg;
        if(currenthp <= 0)
            OnDie();
    }

    public void OnDie()
    {
        Stat.killedEnemy += 1;
        Stat.textUI.SetAllyHPText();
        Stat.EnemyClearPosition(this, gridx, gridy);
        gameObject.SetActive(false);
    }
}
