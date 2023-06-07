using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTester : MonoBehaviour
{

    public Stats statHandler;

    public float Health = 0f;
    public float Max_Mana = 0f;
    public float ATK;
    public float Act_Mana = 0f;
    public float Fuerza = 0f;
    public float Armadura = 0f;

    public string myTag = "";

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        gameObject.tag = myTag;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();

        //botones para probar cosas
        if (Input.GetKeyDown(KeyCode.A)){ //Daño Directo
            statHandler.TakeHP(10);
            Debug.Log("A - Daño Directo (10)");
        }

        if (Input.GetKeyDown(KeyCode.S)){ //Daño Fisico
            statHandler.DamageFis(30);
            Debug.Log("S - Daño Fisico (30)");
        }

        if (Input.GetKeyDown(KeyCode.D)){ //Heal
            statHandler.HealFlat(20);
            Debug.Log("D - Heal (20)");
        }

        if (Input.GetKeyDown(KeyCode.F)){ //Heal
            statHandler.AddFZABonusFlat(20);
            Debug.Log("F - +10 FZA");
        }

        if (Input.GetKeyDown(KeyCode.G)){ //Heal
            statHandler.AddARMBonusPercBase(10);
            Debug.Log("G - +10% ARM Base");
        }

        if (Input.GetKeyDown(KeyCode.H)){ //Heal
            statHandler.AddFZABonusPercActual(20);
            Debug.Log("H - +20% FZA Tot");
        }

    }

    void SetStats ()
    {
        statHandler.setHP_base(Health);
        statHandler.InitHP();
        statHandler.setMN_max(Max_Mana);
        Act_Mana = 0f;
        statHandler.setFZA_base(Fuerza);
        statHandler.setARM_base(Armadura);
    }

    void CopyStats ()
    {
        Health = statHandler.getHP_max();
        Max_Mana = statHandler.getMN_max();
        Fuerza = statHandler.getFZA_total();
        Armadura = statHandler.getARM_total();
    }

    void UpdateStats()
    {
        ATK = statHandler.getATK_total();
        Health = statHandler.getHP_actual();
        Act_Mana = statHandler.getMN_actual();
        Fuerza = statHandler.getFZA_total();
        Armadura = statHandler.getARM_total();
    }
}
