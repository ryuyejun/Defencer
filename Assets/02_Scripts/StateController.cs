using UnityEngine;
using DG.Tweening;

public class StateController : MonoBehaviour
{
    public int HP;
    public int killedEnemy;
    public ConfigTextUI textUI;

    public int mapX;
    public int mapY;
    private EnemyMove[,] enemygrid;
    private AllyMove[,] allygrid;

    private void Awake()
    {
        mapX += 1;
        enemygrid = new EnemyMove[mapX, mapY];
        allygrid = new AllyMove[mapX, mapY];
    }
    private bool CanSetCoord(int x, int y)
    {
        return x >= 0 && x < mapX && y >= 0 && y < mapY;
    }

    public bool InitEnemy(EnemyMove enemy, int newx, int newy)
    {
        if (!CanSetCoord(newx, newy)) return false;

        if(enemygrid[newx, newy] == null)
        {
            enemygrid[newx, newy] = enemy;
            
            return true;
        }
        return false;
    }

    public bool InitAlly(AllyMove ally, int newx, int newy)
    {
        if(!CanSetCoord(newx, newy)) return false;

        if(allygrid[newx, newy] == null)
        {
            allygrid[newx, newy] = ally;

            return true;
        }
        return false;
    }

    public bool EnemyUpdatePosition(EnemyMove enemy, int oldx, int oldy, int newx, int newy)
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

        if(enemygrid[newx, newy] == null && enemygrid[oldx, oldy] == enemy)
        {
            if(enemy != null)
            {
                enemy.transform.DOKill();
                enemygrid[oldx, oldy] = null;
                enemygrid[newx, newy] = enemy;
                enemy.transform.DOMove(new Vector3(35f - newx * 6f, 5f, 12 - newy * 6f), 0.5f);
                
                return true;
            }
        }
        return false;
    }

    public bool AllyUpdatePosition(AllyMove ally, int oldx, int oldy, int newx, int newy)
    {
        if (!CanSetCoord(oldx, oldy) || !CanSetCoord(newx, newy))
        {
            if(newx <= mapX)
            {
                ally.FinishRun();
                return true;
            }
            return false;
        }

        if(allygrid[newx, newy] == null && allygrid[oldx, oldy] == ally)
        {
            if(ally != null)
            {
                ally.transform.DOKill();
                allygrid[oldx, oldy] = null;
                allygrid[newx, newy] = ally;
                ally.transform.DOMove(new Vector3(35f - newx * 6f, 5f, 12 - newy * 6), 0.5f);

                return true;
            }
        }
        return false;
    }

    public void EnemyClearPosition(EnemyMove enemy, int x, int y)
    {
        if(enemygrid[x, y] == enemy)
            enemygrid[x, y] = null;
    }

    public void AllyClearPosition(AllyMove ally, int x, int y)
    {
        if(allygrid[x, y] == ally)
            allygrid[x, y] = null;
    }
}
