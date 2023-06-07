using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    Stats statHandler;
    BattleFieldController BattleField;

    //Probabilidad de Seleccion
    float SelecProb = 100f;
    List<float> enemySelecProb = new List<float>();
    List<string> enemyNames = new List<string>();


    //ARMA
    public GameObject Weapon;
    string WeaponName; //fx setBasicInfo
    int Range;
    //Rango para selec habilidades
    //el rango es determinado por el arma (fx setBasicInfo)

    //Ultimate
    public GameObject Ultimate;
    string UltimateName;

    //Hechizos
    public GameObject[] Spells = new GameObject[4];
    string [] SpellName = new string[4]; //determinado por el hechizo fx SetBasicInfo
    bool [] SpellIsActive = new bool[4];

    //ENEMY ----------
    GameObject enemy;
    Stats enemyStatHandler;
    public string enemyTag;

    //ALLY -------------
    //GameObject ally;
    //Stats allyStatHandler;
    string allyTag;

    //SELF
    string myTag;
    int myLine;

    int TargetIndex = 0; //index para target, en caso de ser necesario

    void Start()
    {
        //yield return new WaitForSeconds(0.3f); //delay en segundos

        statHandler = gameObject.GetComponent<Stats>();
        BattleField = gameObject.GetComponentInParent<BattleFieldController>();

        //Get Tags
        myTag = gameObject.tag;
        allyTag = myTag;
        enemyTag = setEnemyTag(myTag);

        //enemy = GameObject.FindWithTag(enemyTag);
        //enemyStatHandler = enemy.GetComponent<Stats>();

        //Crear objetos en el personaje
        if(Weapon != null)
            Weapon = Instantiate(Weapon,gameObject.transform);

        if(Ultimate != null)
            Ultimate = Instantiate(Ultimate,gameObject.transform);

        for(int i=0 ; i < 4 ; i++)
        {
            if(Spells[i] != null)
                Spells[i] = Instantiate(Spells[i],gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //myTag = gameObject.tag;
        //enemyTag = setEnemyTag(myTag);
        UpdateEnemy();
    }

    public void MakeAction ()
    {
        Debug.Log("Comienza el turno de: " + gameObject.name);
    }

    /*
    Pre[Acc] (Stats lanzador) <- Antes de una accion
    Post[Acc] (Stats lanzador) <- Despues de una accion
                        Se pone como parametro quien este realizando la accion para que el script que reciba la orden pueda saber a quien darle bonus
                        No es necesario que se use, pero ahi ta

    On[Acc] (Stats lanzador) <- durante la accion. Se requiere el objetivo.
    */

//RANDOM SELECTERS ------
    public void selectEnemy ()
    {
        //get random number
        //seleccionar

        float RandomSelector;
        float Acumulate = 0f;

        RandomSelector = Random.Range(0f,100f);

        //Debug.Log("Prob Enemigo = " + RandomSelector);

        for(int i = 0; i < enemySelecProb.Count ; i++)
        {
            if(RandomSelector <= enemySelecProb[i]+Acumulate)
            {
                //Debug.Log("Enemigo encontrado");
                enemy = GameObject.Find(enemyNames[i]);
                break;
            }else{
                Acumulate += enemySelecProb[i];
                //Debug.Log("ENF...A = " + Acumulate);
            }
        }
    }

    //Las DISMINUCIONES de probabilidad se aplican ANTES del calculo.
    //Los AUMENTOS de probabilidad se aplican DESPUES del calculo
    public void getEnemySelecProb ()
    {
        GameObject e;
        float e_prob;
        //get Front Line enemies
        for (int i=0; i < 6 ; i++)
        {
            e = BattleField.getTargetFront(i,enemyTag);
            if(e != null)
            {
                e_prob = e.GetComponent<ActionController>().getSelecProb() / (Range*1f);
                enemySelecProb.Add(e_prob);
                enemyNames.Add(e.name);
            }
        }

        //get Back Line enemies
        if(Range >= 2)
        {
            for (int i=0; i < 6 ; i++)
            {
                e = BattleField.getTargetBack(i,enemyTag);
                if(e != null)
                {
                    e_prob = e.GetComponent<ActionController>().getSelecProb() / ((Range*1f)+1);
                    enemySelecProb.Add(e_prob);
                    enemyNames.Add(e.name);
                }
            }
        }

        CalculateEnemySelecProb();
    }

    public void CalculateEnemySelecProb ()
    {
        float total_prob = 0;

        //contar acumulado
        for(int i = 0 ; i < enemySelecProb.Count ; i++)
        {
            total_prob += enemySelecProb[i];
        }

        //recalcular
        for(int i = 0 ; i < enemySelecProb.Count ; i++)
        {
            enemySelecProb[i] = (enemySelecProb[i] / total_prob) * 100f;
        }
    }


//BASIC USE FX ----------

    public void UseAttack ()
    {
        //comunicar a BattleField que Se esta realizando el ataque
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("BeforeAttack",statHandler,SendMessageOptions.DontRequireReceiver);
        Weapon.BroadcastMessage("Attack",null); //Attack Sequence
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("AfterAttack",statHandler,SendMessageOptions.DontRequireReceiver);
    }

    public void UseSpell (int s)
    {
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("BeforeSpell",statHandler,SendMessageOptions.DontRequireReceiver);
        Spells[s].BroadcastMessage("Effect",null);
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("AfterSpell",statHandler,SendMessageOptions.DontRequireReceiver);
    }

    public void UseUlt ()
    {
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("BeforeUltimate",statHandler,SendMessageOptions.DontRequireReceiver);
        Ultimate.BroadcastMessage("Effect",null);
        gameObject.GetComponentInParent<BattleFieldController>().BroadcastMessage("AfterUltimate",statHandler,SendMessageOptions.DontRequireReceiver);
    }


//GET SET ---------------

    public string setEnemyTag (string myTag)
    {
        if(myTag == "Blue")
            return "Red";

        if(myTag == "Red")
            return "Blue";

        return "Green"; //No team tag
    }


//RANGE
    public int getRange (){return Range;}
    public void setRange (int r){Range = r;}

//TAG
    public void setTag (string t){gameObject.tag = t;}
    public string getmyTag (){return myTag;}

//LINEA
    public void setMyLine (int l){myLine = l;}

//ENEMY
    public int getTargetIndex (){return TargetIndex;}
    public void setTargetIndex(int t){TargetIndex = t;UpdateEnemy();}
    public GameObject getEnemy (){return enemy;}
    public string getEnemyTag (){return enemyTag;}
    public string getEnemyName () {return enemy.name;}

    public void UpdateEnemy ()
    {
        enemy = BattleField.getTargetInLane(Range,TargetIndex,enemyTag);
        if(enemy == null)
            enemy = BattleField.getTargetInLane(Range,0,enemyTag);
            //SIEMPRE DEBE HABER AL MENOS 1 ENEMIGO !!!!!!!!!!!!!!!!!!!!!!!!!!!! <<<<<<<<<<<<<<<<<<<<<<
    }

//ALLY
    public GameObject getAlly (int lane, int index)
    {
        return BattleField.getTargetInLane(lane,index,allyTag);
    }

//SELECT PROB
    public void setSelecProb (float s){SelecProb = s;}
    public float getSelecProb (){return SelecProb;}
    public void AddSelecProb (float s){SelecProb += s;}
    public void RestSelecProb (float s){SelecProb -= s;}
    public void ResetSelecProb (){SelecProb = 100f;}

}
