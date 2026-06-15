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

    public void FinishRun(bool ishit)
    {
        if(!ishit)
        {
            foreach(PlayerPerkSO perk in SO.perks)
            {
                if(perk != null)
                    perk.OnMissHit();
                
            }
        }
        SO.skillpool.Release(this);
    }

    public int OnHit()
    {
        float mul = 1f; // 배수
        foreach(PlayerPerkSO perk in SO.perks) // 퍽 3개 모두에게
        {
            if(perk != null)
                mul += perk.OnBulletHit(); // OnBulletHit 호출
        }
        return (int)(SO.dmg * mul); // 추가 배수만큼 곱해서 반환
    }

    protected virtual void TurnStart()
    {
    }
}
