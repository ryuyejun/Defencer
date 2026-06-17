using UnityEngine;

[CreateAssetMenu(fileName = "Perk_ChargeProtocol", menuName = "SO/Perk/ChargeProtocol")]
public class Perk_ChargeProtocol : PlayerPerkSO
{
    public override float OnTrapHit(int passTurn)
    {
        if(passTurn < 4)
            return -1f;
        else
            return 0.75f;
    }
}
