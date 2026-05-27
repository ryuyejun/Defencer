using UnityEngine;
using TMPro;

public class ConfigTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text leftEnemyText;
    [SerializeField] private TMP_Text allyHPText;
    [SerializeField] private EnemyPool pool;
    [SerializeField] private StateController Stat;

    private void Start()
    {
        SetEnemyText();
        SetAllyHPText();
    }

    public void SetEnemyText() => leftEnemyText.text = $"{Stat.killedEnemy} / {pool.currentWaveData.enemyList.Count}";
    public void SetAllyHPText() => allyHPText.text = $"{Stat.HP}";
}
