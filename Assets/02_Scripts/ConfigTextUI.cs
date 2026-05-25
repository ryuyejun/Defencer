using UnityEngine;
using TMPro;

public class ConfigTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text leftEnemyText;
    [SerializeField] private TMP_Text allyHPText;
    [SerializeField] private EnemyPool pool;

    private void Start()
    {
        SetEnemyText();
        SetAllyHPText();
    }

    public void SetEnemyText() => leftEnemyText.text = $"{Stat.instance.killedEnemy} / {pool.currentWaveData.enemyList.Count}";
    public void SetAllyHPText() => allyHPText.text = $"{Stat.instance.HP}";
}
