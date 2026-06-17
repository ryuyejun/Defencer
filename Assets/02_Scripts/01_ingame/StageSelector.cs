using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageData
{
    public int wavenum;
}

public class StageSelector : MonoBehaviour
{
    [SerializeField] private WaveSO[] wavearray = new WaveSO[5];
    private WaveSO selectedwave;
    private string savePath;

    private void Start() => savePath = Path.Combine(Application.persistentDataPath, "waveData.json"); // 가장 안전한 장소

    public void OnStageButton(int index)
    {
        selectedwave = wavearray[index];
        Onconfirm();
    }

    private void Onconfirm()
    {
        StageData data = new StageData(); // 데이터에 객체 생성
        if(selectedwave != null)
            data.wavenum = selectedwave.num;
        string json = JsonUtility.ToJson(data, true); // json으로 저장

        File.WriteAllText(savePath, json); // 이거 쓰면 이미 UTF-8임, 저장 경로에 이 json 파일 저장

        SceneManager.LoadScene("ready");
    }
}
