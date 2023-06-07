using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excalibur_Script : MonoBehaviour
{
    //cosas en public para test

//COPIAR SIEMPRE --- VVVV
    //Estadisticas
    float ATK = 16;
    int range = 1;
    string DamageTipe = "Fisico";
    float CritChance = 10;
    float HitChance = 100;
    float TotalHitChance;
    bool CanCounter = false;
    int attack_count;
    float CritMultiplier = 2f;

    //unit Stats
    Stats statHandler;
    float DamageOut; //Daño de Ataque

    int enemyIndex;
    string enemyTag;
    public GameObject enemy;
    Stats enemyStatHandler;


    // Start is called before the first frame update
    void Awake() //COPIAR SIEMPRE
    {
        //Reconocer portador y dar ATK
        statHandler = gameObject.GetComponentInParent<Stats>();
        statHandler.setATK_base(ATK);
        statHandler.GetComponentInParent<ActionController>().setRange(range);
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE ENEMY
        //enemy = statHandler.GetComponentInParent<ActionController>().getEnemy();
        //enemyStatHandler = enemy.GetComponent<Stats>();

        //UPDATE RANGE
        statHandler.GetComponentInParent<ActionController>().setRange(range);
    }

    float CalculateDamageOut () //COPIAR SIEMPRE
    {
        if(DamageTipe == "Fisico")
            return statHandler.getATK_total() + statHandler.getFZA_total();

        if(DamageTipe == "Magico")
            return statHandler.getATK_total() + statHandler.getPOD_total();

        return statHandler.getATK_total();

    }

    int CalculateAttackCount () //COPIAR SIEMPRE
    {
        attack_count = statHandler.getHitCant() - enemyStatHandler.getHitCantReduction();

        if(attack_count <= 1)
            attack_count = 1;

        if(attack_count > 5)
            attack_count = 5;

        return attack_count;
    }

    public bool IsHit ()
    {
        float Rand;
        float Hit;

        Rand = Random.Range(0f,100f);

        Hit = (statHandler.getHITChance() + HitChance) - enemyStatHandler.getAVOChance();

        if(Hit >= Rand)
            return true;

        Debug.Log("MISS");
        return false;
    }

    public void Attack () //COPIAR
    {
        //enemy = statHandler.GetComponent<ActionController>().getEnemy();
        //enemyStatHandler = enemy.GetComponent<Stats>();

        //DAÑO BASE DE ATAQUE
        statHandler.BroadcastMessage("PreAttackSequence",statHandler,SendMessageOptions.DontRequireReceiver);
        enemyStatHandler.BroadcastMessage("PreGetAttackSequence",statHandler,SendMessageOptions.DontRequireReceiver);

        //Calcular cantidad de Ataques
        attack_count = CalculateAttackCount();
        for (int i=0; i < attack_count ; i++)
        {
            BasicAttack(i);
        }

        statHandler.BroadcastMessage("PostAttackSequence",statHandler,SendMessageOptions.DontRequireReceiver);
        enemyStatHandler.BroadcastMessage("PostGetAttackSequence",statHandler,SendMessageOptions.DontRequireReceiver);

        //Debug.Log("Ataque Realizado. DF = " + DamageOut + ". DV = " + extraDamage);
    }

    public void BasicAttack (int HitNumber)
    {
        DamageOut = CalculateDamageOut();

        //Probabilidad de golpear
        TotalHitChance = HitChance + statHandler.getHITChance() + (-10*(HitNumber-1));

        if(IsHit()){
            //Crit ?
            if(IsCrit()){
                statHandler.BroadcastMessage("OnCrit",statHandler,SendMessageOptions.DontRequireReceiver);
                enemyStatHandler.BroadcastMessage("OnGetCrit",statHandler,SendMessageOptions.DontRequireReceiver);
                DamageOut = CritMultiplier * DamageOut;
            }

            enemyStatHandler.DamageFis(DamageOut);

            statHandler.BroadcastMessage("OnAttack",statHandler,SendMessageOptions.DontRequireReceiver);
            enemyStatHandler.BroadcastMessage("OnGetAttack",statHandler,SendMessageOptions.DontRequireReceiver);
        }
    }

    public void BasicAttack ()
    {
        DamageOut = CalculateDamageOut();

        //Probabilidad de golpear
        TotalHitChance = HitChance + statHandler.getHITChance();

        if(IsHit()){
            //Crit ?
            if(IsCrit()){
                statHandler.BroadcastMessage("OnCrit",statHandler,SendMessageOptions.DontRequireReceiver);
                enemyStatHandler.BroadcastMessage("OnGetCrit",statHandler,SendMessageOptions.DontRequireReceiver);
                DamageOut = CritMultiplier * DamageOut;
            }

            enemyStatHandler.DamageFis(DamageOut);

            statHandler.BroadcastMessage("OnAttack",statHandler,SendMessageOptions.DontRequireReceiver);
            enemyStatHandler.BroadcastMessage("OnGetAttack",statHandler,SendMessageOptions.DontRequireReceiver);
        }
    }

    public void CounterAttack (Stats enemyStatHandler)
    {
        if(CanCounter)
        {
            statHandler.BroadcastMessage("PreCounter",statHandler,SendMessageOptions.DontRequireReceiver);
            DamageOut = CalculateDamageOut();
            enemyStatHandler.DamageFis(DamageOut);
            statHandler.BroadcastMessage("OnAttack",statHandler,SendMessageOptions.DontRequireReceiver);
            statHandler.BroadcastMessage("PostCounter",statHandler,SendMessageOptions.DontRequireReceiver);
        }
    }

    public void EnableCounter (){CanCounter = true;}
    public void DisableCounter (){CanCounter = false;}

    public bool IsCrit ()
    {
        float Rand;
        float Crit;
        Rand = Random.Range(0f,100f);

        Crit = statHandler.getCRITChance() + CritChance;

        if(Crit >= Rand)
            return true;

        return false;
    }

    public void ChangeCritMultiplier (float c){CritMultiplier = c;}
    public void ResetCritMultiplier (){CritMultiplier = 2f;}
    public void AddCritMultiplier (float c){CritMultiplier += c;}
    public void RestCritMultiplier (float c){CritMultiplier -= c;}

    //Efectos unicos del Arma ------------- vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

    public void PreAttack (Stats casterStatHandler)
    {
        casterStatHandler.AddHitCant(1);
    }

    public void OnAttack (Stats casterStatHandler) //aca se aprovecha q el script ya "sabe" cual es el enemigo y el caster
    {
        float extraDamage; //Daño Verdadero Extra -> 5+(6% FZA)

        //Comprobar y calcular daño extra
        if(statHandler.getFZA_total() > enemyStatHandler.getARM_total())
        {
            extraDamage = 5 + (0.06f * statHandler.getFZA_total());
        }else{
            extraDamage = 0;
        }

        enemyStatHandler.DamageTrue(extraDamage);

    }

    public void PostAttack (Stats casterStatHandler)
    {
        casterStatHandler.HealFlat(15);
        Debug.Log("Curacion Post Ataque");
    }



}
