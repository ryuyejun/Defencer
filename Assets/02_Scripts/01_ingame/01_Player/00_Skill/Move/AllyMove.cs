using UnityEngine;

public class AllyMove : MonoBehaviour
{
    protected int gridx;
    protected int gridy;
    protected PlayerSkillSO SO;
    protected StateController stat;
    [SerializeField] private int passTurn;
    private void OnEnable()
    {
        TurnManage.instance.Turn += TurnStart;
    }
    private void OnDisable()
    {
        TurnManage.instance.Turn -= TurnStart;
        stat.AllyClearPosition(this, gridx, gridy);
    }
    
    public void SetStat(StateController _Stat, PlayerSkillSO _SO)
    {
        stat = _Stat;
        SO = _SO;
    }

    public void Init(int startx, int starty)
    {
        gridx = startx;
        gridy = starty;
    }

    public void FinishRun(bool ishit)
    {
        if(!ishit)
        {
            foreach(PlayerPerkSO perk in stat.perks)
            {
                if(perk != null)
                {
                    if(SO.type == SkillType.bullet)
                        perk.OnBulletMissHit();
                }
                
            }
        }
        SO.skillpool.Release(this);
    }

    public int OnHit(EnemyMove enemy)
    {
        float mul = 1f; // 배수
        foreach(PlayerPerkSO perk in stat.perks) // 퍽 3개 모두에게
        {
            if(perk != null)
            {
                if(SO.type == SkillType.bullet)
                {
                    mul += perk.OnBulletHit(); // OnBulletHit 호출
                    mul += perk.OnBulletHit(enemy);
                }
                if(SO.type == SkillType.sword)
                    mul += perk.OnSwordHit(enemy, stat);
                if(SO.type == SkillType.trap)
                {
                    mul += perk.OnTrapHit(passTurn);
                    perk.OnTrapHit(enemy, SO.dmg);
                }
            }
        }
        return (int)(SO.dmg * mul); // 추가 배수만큼 곱해서 반환
    }

    protected virtual void TurnStart()
    {
        foreach(PlayerPerkSO perk in stat.perks) // 퍽 3개 모두에게
        {
            if(perk != null)
                perk.TurnStart();
        }
        passTurn += 1;
    }
}
