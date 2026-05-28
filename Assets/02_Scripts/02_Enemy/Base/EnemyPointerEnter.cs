using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Renderer renderer; // 렌더러
    [SerializeField] private Material outLine; // 외곽선 머테리얼
    public void OnPointerEnter(PointerEventData data) // 마우스 들어왔을 때
    {
        Material[] currentMat = renderer.sharedMaterials; // 렌더러에서 머테리얼은 배열임
        Material[] newMat = new Material[currentMat.Length + 1]; // 외곽선 머테리얼을 추가할 배열

        for(int i = 0; i < currentMat.Length; i++) // 배열이라 리스트처럼 단순하게 못 함
            newMat[i] = currentMat[i]; // 하나씩 넣기
        newMat[newMat.Length - 1] = outLine; // 배열 마지막에 외곽선 머테리얼 넣기

        renderer.sharedMaterials = newMat; // 렌더러 머테리얼 배열을 새 머테리얼 배열로 바꿈
    }
    public void OnPointerExit(PointerEventData data) // for에 현재 배열 대신 새 배열을 써서 마지막 배열을 복사하지 않는것 말곤 같음
    {
        // 만약 다른 곳에서 머테리얼 마지막에 하나를 추가해버리면 오류가 나긴 하는데 궅이 넣을 것도 없고 넣을 예정도 없어서 유지
        Material[] currentMat = renderer.sharedMaterials;
        Material[] newMat = new Material[currentMat.Length - 1];

        for(int i = 0; i < newMat.Length; i++)
        {
            newMat[i] = currentMat[i];
        }
        renderer.sharedMaterials = newMat;
    }
}
