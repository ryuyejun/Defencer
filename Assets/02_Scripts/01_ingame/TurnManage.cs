using UnityEngine;
using System;

public class TurnManage : MonoBehaviour
{
    public static TurnManage instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public Action Turn;
    public void TurnStart()
    {
        Turn?.Invoke();
    }
}