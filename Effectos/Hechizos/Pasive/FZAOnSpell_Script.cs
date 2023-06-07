using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZAOnSpell_Script : MonoBehaviour
{

    public Stats statHandler;
    public GameObject ParentPJ;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreSpell ()
    {
        //Dar Fuerza
        Debug.Log("Dando +10 FZA");
        statHandler.AddFZABonusFlat(10);

    }

    public void PostSpell ()
    {
        //Quitar Fuerza
        Debug.Log("Quitando -10 FZA");
        statHandler.AddFZABonusFlat(-10);

    }
}
