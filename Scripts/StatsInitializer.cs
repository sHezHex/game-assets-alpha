using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsInitializer : MonoBehaviour
{
    public float HP;
    public float ManaMax;
    public float FZA;
    public float POD;
    public float ARM;
    public float RES;
    public float AGL;
    public float VEL;
    public float TEN;
    public float PRE;
    public float CON;
    public List<int> Traits = new List<int>();

    //public string Tag;

    Stats statHandler;


    // Start is called before the first frame update
    void Awake() //podria ser "ON COMBAT START"
    {
        statHandler = gameObject.GetComponentInParent<Stats>();

        statHandler.setHP_base(HP);
        statHandler.InitHP();
        statHandler.setMN_max(ManaMax);
        statHandler.InitializeMN();
        statHandler.setFZA_base(FZA);
        statHandler.setPOD_base(POD);
        statHandler.setARM_base(ARM);
        statHandler.setRES_base(RES);
        statHandler.setAGL_base(AGL);
        statHandler.setAGL_base(VEL);
        statHandler.setTEN_base(TEN);
        statHandler.setPRE_base(PRE);
        statHandler.setCON_base(CON);
        //gameObject.tag = Tag;

        for(int i=0 ; i<Traits.Count ; i++)
        {
            statHandler.AddTrait(Traits[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
