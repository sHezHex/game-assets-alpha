using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTester : MonoBehaviour
{
    public Stats statHandler;

    //ARMA
    public GameObject Weapon;

    //Hechizo 1
    public GameObject Spell1;

    //Hechizo 2
    public GameObject Spell2;

    //GET ENEMY ----------
    public GameObject enemy;
    public Stats enemy_stats;

    int SpellTimer = 100;

    public float dmg;

    /* Start is called before the first frame update
    void Start()
    {

    }
    */

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f); //delay en segundos

        statHandler = gameObject.GetComponent<Stats>();

        enemy = GameObject.FindWithTag("Red");
        enemy_stats = enemy.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        SpellTimer--;

        if (Input.GetKeyDown(KeyCode.Z)) //Bola Tierra
        {
            Spell1.BroadcastMessage("Effect","Red");
        }

        if (Input.GetKeyDown(KeyCode.X)){ //AutoHeal

            Spell2.BroadcastMessage("Effect","Red");
        }
    }
}
