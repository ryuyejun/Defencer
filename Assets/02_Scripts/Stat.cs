using UnityEngine;

public class Stat : MonoBehaviour
{
    public static Stat instance;
    public int HP;
    public int killedEnemy;
    public int[] enemyPosition = new int[9] {9, 8, 7, 6, 5, 4, 3, 2, 1};
    public ConfigTextUI textUI;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
