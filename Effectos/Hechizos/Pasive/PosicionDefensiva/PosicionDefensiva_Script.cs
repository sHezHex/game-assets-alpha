using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionDefensiva_Script : MonoBehaviour
{
    Stats statHandler;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GiveBuff (int i)
    {
        statHandler.AddARMBonusFlat(12 * i);
        statHandler.AddRESBonusFlat(12 * i);
    }

    public void PreGetAttack (Stats casterStatHandler)
    {
        GiveBuff(1);
        casterStatHandler.AddManaChargeModifierPenalty(10);
    }

    public void PostGetAttack(Stats casterStatHandler)
    {
        GiveBuff(-1);
        casterStatHandler.RestManaChargeModifierPenalty(10);
    }
}
