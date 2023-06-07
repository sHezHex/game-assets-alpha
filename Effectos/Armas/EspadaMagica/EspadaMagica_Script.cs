using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaMagica_Script : MonoBehaviour
{

    Stats statHandler;

    public float base_ATK = 12;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
        statHandler.setATK_base(base_ATK);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreSpell ()
    {
        statHandler.AddFZABonusFlat(10);
        Debug.Log("+10 FZA");
    }

    public void PostSpell ()
    {
        statHandler.AddFZABonusFlat(-10);
        Debug.Log("Bonus Quitado");
    }
}
