using UnityEngine;

[CreateAssetMenu(fileName = "Perk_DeadEye", menuName = "SO/Perk/DeadEye")]
public class Perk_DeadEye : PlayerPerkSO
{

    public override float OnBulletHit()
    {
        player.deadeyecomb += 1;
        if(player.deadeyecomb >= 4) // 최대 콤보 수 4로 제한(2배)
            player.deadeyecomb = 4;
        return 0.25f * player.deadeyecomb; // 1콤보당 25% 데미지 증가
    }

    public override void OnBulletMissHit()
    {
        player.deadeyecomb = 0; // 적중 실패 시 초기화
    }
}
