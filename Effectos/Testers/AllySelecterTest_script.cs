using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVUELVE EL NOMBRE DE TODOS LOS ALIADOS EN LA MISMA LINEA

public class AllySelecterTest_script : MonoBehaviour
{
    int Range;

    BattleFieldController BattleField;
    ActionController AcCon;

    string myTag;

    // Start is called before the first frame update
    void Start()
    {
        BattleField = gameObject.GetComponentInParent<BattleFieldController>();
        AcCon = gameObject.GetComponentInParent<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Effect ()
    {
        myTag = AcCon.getmyTag();
        Range = AcCon.getRange();
        for(int i=0; i < 5 ; i++)
        {
            Debug.Log(BattleField.getTargetInLane(Range,i,myTag).name);
        }
    }
}
