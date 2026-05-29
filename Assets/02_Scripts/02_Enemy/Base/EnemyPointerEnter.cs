using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material outLine;
    public void OnPointerEnter(PointerEventData data)
    {
        outLine.SetFloat("_Thickness", 0.1f);
    }
    public void OnPointerExit(PointerEventData data)
    {
        outLine.SetFloat("_Thickness", 0f);
    }
}
