using UnityEngine;

public class EnemyMove : MonoBehaviour
{

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
        transform.position -= new Vector3(6f, 0f, 0f);
    }
}
