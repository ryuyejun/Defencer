using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemySO SO;
    private int prevTile;
    private void OnEnable()
    {
        TurnManage.instance.Turn += TurnStart;
    }
    private void OnDisable()
    {
        TurnManage.instance.Turn -= TurnStart;
    }

    private void TurnStart()
    {
        transform.position -= new Vector3(6f * SO.speed, 0f, 0f);
        prevTile += SO.speed;
        if(prevTile >= 10)
        {
            Stat.instance.HP -= SO.damage;
            Stat.instance.killedEnemy += 1;
            Stat.instance.textUI.SetEnemyText();
            Stat.instance.textUI.SetAllyHPText();
            gameObject.SetActive(false);
        }
    }
}
