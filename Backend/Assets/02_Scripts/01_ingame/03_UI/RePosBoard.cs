using UnityEngine;
using UnityEngine.EventSystems;

public class RePosBoard : MonoBehaviour, IPointerClickHandler
{
    public EnemyPointerEnter callE;

    public void OnPointerClick(PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
            callE.CameraReset();
    }
}
