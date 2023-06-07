using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAccionController : MonoBehaviour
{

    public ActionController acCon;
    public string enemyTag;
    public GameObject enemy;
    public int enemyIndex;

    // Start is called before the first frame update
    void Start()
    {
        acCon = gameObject.GetComponent<ActionController>();
        enemyTag = acCon.getEnemyTag();
    }

    // Update is called once per frame
    void Update()
    {
        //ACTION CONTROLER TEST
        if(Input.GetKeyDown(KeyCode.O))
            acCon.getEnemySelecProb();

        if(Input.GetKeyDown(KeyCode.P))
            acCon.selectEnemy();

        //ACCIONES
        if(Input.GetKeyDown(KeyCode.A)) //ATAQUE
            acCon.UseAttack();

        if(Input.GetKeyDown(KeyCode.S)) //ULTIMATE
            acCon.UseUlt();

        if(Input.GetKeyDown(KeyCode.Z)) //H1
            acCon.UseSpell(0);

        if(Input.GetKeyDown(KeyCode.X)) //H2
            acCon.UseSpell(1);


        if(Input.GetKeyDown(KeyCode.C)) //H3
            acCon.UseSpell(2);


        if(Input.GetKeyDown(KeyCode.V)) //H4
            acCon.UseSpell(3);

        //Seleccion de objetivo
        if(Input.GetKeyDown(KeyCode.Q))
            acCon.setTargetIndex(0);

        if(Input.GetKeyDown(KeyCode.W))
            acCon.setTargetIndex(1);

        if(Input.GetKeyDown(KeyCode.E))
            acCon.setTargetIndex(2);

        if(Input.GetKeyDown(KeyCode.R))
            acCon.setTargetIndex(3);

        if(Input.GetKeyDown(KeyCode.T))
            acCon.setTargetIndex(4);

        enemy = acCon.getEnemy();
        enemyIndex = acCon.getTargetIndex();
    }
}
