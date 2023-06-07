using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HachaDeBan_Script : MonoBehaviour
{

    public float ATK = 16;
    public int range = 1;
    public string DamageTipe = "Fisico";

    //unit Stats
    Stats statHandler;
    float DamageOut; //Daño de Ataque

    public GameObject enemy;
    Stats enemyStatHandler;


    // Start is called before the first frame update
    void Start()
    {
        //Reconocer portador y dar ATK
        statHandler = gameObject.GetComponentInParent<Stats>();
        statHandler.setATK_base(ATK);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack (string Target)
    {
        DamageOut = statHandler.getATK_total() + statHandler.getFZA_total();

        enemy = GameObject.FindWithTag(Target);
        enemyStatHandler = enemy.GetComponent<Stats>();

        //Hacer Daño
        enemyStatHandler.DamageFis(DamageOut);
    }

    public void PostUltimate (string Target)
    {
        statHandler.HealFlat(statHandler.getHP_max()*0.18f);
    }
}
