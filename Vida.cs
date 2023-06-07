using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float HP_total = 100f; //base + bonus
    public float HP_actual = 0f;

    public BarradeVidaScript BarraHP;

    // Start is called before the first frame update
    void Start()
    {
        HP_actual = HP_total;
        BarraHP.SetMaxHP(HP_total);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

    }

    void TakeDamage (float dmg)
    {
        HP_actual -= dmg;
        BarraHP.SetHP(HP_actual);
    }
}
