using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerSkillSO[] activeSkills = new PlayerSkillSO[3];
    [SerializeField] private int[] currentCooltimes = new int[3];
    [SerializeField] private CoolText[] coolTexts = new CoolText[3];
    [SerializeField] private PlayerPerkSO[] allPerks = new PlayerPerkSO[9];
    public StateController Stat;
    public PlayerPerkSO[] selectedPerks = new PlayerPerkSO[3];
    private IObjectPool<AllyMove>[] bulletPool = new IObjectPool<AllyMove>[3];
    public int deadeyecomb;
    public int practiceStack;
    public int bloodPointStack;

    private void Start()
    {
        if (TurnManage.instance != null)
            TurnManage.instance.Turn += TurnStart;

        string jsondata = File.ReadAllText(Path.Combine(Application.persistentDataPath, "saveData.json"));
        PerkData data = JsonUtility.FromJson<PerkData>(jsondata);

        for(int j = 0; j < selectedPerks.Length; j++)
        {
            {
                for(int i = 0; i < allPerks.Length; i++)
                {
                    if(data.equippedPerks != null && allPerks[i] != null)
                    {
                        if(allPerks[i].perkname == data.equippedPerks[j])
                        {
                            selectedPerks[j] = allPerks[i];
                            selectedPerks[j].EquipPerk(this);
                            break;
                        }
                    }
                }
            }
        }

        Stat.SetPerk(selectedPerks);
        
        for(int i = 0; i < 3; i++)
        {
            int currentIndex = i;

            if(activeSkills[currentIndex] != null && activeSkills[currentIndex].skillPrefab != null)
            {
                bulletPool[currentIndex] = new ObjectPool<AllyMove>(
                    createFunc: () => Instantiate(activeSkills[currentIndex].skillPrefab), // 풀에 총알이 부족할 때 새로 생성하는 방법
                    actionOnGet: (bullet) => bullet.gameObject.SetActive(true), // 풀에서 꺼낼 때 할 행동 (활성화)
                    actionOnRelease: (bullet) => bullet.gameObject.SetActive(false), // 풀로 돌려보낼 때 할 행동 (비활성화)
                    actionOnDestroy: (bullet) => Destroy(bullet.gameObject), // 풀의 최대치를 넘었을 때 할 행동 (파괴)
                    defaultCapacity: 20, // 기본으로 만들어둘 개수
                    maxSize: 100 // 풀이 보관할 수 있는 최대 개수
                    );
            }
        }
    }
    private void OnDestroy()
    {
        if (TurnManage.instance != null)
            TurnManage.instance.Turn -= TurnStart;
    }

    private void OnSkill1(InputValue value) { ExecuteSkill(0); }
    private void OnSkill2(InputValue value) { ExecuteSkill(1); }
    private void OnSkill3(InputValue value) { ExecuteSkill(2); }

    private void ExecuteSkill(int index)
    {
        if(activeSkills[index] != null)
        {
            if(currentCooltimes[index] <= 0)
            {
                activeSkills[index].UseSkill(move.playerx, Stat, bulletPool[index], index, this);

                currentCooltimes[index] = activeSkills[index].cooltime;
                coolTexts[index].UpdateText(currentCooltimes[index]);
            }
        }
    }

    private void TurnStart()
    {
        for(int i = 0; i < 3; i++)
        {
            if(currentCooltimes[i] > 0)
                currentCooltimes[i] -= 1;
            coolTexts[i].UpdateText(currentCooltimes[i]);
        }
    }

    public void SetCool(int num) { currentCooltimes[num] = 0; coolTexts[num].UpdateText(currentCooltimes[num]); }
}
