using UnityEngine;
using DG.Tweening;

public class StateController : MonoBehaviour
{
    public int HP;
    public int killedEnemy;
    public ConfigTextUI textUI;

    public int mapX;
    public int mapY;
    private EnemyMove[,] grid;

    private void Awake()
    {
        mapX += 1;
        grid = new EnemyMove[mapX, mapY];
    }
    private bool CanSetCoord(int x, int y)
    {
        return x >= 0 && x < mapX && y >= 0 && y < mapY;
    }

    public bool InitEnemy(EnemyMove enemy, int newx, int newy)
    {
        if (!CanSetCoord(newx, newy)) return false;

        if(grid[newx, newy] == null)
        {
            grid[newx, newy] = enemy;
            
            return true;
        }
        return false;
    }

    public bool UpdatePosition(EnemyMove enemy, int oldx, int oldy, int newx, int newy)
    {
        if (!CanSetCoord(oldx, oldy) || !CanSetCoord(newx, newy))
        {
            if(newx >= mapX)
            {
                enemy.FinishRun();
                return true;
            }
            return false;
        }

        if(grid[newx, newy] == null && grid[oldx, oldy] == enemy)
        {
            enemy.transform.DOKill();
            grid[oldx, oldy] = null;
            grid[newx, newy] = enemy;
            enemy.transform.DOMove(new Vector3(35f - newx * 6f, 5f, 12 - newy * 6f), 0.5f);
            
            return true;
        }
        Debug.Log("위치메 이미 있음");
        return false;
    }

    public void ClearPosition(EnemyMove enemy, int x, int y)
    {
        if(grid[x, y] == enemy)
            grid[x, y] = null;
    }
}
