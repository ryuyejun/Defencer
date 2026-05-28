using UnityEngine;

public class EnemyMoveForward : EnemyMove
{
    protected override void TurnStart()
    {
        int targetx = gridx + SO.speed;
        int targety = gridy;

        if(Stat.UpdatePosition(this, gridx, gridy, targetx, targety))
        {
            gridx = targetx;
            gridy = targety;
        }
        else
            Debug.Log("이동 실패");
        // 부모의 TurnStart()에 아무것도 안 넣었으니 주석 처리
        // base.TurnStart();
    }
}
