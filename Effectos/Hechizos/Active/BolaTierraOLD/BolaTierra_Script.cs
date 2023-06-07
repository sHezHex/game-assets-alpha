using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
idk
30 + 30% FZA
*/

public class BolaTierra_Script : MonoBehaviour
{
    public float dmgOut;

    public Stats statHandler;
    public GameObject ParentPJ;

    //public Stats enemy_stats;
    //public GameObject enemy;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f); //delay en segundos

        statHandler = gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Saludo ()
    {
        Debug.Log("IM ALIVE");
    }


    float DamageOut ()
    {
        dmgOut = 30 + (statHandler.getFZA_total()*0.3f);
        return dmgOut;
    }

    public void Effect (string enemy_tag)
    {
        Stats enemy_stats;
        GameObject enemy;

        enemy = GameObject.FindWithTag("Red");
        enemy_stats = enemy.GetComponent<Stats>();

        //Call for Spell Call Effect
        statHandler.BroadcastMessage("PreSpell",null,SendMessageOptions.DontRequireReceiver);

        //Do Damage
        enemy_stats.TakeHP(DamageOut());
        statHandler.ChargeMNSpell();

        statHandler.BroadcastMessage("PostSpell",null,SendMessageOptions.DontRequireReceiver);
    }
}
