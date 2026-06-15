using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject warningBoard;

    public void OnClickCon() => warningBoard.SetActive(false); // 확인 누르면 비활성화
}
