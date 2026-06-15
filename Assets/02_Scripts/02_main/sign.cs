using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
// POST 전달하는거 뺴공 다 같음
public class sign : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField pw;
    [SerializeField] private string url;
    [SerializeField] private GameObject warningBoard;
    [SerializeField] private TMP_Text warningtext;

    public void OnSign()
    {
        string id = userName.text;
        string pass = pw.text;

        if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pass))
        {
            Debug.Log("id와 비빌번호는 비울 수 없습니다.");
            warningBoard.SetActive(true);
            warningtext.text = "id와 비밀번호는 비울 수 없습니다.";
            return;
        }

        UserData data = new UserData{nickname = id, password = pass};
        StartCoroutine(GetLogin(data));
    }

    private IEnumerator GetLogin(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST"); // POST
        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
            Debug.Log($"성공 : {req.downloadHandler.text}");
        else
        {
            warningBoard.SetActive(true);
            warningtext.text = req.error;
        }
    }

}