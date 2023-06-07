using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public List<GameObject> Buffs = new List<GameObject>();
    public List<bool> BuffCanBeRemove = new List<bool>();

    public List<GameObject> Debuffs = new List<GameObject>();

    //public List<int> Test = new List<int>();
    //public int i;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddBuff (GameObject buff)
    {
        int index;
        //Debug.Log("Añadiendo Buff " + buff.name);

        buff = Instantiate(buff,gameObject.transform);

        Buffs.Add(buff); //añadir Buff
        index = Buffs.Count - 1; //conseguir index <- usar para borrar (?)
        Buffs[index].BroadcastMessage("AssingIndex",index); //dar index
        Buffs[index].BroadcastMessage("InitBuff",null); //inicializar Buff
    }

    public void RemoveBuff (int index)
    {
        Destroy(Buffs[index]); //Borrar Objeto
        Buffs.RemoveAt(index); //Reajustar Lista
    }

    public void DestoyBuff (int index) //usar ESTA funcion para efectos <<<<<<! !!  !!
    {
        Buffs[index].BroadcastMessage("TerminateBuff",null,SendMessageOptions.DontRequireReceiver);
    }

    public void AddDebuff (GameObject debuff)
    {
        int index;
        //Debug.Log("Añadiendo Buff " + buff.name);

        debuff = Instantiate(debuff,gameObject.transform);

        Debuffs.Add(debuff); //añadir Buff
        index = Debuffs.Count - 1; //conseguir index <- usar para borrar (?)
        Debuffs[index].BroadcastMessage("AssingIndex",index); //dar index
        Debuffs[index].BroadcastMessage("InitDebuff",null); //inicializar Debuff
    }

    public void RemoveDebuff (int index)
    {
        Destroy(Debuffs[index]); //Borrar Objeto
        Debuffs.RemoveAt(index); //Reajustar Lista
    }

    public void DestoyDebuff (int index) //usar ESTA funcion para efectos <<<<<<! !!  !!
    {
        Debuffs[index].BroadcastMessage("TerminateDebuff",null,SendMessageOptions.DontRequireReceiver);
    }
}
