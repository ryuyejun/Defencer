using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private TurnManage turn;
    [SerializeField] private StateController Stat;
    [SerializeField] private List<EnemyMove> enemies = new List<EnemyMove>();
    public List<WaveSO> waveList = new List<WaveSO>();
    [SerializeField] private int[] enemySlot = new int[5] {2, 1, 0, -1, -2};
    public WaveSO currentWaveData;
    private int turnDelay;
    private int enemyNum;

    private void OnEnable()
    {
        turn.Turn += TurnStart;
    }

    private void OnDisable()
    {
        turn.Turn -= TurnStart;
    }

    public void StartWave(int wavenum)
    {
        currentWaveData = waveList[wavenum];
        Stat.killedEnemy = 0;
        enemyNum = 0;
    }

    private void Start()
    {
        StartWave(0);
        for(int i = 0; i < currentWaveData.enemyList.Count; i++)
        {
            EnemyMove enemy = Instantiate(currentWaveData.enemyList[i].enemyPrefab);
            enemy.SO = currentWaveData.enemyList[i];
            enemy.SetStat(Stat);
            enemies.Add(enemy);
            enemy.gameObject.SetActive(false);
        }
    }


    private void TurnStart()
    {
        if(enemyNum >= currentWaveData.enemyList.Count) return;
        if(turnDelay <= 0)
        {
            int randomy = Random.Range(0, 5);
            if(Stat.InitEnemy(enemies[enemyNum], 0, randomy))
            {
                enemies[enemyNum].gameObject.transform.position = new Vector3(35f, 5f, enemySlot[randomy] * 6);
                enemies[enemyNum].gameObject.SetActive(true);
                enemies[enemyNum].Init(0, randomy);
                turnDelay = currentWaveData.enemyList[enemyNum].spawndelay;
                enemyNum += 1;
            }
        }
        if(turnDelay > 0)
            turnDelay -= 1;
    }
}
