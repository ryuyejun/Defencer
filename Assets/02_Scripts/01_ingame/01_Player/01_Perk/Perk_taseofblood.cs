using UnityEngine;

[CreateAssetMenu(fileName = "Perk_TastofBlood", menuName = "SO/Perk/TaseofBlood")]
public class Perk_TastofBlood : PlayerPerkSO
{
    public override void EnemyDie()
    {
        player.Stat.HP += 1;
        player.Stat.textUI.SetAllyHPText();
    }
}
