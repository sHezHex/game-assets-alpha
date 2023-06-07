using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValorReal_Script : MonoBehaviour
{

    //Unidad
    Stats statHandler;
    public BuffController buffHandler;

    //Action Controler
    public ActionController acCon;

    public GameObject ValorRealBuff;

    //Aliados
    GameObject ally;
    Stats allyStatHandler;

    //Enemigo
    GameObject enemy;
    Stats enemyStatHandler;

    //Estadisticas
    public int range = 1;
    public float CritChance = 0;
    public float HitChance = 110;

    // Start is called before the first frame update
    void Awake()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
        buffHandler = gameObject.GetComponentInParent<BuffController>();
        acCon = gameObject.GetComponentInParent<ActionController>();
        ValorRealBuff = GameObject.Find("ValorReal_Buff");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Effect()
    {
        //dar buffo a si mismo
        buffHandler.AddBuff(ValorRealBuff);
        //buffHandler.AddDebuff(ValorRealNoMana);

        //dar buffo a aliado
        for(int i=0; i < 6 ; i++)
        {
            ally = acCon.getAlly(acCon.getRange(),i);
            if(ally != null){
                if(ally.name != statHandler.name){
                    if(ally.GetComponent<BuffController>() != null){
                        ally.GetComponent<BuffController>().AddBuff(ValorRealBuff);
                    }
                }
            }
        }
    }
}
