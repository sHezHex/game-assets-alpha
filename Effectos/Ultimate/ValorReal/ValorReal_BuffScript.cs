using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValorReal_BuffScript : MonoBehaviour
{
    Stats statHandler;
    BuffController buffHandler;

    public int TurnCounter;
    public bool CanBeRemove = true;

    public int AssignedIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //remove Buff Test
        //if(Input.GetKeyDown(KeyCode.L)) //ATAQUE
            //EndTurn();
    }

    public void AssingIndex (int index) //COPIAR SIEMPRE
    {
        AssignedIndex = index;
    }

    //EFECTOS DEL BUFF---------------VVVVVVVVVVVVVVV
    public void InitBuff ()
    {
        TurnCounter = 3;
        buffHandler = gameObject.GetComponentInParent<BuffController>();

        //Buff a caster
        statHandler = gameObject.GetComponentInParent<Stats>();
        GiveBuff();

        //Buff a aliados
    }

    void GiveBuff ()
    {
        statHandler.AddTENBonusFlat(15);
        statHandler.AddFZABonusFlat(20);
        statHandler.AddPODBonusFlat(20);
        statHandler.AddAGLBonusFlat(10);
    }

    public void EndTurn ()
    {
        TurnCounter--;
        if(TurnCounter <= 0)
            TerminateBuff();
    }

    public void TerminateBuff () //Usar esta Funcion para remover BUFFS
    {
        //Debug.Log("Terminando Buff");
        statHandler.AddTENBonusFlat(-15);
        statHandler.AddFZABonusFlat(-20);
        statHandler.AddPODBonusFlat(-20);
        statHandler.AddAGLBonusFlat(-10);
        buffHandler.RemoveBuff(AssignedIndex);
    }

    public void OnAttack (Stats casterStatHandler)
    {
        Stats enemyStatHandler;
        float dmg;
        enemyStatHandler = casterStatHandler.GetComponentInParent<ActionController>().getEnemy().GetComponent<Stats>();

        if(TurnCounter > 0)
        {
            dmg = 20 + (2*casterStatHandler.getATK_total());

            enemyStatHandler.BroadcastMessage("PreMagicDamage",casterStatHandler,SendMessageOptions.DontRequireReceiver);
            enemyStatHandler.DamageMag(dmg);
            enemyStatHandler.BroadcastMessage("OnMagicDamage",casterStatHandler,SendMessageOptions.DontRequireReceiver);
            enemyStatHandler.BroadcastMessage("PostMagicDamage",casterStatHandler,SendMessageOptions.DontRequireReceiver);

        }
    }
}
