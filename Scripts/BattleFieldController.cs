using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldController : MonoBehaviour
{

    //HACER ESTO CON ARRAY <<<----
    /*
    2 lineas <- VANGUARDIA (FRONT)
                RETAGUARDIA (BACK)

    10 pjs maximo por linea <- podrian ser menos

    5 pjs por linea (para pruebas)
    */

    public uint n = 6; //espacios maximos
    public Vector3 scale = new Vector3(0.2f,0.2f,0.2f);
    public Vector3 OffSet = new Vector3 (0,0,10);

    public PartyLoader pLoader;

    //Blue team
    GameObject[] FrontBlue = new GameObject[6];
    GameObject[] BackBlue = new GameObject[6];

    //Read Team
    GameObject[] FrontRed = new GameObject[6];
    GameObject[] BackRed = new GameObject[6];

    string CharPos;
    GameObject character;
    string charPath;

    // Start is called before the first frame update
    void Start()
    {
        //test = Resources.Load("Characters/Dummy") as GameObject;
        //Instantiate(test,gameObject.transform);
        pLoader = gameObject.GetComponent<PartyLoader>();
        CharacterInitialization();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void CharacterInitialization()
    {
        //FRONT BLUE
        for (int i=0 ; i < pLoader.FrontBlue.Count ; i++)
        {
            if(pLoader.FrontBlue[i] != null)
            {
                CharPos = "BF" + (i+1);
                charPath = "Characters/"+pLoader.FrontBlue[i];
                character = Resources.Load(charPath) as GameObject;
                if(character != null){
                    FrontBlue[i] = Instantiate(character,gameObject.transform);
                    FrontBlue[i].GetComponentInParent<ActionController>().setTag("Blue");
                    FrontBlue[i].GetComponentInParent<ActionController>().setMyLine(1);
                    FrontBlue[i].name = FrontBlue[i].name + " Blue " + i;
                    FrontBlue[i].transform.localScale = scale;
                    FrontBlue[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
                }
            }
        }

        //BACK BLUE
        for (int i=0 ; i < pLoader.BackBlue.Count ; i++)
        {
            if(pLoader.BackBlue[i] != null)
            {
                CharPos = "BB" + (i+1);
                charPath = "Characters/"+pLoader.BackBlue[i];
                character = Resources.Load(charPath) as GameObject;
                if(character != null){
                    BackBlue[i] = Instantiate(character,gameObject.transform);
                    BackBlue[i].tag = "Blue";
                    BackBlue[i].GetComponentInParent<ActionController>().setMyLine(2);
                    BackBlue[i].name = BackBlue[i].name + " Blue " + i;
                    BackBlue[i].transform.localScale = scale;
                    BackBlue[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
                }
            }
        }

        //FRONT RED
        for (int i=0 ; i < pLoader.FrontRed.Count ; i++)
        {
            if(pLoader.FrontRed[i] != null)
            {
                CharPos = "RF" + (i+1);
                charPath = "Characters/"+pLoader.FrontRed[i];
                character = Resources.Load(charPath) as GameObject;
                if(character != null){
                    FrontRed[i] = Instantiate(character,gameObject.transform);
                    FrontRed[i].tag = "Red";
                    FrontRed[i].GetComponentInParent<ActionController>().setMyLine(1);
                    FrontRed[i].name = FrontRed[i].name + " Red " + i;
                    FrontRed[i].transform.localScale = scale;
                    FrontRed[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
                }
            }
        }

        //BACK RED
        for (int i=0 ; i < pLoader.BackRed.Count ; i++)
        {
            if(pLoader.BackRed[i] != null)
            {
                CharPos = "RB" + (i+1);
                charPath = "Characters/"+pLoader.BackRed[i];
                character = Resources.Load(charPath) as GameObject;
                if(character != null){
                    BackRed[i] = Instantiate(character,gameObject.transform);
                    BackRed[i].GetComponentInParent<ActionController>().setTag("Red");
                    BackRed[i].GetComponentInParent<ActionController>().setMyLine(2);
                    BackRed[i].name = BackRed[i].name + " Red " + i;
                    BackRed[i].transform.localScale = scale;
                    BackRed[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
                }
            }
        }
    }

/*
    void CharacterInitialization()
    {
        //Checkear por empty GameObject
            //iniciar gameobject
            //dar tag
            //asignar linea
            //asignar nombre <- para test
            //escalar personaje (respecto a arena)
            //asignar posicion <- Posicion Inicial + Posicion de Arena + Corrimiento(para ponerlo sobre la arena)

        for(int i=0 ; i < n ; i++)
        {
            //BLUE FRONT----------------------------------------------------------------------
            if (FrontBlue[i] != null)
            {
                CharPos = "BF" + (i+1);
                FrontBlue[i] = Instantiate(FrontBlue[i],gameObject.transform);
                FrontBlue[i].tag = "Blue";
                FrontBlue[i].GetComponentInParent<ActionController>().setMyLine(1);
                FrontBlue[i].name = FrontBlue[i].name + " Blue " + i;
                FrontBlue[i].transform.localScale = scale;
                FrontBlue[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
            }

            //BLUE BACK -------------------------------------------------------------
            if (BackBlue[i] != null)
            {
                CharPos = "BB" + (i+1);
                BackBlue[i] = Instantiate(BackBlue[i],gameObject.transform);
                BackBlue[i].tag = "Blue";
                BackBlue[i].GetComponentInParent<ActionController>().setMyLine(2);
                BackBlue[i].name = BackBlue[i].name + " Blue " + i;
                BackBlue[i].transform.localScale = scale;
                BackBlue[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
            }

            //RED FRONT -----------------------------------------------------------------------
            if (FrontRed[i] != null)
            {
                CharPos = "RF" + (i+1);
                FrontRed[i] = Instantiate(FrontRed[i],gameObject.transform);
                FrontRed[i].tag = "Red";
                FrontRed[i].GetComponentInParent<ActionController>().setMyLine(1);
                FrontRed[i].name = FrontRed[i].name + " Red " + i;
                FrontRed[i].transform.localScale = scale;
                FrontRed[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
            }

            //RED BACK ----------------------------------------------------------------
            if (BackRed[i] != null)
            {
                CharPos = "RB" + (i+1);
                BackRed[i] = Instantiate(BackRed[i],gameObject.transform);
                BackRed[i].tag = "Red";
                BackRed[i].GetComponentInParent<ActionController>().setMyLine(2);
                BackRed[i].name = BackRed[i].name + " Red " + i;
                BackRed[i].transform.localScale = scale;
                BackRed[i].transform.position = GameObject.Find(CharPos).transform.position + GameObject.Find("BattleField").transform.position + (OffSet*(i+1));
            }
        }
    }
*/

    public GameObject getTargetBack (int index, string Tag)
    {
        if(Tag == "Blue")
            return BackBlue[index];

        if(Tag == "Red")
            return BackRed[index];

        return null;
    }

    public GameObject getTargetFront (int index, string Tag)
    {
        if(Tag == "Blue")
            return FrontBlue[index];

        if(Tag == "Red")
            return FrontRed[index];

        return null;
    }

    public GameObject getTargetInLane (int lane, int index, string Tag)
    {
        if (lane == 1)
            return getTargetFront(index,Tag);

        if (lane == 2)
            return getTargetBack(index,Tag);

        return null;
    }

    public int CountInFront (string Tag)
    {
        int cant = 0;
        if(Tag == "Blue"){
            for(int i=0; i < FrontBlue.Length ; i++)
            {
                if(FrontBlue[i] != null)
                    cant++;
            }
        }else if(Tag == "Red"){
            for(int i=0; i < FrontRed.Length ; i++)
            {
                if(FrontRed[i] != null)
                    cant++;
            }
        }
        return cant;
    }

    public int CountInBack (string Tag)
    {
        int cant = 0;
        if(Tag == "Blue"){
            for(int i=0; i < BackBlue.Length ; i++)
            {
                if(BackBlue[i] != null)
                    cant++;
            }
        }else if(Tag == "Red"){
            for(int i=0; i < BackRed.Length ; i++)
            {
                if(BackRed[i] != null)
                    cant++;
            }
        }
        return cant;
    }

    public int CountInLine (int line, string Tag)
    {
        if(line == 1)
            return CountInFront(Tag);

        if(line == 2)
            return CountInBack(Tag);

        return -1; //Error
    }

    public GameObject getCharFront(int index, string Color)
    {
        if(Color == "Blue")
            return FrontBlue[index];

        if(Color == "Red")
            return FrontRed[index];

        return null;
    }

    public GameObject getCharBack(int index, string Color)
    {
        if(Color == "Blue")
            return BackBlue[index];

        if(Color == "Red")
            return BackRed[index];

        return null;
    }
}
