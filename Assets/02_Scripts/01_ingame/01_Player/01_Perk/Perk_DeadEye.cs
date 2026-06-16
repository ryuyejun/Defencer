using UnityEngine;

[CreateAssetMenu(fileName = "Perk_DeadEye", menuName = "SO/Perk/DeadEye")]
public class Perk_DeadEye : PlayerPerkSO
{
    private int comb; // 현재 콤보 수
    public override float OnBulletHit()
    {
        comb += 1;
        if(comb >= 4) // 최대 콤보 수 4로 제한(2배)
            comb = 4;
        return 0.25f * comb; // 1콤보당 25% 데미지 증가
    }

    public override void OnBulletMissHit()
    {
        comb = 0; // 적중 실패 시 초기화
    }
}
