using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class login : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName; // 인펏에 넣은 id
    [SerializeField] private TMP_InputField pw; // 인펏에 넣은 비밀번호
    [SerializeField] private string url; // url
    [SerializeField] private GameObject warningBoard; // 경고창
    [SerializeField] private TMP_Text warningtext; // 경고 텍스트

    public void OnLogin() // 로그인 버튼
    {
        string id = userName.text; // id는 id 인펏에 넣은 텍스트
        string pass = pw.text; // 비밀번호는 비밀번호 인펏에 넣은 텍스트

        if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pass)) // 비어있으면
        {
            Debug.Log("id와 비빌번호는 비울 수 없습니다.");
            warningBoard.SetActive(true); // 경고창 활성화
            warningtext.text = "id와 비밀번호는 비울 수 없습니다."; // 경고 텍스트
            return; // 예외처리
        }

        UserData data = new UserData{nickname = id, password = pass}; // userdata에 있는 변수 변경하면서 새 객체 생성
        StartCoroutine(GetLogin(data)); // 그객체를 주면서 대기 함수
    }

    private IEnumerator GetLogin(UserData data) // IEnumerator은 원래 기다리게 하는거 넣을 수 있는 함수에 넣음, 이건 서버 연결될 때까지 대기
    {
        string json = JsonUtility.ToJson(data, true); // json형식으로 변경
        byte[] body = Encoding.UTF8.GetBytes(json); // UTF-8 변환

        UnityWebRequest req = new UnityWebRequest(url, "GET"); // GET을 url 서버에
        req.uploadHandler = new UploadHandlerRaw(body); // 나온 json을 UTF-8로 변환한 것을 업로드 하는 객체
        req.downloadHandler = new DownloadHandlerBuffer(); // 서버에서 반환하는 값 받는 객체

        req.SetRequestHeader("Content-Type", "application/json"); // json현식으로 전달

        yield return req.SendWebRequest(); // 서버 대기

        if(req.result == UnityWebRequest.Result.Success) // 성공
            Debug.Log($"성공 : {req.downloadHandler.text}");
        else // 실패
        {
            warningBoard.SetActive(true); // 경고 창
            warningtext.text = req.error; // 에러 메세지
        }
    }

}