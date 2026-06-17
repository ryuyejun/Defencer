using System.IO;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class PerkData
{
    public string[] equippedPerks = new string[3];
    public int wavenum;
    public bool isingame;
}

public class PerkSelect : MonoBehaviour
{
    private string savePath; // 저장 경로
    [SerializeField] private PlayerPerkSO[] perks = new PlayerPerkSO[9]; // 퍽 총 8개(늘어날 수 있음)
    [SerializeField] private TMP_Text[] perktexts = new TMP_Text[9];
    [SerializeField] private TMP_Text selectText; // 선택 할 때 표시할 텍스트
    [SerializeField] private TMP_Text[] slotText = new TMP_Text[3]; // 슬롯 텍스트
    [SerializeField] private string url;
    private PlayerPerkSO[] equipPerks = new PlayerPerkSO[3]; // 장착한 퍽 저장
    private PlayerPerkSO selectedPerk; // 선택한 퍽

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json"); // 가장 안전한 장소
        for(int i = 0; i < perks.Length; i++)
        {
            if(perks[i] != null)
                perktexts[i].text = perks[i].perkname;
        }
    }

    public void OnPerkClick(int index)
    {
        selectedPerk = perks[index];
        if(selectedPerk != null)
            selectText.text = $"선택 : {selectedPerk.perkdisc}";
        else
            selectText.text = "선택 : None";
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
        string wavedata = File.ReadAllText(Path.Combine(Application.persistentDataPath, "waveData.json"));
        StageData stagedata = JsonUtility.FromJson<StageData>(wavedata);
        data.wavenum = stagedata.wavenum;
        data.isingame = true;

        string json = JsonUtility.ToJson(data, true); // json으로 저장

        File.WriteAllText(savePath, json); // 이거 쓰면 이미 UTF-8임, 저장 경로에 이 json 파일 저장

        SceneManager.LoadScene("ingame");
        // StartCoroutine(GetDataPost(json));
    }

    private IEnumerator GetDataPost(string json)
    {
        byte[] body = Encoding.UTF8.GetBytes(json); // UTF-8 변환

        UnityWebRequest req = new UnityWebRequest(url, "POST"); // GET을 url 서버에
        req.uploadHandler = new UploadHandlerRaw(body); // 나온 json을 UTF-8로 변환한 것을 업로드 하는 객체
        req.downloadHandler = new DownloadHandlerBuffer(); // 서버에서 반환하는 값 받는 객체

        req.SetRequestHeader("Content-Type", "application/json"); // json현식으로 전달

        yield return req.SendWebRequest(); // 서버 대기

        if(req.result == UnityWebRequest.Result.Success) // 성공
        {
            Debug.Log($"성공 : {req.downloadHandler.text}\n 저장이 성공적으로 완료되었습니다.");

        }
        else // 실패
        {
            Debug.Log(req.error);
        }
    }
}
