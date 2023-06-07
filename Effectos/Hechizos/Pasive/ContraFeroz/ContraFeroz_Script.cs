using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraFeroz_Script : MonoBehaviour
{
    Stats statHandler;

    public float Bonus_FZA = 10;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginBattle ()
    {
        statHandler.BroadcastMessage("EnableCounter",null);
    }

    public void OnGetAttack (Stats enemyStatHandler)
    {
        statHandler.GetComponentInParent<ActionController>().Weapon.BroadcastMessage("CounterAttack",enemyStatHandler,SendMessageOptions.DontRequireReceiver);
    }

    public void PreGetAttack (Stats enemyStatHandler)
    {
        statHandler.AddFZABonusFlat(Bonus_FZA);
    }

    public void PostGetAttack (Stats enemyStatHandler)
    {
        statHandler.AddFZABonusFlat(-Bonus_FZA);
    }
}
