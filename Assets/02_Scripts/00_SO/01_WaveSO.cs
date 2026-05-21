using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Wave/Wave")]
public class WaveSO : ScriptableObject
{
    public List<EnemySO> enemyList = new List<EnemySO>();
}
