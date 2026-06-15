using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StateController : MonoBehaviour
{
    public int HP;
    public int killedEnemy;
    public ConfigTextUI textUI;

    public int mapX;
    public int mapY;
    public bool tileSelecting;
    private EnemyMove[,] enemygrid;
    private AllyMove[,] allygrid;
    private AllyMove[,] trapgrid;
    [SerializeField] private GameObject buttonTileGroup;
    [SerializeField] private GameObject turnButton;
    private Vector3 tileGrid;
    private Action<Vector2> tileClick;

    private void Awake()
    {
        mapX += 1;
        enemygrid = new EnemyMove[mapX, mapY];
        allygrid = new AllyMove[mapX, mapY];
        trapgrid = new AllyMove[mapX, mapY];
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

        if(enemygrid[newx, newy] != null)
        {
            enemygrid[newx, newy].GetHit(ally.OnHit());
            ally.FinishRun(true);
        }

        if(allygrid[newx, newy] == null)
        {
            allygrid[newx, newy] = ally;

            return true;
        }
        return false;
    }

    public bool InitTrap(AllyMove ally, int newx, int newy)
    {
        if(!CanSetCoord(newx, newy)) return false;

        if(enemygrid[newx, newy] != null)
        {
            Debug.Log("설치 hit");
            enemygrid[newx, newy].GetHit(ally.OnHit());
            ally.FinishRun(true);

            return true;
        }

        if(trapgrid[newx, newy] == null)
        {
            trapgrid[newx, newy] = ally;

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
                if(trapgrid[newx, newy] != null)
                    TrapHit(trapgrid[newx, newy], oldx, oldy, newx, newy);
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
            if(newx < 1)
            {
                ally.FinishRun(false);
                return true;
            }
            return false;
        }

        if((enemygrid[newx, newy] != null || enemygrid[oldx, oldy] != null) && allygrid[oldx, oldy] == ally)
        {
            if(ally != null)
            {

                ally.transform.DOKill();

                EnemyMove targetEnemy = enemygrid[newx, newy] != null ? enemygrid[newx, newy] : enemygrid[oldx, oldy];

                targetEnemy.GetHit(ally.OnHit());
                allygrid[oldx, oldy] = null;
                ally.transform.DOMove(new Vector3(35f - newx * 6f, 5f, 12 - newy * 6), 0.5f);
                ally.FinishRun(true);

                return true;
            }
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

    private void TrapHit(AllyMove ally, int Ex, int Ey, int Tx, int Ty)
    {
        if(ally != null)
        {
            EnemyMove target = enemygrid[Ex, Ey];
            target.GetHit(ally.OnHit());
            trapgrid[Tx, Ty] = null;
            ally.FinishRun(true);
        }
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
        else if(trapgrid[x, y] == ally)
            trapgrid[x, y] = null;
    }

    public void SelectTile(Action<Vector2> onSelected)
    {
        buttonTileGroup.SetActive(true);
        turnButton.SetActive(false);
        tileSelecting = true;

        tileClick = onSelected;
    }

    public void OnTileClick(TileButtonGrid clickedButton)
    {
        tileGrid = new Vector2(clickedButton.gridx, clickedButton.gridy);
        buttonTileGroup.SetActive(false);
        turnButton.SetActive(true);
        tileSelecting = false;

        tileClick?.Invoke(tileGrid);
    }
}
