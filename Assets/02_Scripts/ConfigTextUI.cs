using UnityEngine;
using TMPro;

public class ConfigTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text leftEnemyText;
    [SerializeField] private int killedEnemy = 0;
    [SerializeField] private TMP_Text allyHPText;
    [SerializeField] private int HP;
    [SerializeField] private EnemyPool pool;

    private void Start()
    {
        SetEnemyText();
        SetAllyHPText();
    }

    public void SetEnemyText() => leftEnemyText.text = $"{killedEnemy} / {pool.currentWaveData.enemyList.Count}";
    public void SetAllyHPText() => allyHPText.text = $"{HP}";
}
