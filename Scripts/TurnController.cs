using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    BattleFieldController BattleField;
    ActionController acCon;

    string[] characterOrder = new string[200];
    bool BattleInitiated = false;



    // Start is called before the first frame update
    void Start()
    {
        BattleField.GetComponent<BattleFieldController>();
    }

    // Update is called once per frame
    void Update()
    {
        //MANUAL CONTROLL --- BORRAR DESPUES
    }

    void BeginBattle()
    {
        BattleInitiated = true;
    }

    void EndBattle()
    {
        BattleInitiated = false;
    }

    void BeginTurn()
    {
        if(BattleInitiated)
        {
            gameObject.BroadcastMessage("OnTurnBegin",null,SendMessageOptions.DontRequireReceiver);
        }
    }

    void EndTurn()
    {
        gameObject.BroadcastMessage("OnTurnEnd",null,SendMessageOptions.DontRequireReceiver);
    }

    void Turn ()
    {
        //int index = 0;
        BeginTurn();
        for(int i = 0; i < 200 ; i++)
        {
            if(characterOrder[i] != "")
            {
                acCon = GameObject.Find(characterOrder[i]).GetComponent<ActionController>();
                acCon.BroadcastMessage("MakeAction",null);
            }
        }
    }


    //ORDENAMIENTO
    //1 Toma el valor de AGL de cada personaje y coloca el nombre del personaje en el array tal que index = AGL.
    //2 Si en el index ya hay un personaje, le suma +1 o -1 hasta encontrar una posicion disponible.
    //3 Cuando un personaje cambia su AGL Manda un mensaje "SendMessageUpwards(ChangeAGL,Stats pjs)" y se repite el paso 1->2 para ese pj
        //ajustar para que la seleccion de linea "FrontBlue, FrontRed, etc" sea aleatoria
    void ArrangeCharacters ()
    {
        int RandomSelect;
        int index;
        GameObject character;
        for (int i = 0; i < BattleField.n ; i++)
        {
            //FRONT BLUE
            character = BattleField.getCharFront(i,"Blue");
            index = Mathf.RoundToInt(character.GetComponent<Stats>().getAGL_total());
            if(character != null)
            {
                do
                {
                    if(characterOrder[index] == "") //espacio vacio
                    {
                        characterOrder[index] = character.name;
                        break;
                    }
                    RandomSelect = Random.Range(-1,2); //valores entre -1 y 1
                    index += RandomSelect; //re ajustar index

                }while(characterOrder[index] != ""); //Ojala funcione, no quiero poner un while(true)
            }

            //FRONT RED
            character = BattleField.getCharFront(i,"Red");
            index = Mathf.RoundToInt(character.GetComponent<Stats>().getAGL_total());
            if(character != null)
            {
                do
                {
                    if(characterOrder[index] == "") //espacio vacio
                    {
                        characterOrder[index] = character.name;
                        break;
                    }
                    RandomSelect = Random.Range(-1,2); //valores entre -1 y 1
                    index += RandomSelect; //re ajustar index

                }while(characterOrder[index] != ""); //Ojala funcione, no quiero poner un while(true)
            }

            //BACK BLUE
            character = BattleField.getCharBack(i,"Blue");
            index = Mathf.RoundToInt(character.GetComponent<Stats>().getAGL_total());
            if(character != null)
            {
                do
                {
                    if(characterOrder[index] == "") //espacio vacio
                    {
                        characterOrder[index] = character.name;
                        break;
                    }
                    RandomSelect = Random.Range(-1,2); //valores entre -1 y 1
                    index += RandomSelect; //re ajustar index

                }while(characterOrder[index] != ""); //Ojala funcione, no quiero poner un while(true)
            }

            //BACK RED
            character = BattleField.getCharBack(i,"Red");
            index = Mathf.RoundToInt(character.GetComponent<Stats>().getAGL_total());
            if(character != null)
            {
                do
                {
                    if(characterOrder[index] == "") //espacio vacio
                    {
                        characterOrder[index] = character.name;
                        break;
                    }
                    RandomSelect = Random.Range(-1,2); //valores entre -1 y 1
                    index += RandomSelect; //re ajustar index

                }while(characterOrder[index] != ""); //Ojala funcione, no quiero poner un while(true)
            }
        }
    }

    public void AGLChange (Stats statHandler)
    {
        int index, RandomSelect;
        index = SearchChar(statHandler.name);
        characterOrder[index] = "";
        do
        {
            if(characterOrder[index] == "") //espacio vacio
            {
                characterOrder[index] = statHandler.name;
                break;
            }
            RandomSelect = Random.Range(-1,2); //valores entre -1 y 1
            index += RandomSelect; //re ajustar index
        }while(characterOrder[index] != ""); //Ojala funcione, no quiero poner un while(true)
    }

    int SearchChar (string name)
    {
        for (int i=0 ; i < 200 ; i++)
        {
            if(characterOrder[i] == name)
                return i;
        }
        return 0;
    }

}
