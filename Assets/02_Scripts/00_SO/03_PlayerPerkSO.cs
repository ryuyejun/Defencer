using UnityEngine;

// 퍽들의 부모 SO기 때문에 CreateAssetMenu를 쓰지 않음
public class PlayerPerkSO : ScriptableObject
{
    public string perkname; // 이름

    public virtual float OnBulletHit() // 탄 적중
    {
        return 0; // 기본으론 0 반환
    }

    public virtual void OnMissHit() // 탄 적중 실패
    {
        //기본 없음
    }
}
