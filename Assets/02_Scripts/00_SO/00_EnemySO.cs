using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string enemyName;
    public int damage;
    public int maxhp;
    public GameObject enemyPrefab;
    public int spawndelay;
}
