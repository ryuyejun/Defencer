using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemySO SO;
    public EnemyPointerEnter pointer;
    public int gridx;
    public int gridy;
    public int fatal;
    protected StateController Stat; 
    public int currenthp;
    public int decayPower;
    public int decayCount;

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
        fatal -= 1;
        currenthp -= decayPower / 2;
        decayCount -= 1;
        if(decayCount <= 0)
        {
            decayPower = 0;
            decayCount = 0;
        }
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
        currenthp -= (int)(dmg * (fatal >= 1? 1.5f : 1.0f));
        if(currenthp <= 0)
            OnDie();
    }

    public void OnDie()
    {
        Stat.killedEnemy += 1;
        Stat.textUI.SetAllyHPText();
        Stat.textUI.SetEnemyText();
        Stat.EnemyClearPosition(this, gridx, gridy);
        foreach(PlayerPerkSO perk in Stat.perks) // 퍽 3개 모두에게
        {
            if(perk != null)
                perk.EnemyDie();
        }
        gameObject.SetActive(false);
    }
}
