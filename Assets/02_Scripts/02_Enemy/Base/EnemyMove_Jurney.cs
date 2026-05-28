using UnityEngine;

public class EnemyMove_Jurney : EnemyMove
{
    private int dir = -1;
    private bool firstmove = true;
    protected override void TurnStart()
    {
        int targetx = gridx;
        int targety = gridy + (SO.speed * dir);
        if(firstmove)
        {
            targetx = gridx + SO.speed;
            targety = gridy;
        }

        if(Stat.UpdatePosition(this, gridx, gridy, targetx, targety))
        {
            gridx = targetx;
            gridy = targety;
            firstmove = false;
        }
        else
        {
            targetx = gridx + SO.speed;
            targety = gridy;
            if(Stat.UpdatePosition(this, gridx, gridy, targetx, targety))
            {
                gridx = targetx;
                gridy = targety;
                dir = -dir;
            }
            else Debug.Log("이동 실패");
        }
        // 부모의 TurnStart에 아무것도 안 넣었으니 주석 처리
        // base.TurnStart();
    }
}
