using UnityEngine;
using TMPro;

public class CoolText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public void UpdateText(int cooltime)
    {
        text.text = $"쿨타임\n{cooltime}";
    }
}
