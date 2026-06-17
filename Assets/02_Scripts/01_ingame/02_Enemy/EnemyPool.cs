using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private RePosBoard board;
    [SerializeField] private TurnManage turn;
    [SerializeField] private StateController Stat;
    [SerializeField] private List<EnemyMove> enemies = new List<EnemyMove>();
    [SerializeField] private int[] enemySlot = new int[5] {2, 1, 0, -1, -2};
    [SerializeField] private List<WaveSO> waves = new List<WaveSO>();
    [SerializeField] private RectTransform clear;
    public WaveSO currentWaveData;
    public int currentWaveNum;
    private int turnDelay;
    private int enemyNum;
    private string savePath; // 저장 경로

    private void OnEnable()
    {
        turn.Turn += TurnStart;
    }

    private void OnDisable()
    {
        turn.Turn -= TurnStart;
    }

    public void StartWave()
    {
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
            enemy.pointer.stat = Stat;
            enemy.gameObject.SetActive(false);
        }
    }

    public void EndWave()
    {
        foreach(EnemyMove enemy in enemies)
            Destroy(enemy.gameObject);
        enemies.Clear();
        Debug.Log($"{currentWaveNum + 1}스테이지 클리어");

        clear.DOKill();
        clear.DOAnchorPos(Vector2.zero, 0.5f);

        PerkData data = new PerkData(); // 데이터에 객체 생성
        for(int i = 0; i < data.equippedPerks.Length; i++)
        {
            data.equippedPerks[i] = "";
        }
        data.wavenum = -1;
        data.isingame = false;

        string json = JsonUtility.ToJson(data, true); // json으로 저장

        File.WriteAllText(savePath, json); // 이거 쓰면 이미 UTF-8임, 저장 경로에 이 json 파일 저장
        StartCoroutine(Intermission());
    }

    private IEnumerator Intermission()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("waveselect");
    }

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json"); // 가장 안전한 장소
        string jsondata = File.ReadAllText(Path.Combine(Application.persistentDataPath, "saveData.json"));
        PerkData data = JsonUtility.FromJson<PerkData>(jsondata);
        currentWaveNum = data.wavenum;
        foreach(WaveSO wave in waves)
        {
            if(wave.num == data.wavenum)
                currentWaveData = wave;
        }
        StartWave();
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
