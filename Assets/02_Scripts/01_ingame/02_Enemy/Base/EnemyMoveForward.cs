using UnityEngine;

public class EnemyMoveForward : EnemyMove
{
    protected override void TurnStart()
    {
        int targetx = gridx + SO.speed;
        int targety = gridy;

        if(Stat.EnemyUpdatePosition(this, gridx, gridy, targetx, targety))
        {
            gridx = targetx;
            gridy = targety;
        }
        else
            Debug.Log("이동 실패");
    }
}
