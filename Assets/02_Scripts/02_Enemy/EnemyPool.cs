using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private RePosBoard board;
    [SerializeField] private TurnManage turn;
    [SerializeField] private StateController Stat;
    [SerializeField] private List<EnemyMove> enemies = new List<EnemyMove>();
    public List<WaveSO> waveList = new List<WaveSO>();
    [SerializeField] private int[] enemySlot = new int[5] {2, 1, 0, -1, -2};
    public WaveSO currentWaveData;
    public int currentWaveNum;
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
        if(wavenum >= waveList.Count)
        {
            Debug.Log("게임 클리어");
            return;
        }
        currentWaveData = waveList[wavenum];
        currentWaveNum = wavenum;
        Stat.killedEnemy = 0;
        enemyNum = 0;
        turnDelay = 0;
        for(int i = 0; i < currentWaveData.enemyList.Count; i++)
        {
            EnemyMove enemy = Instantiate(currentWaveData.enemyList[i].enemyPrefab);
            enemy.SO = currentWaveData.enemyList[i];
            enemy.SetStat(Stat);
            enemies.Add(enemy);
            enemy.pointer.backboard = board;
            enemy.gameObject.SetActive(false);
        }
    }

    public void EndWave()
    {
        foreach(EnemyMove enemy in enemies)
            Destroy(enemy.gameObject);
        enemies.Clear();
        Debug.Log($"{currentWaveNum + 1}번째 웨이브 클리어");

        StartCoroutine(WaveEndWait(currentWaveNum));
    }
    private IEnumerator WaveEndWait(int wavenum)
    {
        yield return new WaitForSeconds(10f);

        Debug.Log($"{wavenum + 2}번째 웨이브 시작");
        StartWave(wavenum + 1);
    }

    private void Start()
    {
        StartWave(0);
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
