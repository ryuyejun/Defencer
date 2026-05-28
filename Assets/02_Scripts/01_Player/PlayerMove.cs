using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private int playerx;
    [SerializeField] private Transform player;

    private void Start()
    {
        playerx = 0;
        SetPlayerPos(0);
    }

    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        if(dir.y == -1)
        {
            if(playerx != -2)
            {
                playerx -= 1;
                SetPlayerPos(playerx);
            }
        }
        if(dir.y == 1)
        {
            if(playerx != 2)
            {   
                playerx += 1;
                SetPlayerPos(playerx);
            }
        }
    }

    private void SetPlayerPos(int x)
    {
        player.DOMove(new Vector3(player.position.x, player.position.y, x * 6), 0.3f);
    }
}
