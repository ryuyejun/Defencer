using UnityEngine;

public class AllyMove_Forward : AllyMove
{
    protected override void TurnStart()
    {
        int targetx = gridx - SO.speed;
        int targety = gridy;

        if(Stat.AllyUpdatePosition(this, gridx, gridy, targetx, targety))
        {
            gridx = targetx;
            gridy = targety;
        }
        else
            Debug.Log("이동 실패");
    }
}
