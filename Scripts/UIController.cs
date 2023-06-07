using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Stats statHandler;

    //Sliders
    public Slider HPSlider;
    public Slider MNSlider;

    //Text
    public int text_timer; //timer para que desaparezca el texto
    public Text FisDmgText;
    public Text MagDmgText;
    public Text TrueDmgText;
    public Text HPText;
    public Text MNText;

    // Start is called before the first frame update
    void Start()
    {
        statHandler = gameObject.GetComponentInParent<Stats>();

        HPSlider.minValue = 0;
        MNSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizar valores maximos
        HPSlider.maxValue = statHandler.getHP_max();
        MNSlider.maxValue = statHandler.getMN_max();

        //Actualizar Valores actuales
        HPSlider.value = statHandler.getHP_actual();
        MNSlider.value = statHandler.getMN_actual();

        //Valores texto HP y Mana
        HPText.text = statHandler.getHP_actual().ToString("#") + " / " + statHandler.getHP_max().ToString("#");
        MNText.text = statHandler.getMN_actual().ToString("#") + " / " + statHandler.getMN_max().ToString("#");
    }

    public void PostTakeFisDamage (float dmgTaken)
    {
        FisDmgText.text = dmgTaken.ToString("#");
    }

    public void PostTakeMagDamage (float dmgTaken)
    {
        MagDmgText.text = dmgTaken.ToString("#");
    }

    public void PostTakeTrueDamage (float dmgTaken)
    {
        TrueDmgText.text = dmgTaken.ToString("#");
    }

}
