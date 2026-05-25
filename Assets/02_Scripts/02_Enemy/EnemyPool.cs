using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    public List<WaveSO> waveList = new List<WaveSO>();
    [SerializeField] private TurnManage turn;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
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
        Stat.instance.killedEnemy = 0;
        enemyNum = 0;
    }

    private void Start()
    {
        StartWave(0);
        for(int i = 0; i < currentWaveData.enemyList.Count; i++)
        {
            EnemyMove enemy = Instantiate(currentWaveData.enemyList[i].enemyPrefab);
            enemy.SO = currentWaveData.enemyList[i];
            enemies.Add(enemy.gameObject);
            enemy.gameObject.SetActive(false);
        }
    }


    private void TurnStart()
    {
        if(enemyNum >= currentWaveData.enemyList.Count) return;
        if(turnDelay <= 0)
        {
            enemies[enemyNum].transform.position = new Vector3(35f, 5f, enemySlot[Random.Range(0, 5)] * 6);
            enemies[enemyNum].SetActive(true);
            turnDelay = currentWaveData.enemyList[enemyNum].spawndelay;
            enemyNum += 1;
        }
        turnDelay -= 1;
    }
}
