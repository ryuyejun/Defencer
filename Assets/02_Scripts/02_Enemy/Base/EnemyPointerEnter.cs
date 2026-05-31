using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EnemyPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("줌")]
    [SerializeField] private float zoomFov = 30f;
    [SerializeField] private float zoomTime = 0.5f;
    [Header("외곽선")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material outLine;
    [SerializeField] private bool isClicked = false;

    private void Start()
    {
        outLine = renderer.materials[1];
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(!isClicked)
        {
            outLine.SetFloat("_Thickness", 0.1f);
        }
    }
    public void OnPointerExit(PointerEventData data)
    {
        if(!isClicked)
        {
            outLine.SetFloat("_Thickness", 0f);
        }
    }
    public void OnPointerClick(PointerEventData data)
    {
        // if(data.button == PointerEventData.InputButton.Left)
        // {
        //     Camera.main.DOFieldOfView(zoomFov, zoomTime).SetEase(Ease.OutCubic);
        //     isClicked = true;
        // }
        Debug.Log("클릭함");
    }
}
