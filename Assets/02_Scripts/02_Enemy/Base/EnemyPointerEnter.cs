using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EnemyPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("줌")]
    private Vector3 oldpos;
    private float oldzoom;
    [SerializeField] private float zoomFov = 30f;
    [SerializeField] private float zoomTime = 0.5f;
    [SerializeField] private Vector3 offset = new Vector3(-6, 35, 0);
    public RePosBoard backboard;
    [Header("외곽선")]
    [SerializeField] private Renderer enemyren;
    [SerializeField] private Material outLine;
    [SerializeField] private bool isClicked = false;

    private void Start()
    {
        oldpos = Camera.main.transform.position;
        oldzoom = Camera.main.fieldOfView;
        outLine = enemyren.materials[1];
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
        if(data.button == PointerEventData.InputButton.Left)
        {
            if(!isClicked)
            {
                Camera.main.transform.DOKill();
                Camera.main.transform.DOMove(transform.position + offset, zoomTime);
                Camera.main.DOFieldOfView(zoomFov, zoomTime).SetEase(Ease.OutCubic);
                backboard.gameObject.SetActive(true);
                backboard.callE = this;
                isClicked = true;
            }
        }
    }
    
    public void CameraReset()
    {
        Camera.main.transform.DOKill();
        Camera.main.transform.DOMove(oldpos, zoomTime);
        Camera.main.DOFieldOfView(oldzoom, zoomTime).SetEase(Ease.OutCubic);
        backboard.gameObject.SetActive(false);
        isClicked = false;
        outLine.SetFloat("_Thickness", 0f);
    }
}
