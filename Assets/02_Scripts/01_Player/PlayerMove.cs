using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private int playerx;
    [SerializeField] private Transform player;

    void Start()
    {
        playerx = 0;
        SetPlayerPos(0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(playerx != 2)
            {
                playerx += 1;
                SetPlayerPos(playerx);
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(playerx != -2)
            {
                playerx -= 1;
                SetPlayerPos(playerx);
            }

        }
    }

    void SetPlayerPos(int x)
    {
        player.position = new Vector3(player.position.x, player.position.y, x * 6);
    }
}
