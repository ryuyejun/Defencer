using UnityEngine;

public class EnemyMove_Jurney : EnemyMove
{
    private int dir = -1;
    private bool firstmove = true;
    protected override void TurnStart()
    {
        base.TurnStart();
        int targetx = gridx;
        int targety = gridy + (SO.speed * dir);
        if(firstmove)
        {
            targetx = gridx + SO.speed;
            targety = gridy;
        }

        if(Stat.EnemyUpdatePosition(this, gridx, gridy, targetx, targety))
        {
            gridx = targetx;
            gridy = targety;
            firstmove = false;
        }
        else
        {
            targetx = gridx + SO.speed;
            targety = gridy;
            if(Stat.EnemyUpdatePosition(this, gridx, gridy, targetx, targety))
            {
                gridx = targetx;
                gridy = targety;
                dir = -dir;
            }
            else Debug.Log("이동 실패");
        }
    }
}
