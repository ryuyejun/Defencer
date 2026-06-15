using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PerkData
{
    public string[] equippedPerks = new string[3];
}

public class PerkSelect : MonoBehaviour
{
    [SerializeField] private string savePath; // 저장 경로
    [SerializeField] private PlayerPerkSO[] perks = new PlayerPerkSO[8]; // 퍽 총 8개(늘어날 수 있음)
    [SerializeField] private TMP_Text selectText; // 선택 할 때 표시할 텍스트
    [SerializeField] private TMP_Text[] slotText = new TMP_Text[3]; // 슬롯 텍스트
    private PlayerPerkSO[] equipPerks = new PlayerPerkSO[3]; // 장착한 퍽 저장
    private PlayerPerkSO selectedPerk; // 선택한 퍽

    private void Start() => savePath = Path.Combine(Application.persistentDataPath, "saveData.json"); // 가장 안전한 장소

    public void OnPerkClick(int index)
    {
        selectedPerk = perks[index]; // 클릭할 때 색인을 받아서 씀(하드코딩;;)
        selectText.text = $"선택 : {selectedPerk.perkname}";
    }

    public void OnSlotClick(int index)
    {
        if(selectedPerk != null) // 퍽 선택 시 그 자리에 선택한 퍽 장착
        {
            equipPerks[index] = selectedPerk;
            slotText[index].text = $"{index + 1}번 슬롯\n{selectedPerk.perkname}";
        }
        else // 퍽 선택 하지 않을 시 해제
        {
            equipPerks[index] = null;
            slotText[index].text = $"{index + 1}번 슬롯";
        }
    }

    public void GameStart() // 게임시작 버튼 눌렀을 때
    {
        PerkData data = new PerkData(); // 데이터에 객체 생성
        for(int i = 0; i < data.equippedPerks.Length; i++)
        {
            if(equipPerks[i] != null)
                data.equippedPerks[i] = equipPerks[i].perkname;
            else
                data.equippedPerks[i] = "";
        }

        string json = JsonUtility.ToJson(data, true); // json으로 저장

        File.WriteAllText(savePath, json); // 이거 쓰면 이미 UTF-8임, 저장 경로에 이 json 파일 저장

        SceneManager.LoadScene("ingame");
    }
}
