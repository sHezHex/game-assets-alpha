using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHeal_Script : MonoBehaviour
{
    public Stats statHandler;

    public float healValue;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        HealValue();
    }

    public void Effect (string Target)
    {
        statHandler.HealFlat(healValue);
    }

    void HealValue ()
    {
        healValue = 10 + (statHandler.getFZA_total() * 0.1f);
    }
}
