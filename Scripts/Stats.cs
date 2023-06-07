using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Diego, se que no te gustan las variables y fx asi,
pero es por claridad visual
sorry xoxo
*/

public class Stats : MonoBehaviour
{
        bool[] Trait = new bool[10];

        //ATK
        float ATK_base = 0f;
        float ATK_bonus = 0f;
        float ATK_total = 0f;

        //Status
        public bool _alive = true;

        //HP ---
        float HP_base = 0f;
        float HP_bonus = 0f;
        float HP_max = 0f; //base + bonus
        float HP_actual = 0f;

        //Mana ---
        float MN_base = 0f;
        //float MN_bonus = 0f; //not used
        //float MN_total = 0f; //base + bonus
        float MN_actual = 0f;
        float MN_max = 0f;
        bool ManaChargeAviable = true;
        float ManaChargeModifier = 1f;
        //Agregar Multiplicadores de carga de Mana

        //Fuerza FZA ---
        float FZA_base = 0f;
        float FZA_bonus = 0f;
        float FZA_total = 0f; //base + bonus
        //float FZA_actual = 0f;

        //Poder POD
        float POD_base = 0f;
        float POD_bonus = 0f;
        float POD_total = 0f; //base + bonus
        //float POD_actual = 0f;

        //Armadura ARM
        float ARM_base = 0f;
        float ARM_bonus = 0f;
        float ARM_total = 0f; //base + bonus
        //float ARM_actual = 0f;

        //Resistencia RES
        float RES_base = 0f;
        float RES_bonus = 0f;
        float RES_total = 0f; //base + bonus
        //float RES_actual = 0f;

        //Agilidad AGL
        float AGL_base = 0f;
        float AGL_bonus = 0f;
        float AGL_total = 0f; //base + bonus
        //float AGL_actual = 0f;

        //Velocidad VEL
        float VEL_base = 0f;
        float VEL_bonus = 0f;
        float VEL_total = 0f; //base + bonus
        //float VEL_actual = 0f;

        //Precicion PRE
        float PRE_base = 0f;
        float PRE_bonus = 0f;
        float PRE_total = 0f; //base + bonus
        //float PRE_actual = 0f;

        //Tenacidad TEN
        float TEN_base = 0f;
        float TEN_bonus = 0f;
        float TEN_total = 0f; //base + bonus

        //Concentracion CON
        float CON_base = 0f;
        float CON_bonus = 0f;
        float CON_total = 0f; //base + bonus


        //Mod Esquivar
        float AVO_mod;

        //Escudos
        float Shield = 0f;
        float MaxShield = 1000f;
        bool ShieldActive = false;

        //Damage Stats
        public int HitCant = 1; //Cantidad de veces que atacará el personaje (max 5)
        public int HitCantReduction = 0; //Al recibir un ataque, cantidad de ataques que podra negar. Por defecto es 0
        //Al atacar la unidad toma su HitCant y resta el HitCantReduction del oponente
        //Cantidad de ataques => HC_atack - HCR_defend

        //Multiplicador de Daño
        public float DamageMultiplier = 1f; //Multiplicador de Daño

        //Reductor de Daño
        public float DamageReductionFlat = 0f;
        public float DamageReductionPerc = 0f;

        //Heal Modifier
        float healModifier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<Trait.Length;i++){Trait[i]=false;} //llenar con false
        CheckAlive();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        CheckAlive();
    }

    //fx

//traits
    public void AddTrait (int i)
    {
        Trait[i] = true;
    }

    public void RemoveTrait (int i)
    {
        Trait[i] = false;
    }

    public bool GetTrait (int i)
    {
        return Trait[i];
    }

//status
    public bool getAlive () {return _alive;}

    public void CheckAlive () //alive check
    {
        if(HP_actual <= 0)
        {
            _alive = false;
        }else
        {
            _alive = true;
        }
    }

//ATK ------------------------------------------ Ataque de Arma
    public float getATK_total () {return ATK_total;}
    public float getATK_bonus () {return ATK_bonus;}
    public float getATK_base () {return ATK_base;}

    public void UpdateATK ()
    {
        ATK_total = ATK_base + ATK_bonus;
        if(ATK_total < 0)
            ATK_total = 0;
    }

    public void setATK_base (float atk) {ATK_base = atk;UpdateATK();}
    public void setATK_bonus (float atk) {ATK_bonus = atk;UpdateATK();}

    public void AddATK_bonusFlat (float bon)
    {
        ATK_bonus += bon;
        UpdateATK();
    }

//HP ---------------------------------------------- H P
    public float getHP_max (){return HP_max;}
    public float getHP_base () {return HP_base;}
    public float getHP_actual () {return HP_actual;}

    public void CheckHP ()
    {
        if(HP_actual >= HP_max)
            HP_actual = HP_max;
        if(HP_actual <= 0)
        {
            HP_actual = 0;
            _alive = false;
        }
    }

    public void UpdateHP () //
    {
        HP_max = HP_base + HP_bonus;
    }

    public void TakeHP (float d)
    {
        float d_postShield;

        if (ShieldActive)
        {
            if(Shield-d >= 0) //escudo mayor q daño
            {
                Shield -= d;
                d_postShield = 0;
                UpdateShield();
            }else{ //daño mayor q escudo
                d_postShield = d - Shield;
                Shield = 0;
                UpdateShield();
            }
        }else{
            d_postShield = d;
        }

        HP_actual -= d_postShield;
        //gameObject.BroadcastMessage("OnTakeDamage",d,SendMessageOptions.DontRequireReceiver);
    }

    public void InitHP () {HP_actual = HP_max;}
    public void InitHP (float h) {HP_actual = h;}

    public void setHP_actual (float hp){HP_actual = hp;UpdateHP();}
    public void setHP_base (float hp){HP_base = hp; UpdateHP();}
    public void setHP_bonus (float hp){HP_bonus = hp;UpdateHP();} //usar para reset

    public void AddHPBonusFlat (float bon)
    {
        if(bon <= 0)
        {
            bon = 0;
        }
        HP_bonus += bon;
        UpdateHP();
    }

    public void AddHPBaseBonusPerc (float bon) //poner el bonus como numero completo (30% = 30)
    {
        AddHPBonusFlat(HP_base * (bon/100));
    }

    public void AddHPTotalBonusPerc (float bon) //poner el bonus como numero completo (30% = 30)
    {
        AddHPBonusFlat(HP_max * (bon/100));
    }

//HEAL ---------------------------------------------------- HEAL
    public void HealFlat (float heal)
    {
        gameObject.BroadcastMessage("PreHeal",null,SendMessageOptions.DontRequireReceiver);
        HP_actual += heal * healModifier;
        gameObject.BroadcastMessage("PostHeal",null,SendMessageOptions.DontRequireReceiver);
        CheckHP();
    }

    public void HealPercentMax (float perc)
    {
        HP_actual += (HP_max * (1 + (perc/100)))*healModifier;
        CheckHP();
    }

//MANA ------------------------------------------------------MANA
    public float getMN_base (){return MN_base;}
    public float getMN_actual (){return MN_actual;}
    public float getMN_max (){return MN_max;}

    public bool UltReady () //<-- check Max Mana
    {
        if (MN_actual >= MN_max)
        {
            MN_actual = MN_max;
            return true;
        }else{
            return false;
        }
    }

    public void UseAllMN (){MN_actual = 0;}
    public void setMN_max (float m){MN_max = m;UpdateMN();}

    public void UpdateMN () //
    {
        if (MN_actual >= MN_max){
            MN_actual = MN_max;
        }
        if (MN_actual < 0){
            MN_actual = 0;
        }
    }

    public void ChargeMNAttack ()
    {
        float m;
        m = (MN_max * 0.1f) + (ATK_total * (1 + 0.005f * PRE_total)) + ((FZA_total + POD_total)/MN_max);
        if(ManaChargeAviable)
            MN_actual += m * ManaChargeModifier;
        UpdateMN();
    }

    public float ChargeMNAttackValue ()
    {
        float m;
        m = (MN_max * 0.1f) + (ATK_total * (1 + 0.005f * PRE_total)) + ((FZA_total + POD_total)/MN_max);
        return m * ManaChargeModifier;
    }

    public void ChargeMNSpell ()
    {
        float m;
        m = (MN_max * 0.25f) + ((FZA_total + POD_total)/(MN_max * 0.5f));
        if(ManaChargeAviable)
            MN_actual += m * ManaChargeModifier;
        UpdateMN();
        //Debug.Log("Se ha cargado " + m + " Mana");
    }

    public float ChargeMNSpellValue ()
    {
        float m;
        m = (MN_max * 0.25f) + ((FZA_total + POD_total)/(MN_max * 0.5f));
        return m * ManaChargeModifier;
    }

    public void AddFlatMN (float mana)
    {
        MN_actual += mana;
        UpdateMN();
    }

    public void AddPercMaxMN (float perc)
    {
        AddFlatMN ((perc/100)*MN_max);
    }

    public void AddPercActualMN (float perc)
    {
        AddFlatMN ((perc/100)*MN_actual);
    }

    public void InitializeMN ()
    {
        MN_actual = 0;
        UpdateMN();
    }

    public void CanCharge (bool canCharge)
    {
        ManaChargeAviable = canCharge;
    }

    //ManaChargeModifier se suma de forma multiplicativa
    public void AddManaChargeModifierBonus(float percent) //Añadir como porcentaje
    {
        ManaChargeModifier = ManaChargeModifier * (1+(percent/100));
    }

    public void RestManaChargeModifierBonus(float percent)
    {
        ManaChargeModifier = ManaChargeModifier / (1+(percent/100));
    }

    public void AddManaChargeModifierPenalty(float percent) //Añadir como porcentaje
    {
        ManaChargeModifier = ManaChargeModifier * (1-(percent/100));
    }

    public void RestManaChargeModifierPenalty(float percent) //Añadir como porcentaje
    {
        ManaChargeModifier = ManaChargeModifier / (1-(percent/100));
    }

    public void ResetManaChargeModifier ()
    {
        ManaChargeModifier = 1f;
    }

//FZA----------------------------------------------------------Fuerza
    public float getFZA_total (){return FZA_total;}
    public float getFZA_base (){return FZA_base;}
    public float getFZA_bonus (){return FZA_bonus;}

    public void setFZA_base(float x){FZA_base = x;UpdateFZA();}

    public void UpdateFZA ()
    {
        FZA_total = FZA_base + FZA_bonus;
        if (FZA_total <= 0)
            FZA_total = 0;
        if (FZA_total > 500) // limite superior
            FZA_total = 500;
    }

    public void AddFZABonusFlat (float b)
    {
        FZA_bonus += b;
        UpdateFZA();
    }

    public void AddFZABonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddFZABonusFlat(FZA_base*(p/100));
    }

    public void AddFZABonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddFZABonusFlat(FZA_total*(p/100));
    }

//POD -------------------------------------------------- Poder
    public float getPOD_total (){return POD_total;}
    public float getPOD_base (){return POD_base;}
    public float getPOD_bonus (){return POD_bonus;}

    public void setPOD_base(float x){POD_base = x;UpdatePOD();}

    public void UpdatePOD ()
    {
        POD_total = POD_base + POD_bonus;
        if (POD_total <= 0)
            POD_total = 0;
        if (POD_total > 500) // limite superior
            POD_total = 500;
    }

    public void AddPODBonusFlat (float b)
    {
        POD_bonus += b;
        UpdatePOD();
    }

    public void AddPODBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddPODBonusFlat(POD_base*(p/100));
    }

    public void AddPODBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddPODBonusFlat(POD_total*(p/100));
    }

//ARM -------------------------------------------------- Armadura
    public float getARM_total (){return ARM_total;}
    public float getARM_base (){return ARM_base;}
    public float getARM_bonus (){return ARM_bonus;}

    public void setARM_base(float x){ARM_base = x;UpdateARM();}

    public void UpdateARM ()
    {
        ARM_total = ARM_base + ARM_bonus;
        if (ARM_total <= 0)
            ARM_total = 0;
        if (ARM_total > 500) // limite superior
            ARM_total = 500;
    }

    public void AddARMBonusFlat (float b)
    {
        ARM_bonus += b;
        UpdateARM();
    }

    public void AddARMBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddARMBonusFlat(ARM_base*(p/100));
    }

    public void AddARMBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddARMBonusFlat(ARM_total*(p/100));
    }

//RES ------------------------------------------------- Resistencia
    public float getRES_total (){return RES_total;}
    public float getRES_base (){return RES_base;}
    public float getRES_bonus (){return RES_bonus;}

    public void setRES_base(float x){RES_base = x;UpdateRES();}

    public void UpdateRES ()
    {
        RES_total = RES_base + RES_bonus;
        if (RES_total <= 0)
            RES_total = 0;
        if (RES_total > 500) // limite superior
            RES_total = 500;
    }

    public void AddRESBonusFlat (float b)
    {
        RES_bonus += b;
        UpdateRES();
    }

    public void AddRESBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddRESBonusFlat(RES_base*(p/100));
    }

    public void AddRESBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddRESBonusFlat(RES_total*(p/100));
    }

//AGL ------------------------------------------------- Agilidad
    public float getAGL_total (){return AGL_total;}
    public float getAGL_base (){return AGL_base;}
    public float getAGL_bonus (){return AGL_bonus;}

    public void setAGL_base(float x){AGL_base = x;UpdateAGL();}

    public void UpdateAGL ()
    {
        AGL_total = AGL_base + AGL_bonus;
        if (AGL_total <= 0)
            AGL_total = 0;
        if (AGL_total > 500) // limite superior
            AGL_total = 500;
    }

    public void AddAGLBonusFlat (float b)
    {
        AGL_bonus += b;
        UpdateAGL();
    }

    public void AddAGLBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddAGLBonusFlat(AGL_base*(p/100));
    }

    public void AddAGLBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddAGLBonusFlat(AGL_total*(p/100));
    }

//VEL ------------------------------------------------ Velocidad
    public float getVEL_total (){return VEL_total;}
    public float getVEL_base (){return VEL_base;}
    public float getVEL_bonus (){return VEL_bonus;}

    public void setVEL_base(float x){VEL_base = x;UpdateVEL();}

    public void UpdateVEL ()
    {
        VEL_total = VEL_base + VEL_bonus;
        if (VEL_total <= 0)
            VEL_total = 0;
        if (VEL_total > 500) // limite superior
            VEL_total = 500;
    }

    public void AddVELBonusFlat (float b)
    {
        VEL_bonus += b;
        UpdateVEL();
    }

    public void AddVELBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddVELBonusFlat(VEL_base*(p/100));
    }

    public void AddVELBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddVELBonusFlat(VEL_total*(p/100));
    }

//PRE ------------------------------------------------ Precicion
    public float getPRE_total (){return PRE_total;}
    public float getPRE_base (){return PRE_base;}
    public float getPRE_bonus (){return PRE_bonus;}

    public void setPRE_base(float x){PRE_base = x;UpdatePRE();}

    public void UpdatePRE ()
    {
        PRE_total = PRE_base + PRE_bonus;
        if (PRE_total <= 0)
            PRE_total = 0;
        if (PRE_total > 500) // limite superior
            PRE_total = 500;
    }

    public void AddPREBonusFlat (float b)
    {
        PRE_bonus += b;
        UpdatePRE();
    }

    public void AddPREBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddPREBonusFlat(PRE_base*(p/100));
    }

    public void AddPREBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddPREBonusFlat(PRE_total*(p/100));
    }

//TEN --------------------------------------------------- Tenacidad
    public float getTEN_total (){return TEN_total;}
    public float getTEN_base (){return TEN_base;}
    public float getTEN_bonus (){return TEN_bonus;}

    public void setTEN_base(float x){TEN_base = x;UpdateTEN();}

    public void UpdateTEN ()
    {
        TEN_total = TEN_base + TEN_bonus;
        if (TEN_total <= 0)
            TEN_total = 0;
        if (TEN_total > 500) // limite superior
            TEN_total = 500;
    }

    public void AddTENBonusFlat (float b)
    {
        TEN_bonus += b;
        UpdateTEN();
    }

    public void AddTENBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddTENBonusFlat(TEN_base*(p/100));
    }

    public void AddTENBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddTENBonusFlat(TEN_total*(p/100));
    }

//CON --------------------------------------------------- Concentracion
    public float getCON_total (){return CON_total;}
    public float getCON_base (){return CON_base;}
    public float getCON_bonus (){return CON_bonus;}

    public void setCON_base(float x){CON_base = x;UpdateCON();}

    public void UpdateCON ()
    {
        CON_total = CON_base + CON_bonus;
        if (CON_total <= 0)
            CON_total = 0;
        if (CON_total > 500) // limite superior
            CON_total = 500;
    }

    public void AddCONBonusFlat (float b)
    {
        CON_bonus += b;
        UpdateCON();
    }

    public void AddCONBonusPercBase (float p) //poner numero entero 30% = 30
    {
        AddCONBonusFlat(CON_base*(p/100));
    }

    public void AddCONBonusPercActual (float p) //poner numero entero 30% = 30
    {
        AddCONBonusFlat(CON_total*(p/100));
    }

    //DAMAGE ------------------------------------------------------ DAMAGE
    //FALTA INCORPORAR LA MITIGACION DE DAÑO <<<<<<<<<<<<<

//DAÑO RETORNA VALORES ----------------- RETORNO DE VALORES
    public float DamageFisValue (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeFisDamage",null,SendMessageOptions.DontRequireReceiver);

        d = dmg * (1-((ARM_total/(ARM_total+100)) * (1+ TEN_total/1000))); //reducción de daño

        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;

        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeFisDamage",d,SendMessageOptions.DontRequireReceiver);
        return d;
    }

    public float DamageMagValue (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeMagDamage",null,SendMessageOptions.DontRequireReceiver);
        d = dmg * (1-((RES_total/(RES_total+100)) * (1+ TEN_total/1000))); //reducción de daño
        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;
        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeMagDamage",d,SendMessageOptions.DontRequireReceiver);
        return d;
    }

    public float DamageTrueValue (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeTrueDamage",null,SendMessageOptions.DontRequireReceiver);
        d = dmg * (1 - TEN_total/200); //reducción de daño
        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;
        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeTrueDamage",d,SendMessageOptions.DontRequireReceiver);
        return d;
    }

//Daño VOID ---- NO RETORNAR VALORES
    public void DamageFis (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeFisDamage",null,SendMessageOptions.DontRequireReceiver);
        d = dmg * (1-((ARM_total/(ARM_total+100)) * (1+ TEN_total/1000))); //reducción de daño
        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;
        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeFisDamage",d,SendMessageOptions.DontRequireReceiver);
    }

    public void DamageMag (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeMagDamage",null,SendMessageOptions.DontRequireReceiver);
        d = dmg * (1-((RES_total/(RES_total+100)) * (1+ TEN_total/1000))); //reducción de daño
        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;
        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeMagDamage",d,SendMessageOptions.DontRequireReceiver);
    }

    public void DamageTrue (float dmg)
    {
        float d;
        gameObject.BroadcastMessage("PreTakeTrueDamage",null,SendMessageOptions.DontRequireReceiver);
        d = dmg * (1 - TEN_total/200); //reducción de daño
        d = (d * (1 - DamageReductionPerc))-DamageReductionFlat;
        TakeHP(d); //recibir daño <----
        gameObject.BroadcastMessage("PostTakeTrueDamage",d,SendMessageOptions.DontRequireReceiver);
    }

//SHIELD ---------------------------------------- SHIELD
    public float getShield (){return Shield;}
    public float getMaxShield () {return MaxShield;}

    public void UpdateShield()
    {
        if(Shield < 0)
        {
            Shield = 0;
            ShieldActive = false;
        }else{
            ShieldActive = true;
        }
    }

    public void AddShield (float s)
    {
        Shield += s;
        UpdateShield();
    }

    public void setShield (float s){Shield = s;UpdateShield();}

//DAMAGE MULTIPLIER ------------------------------------------------
    //Los multiplicadores de dmg se suman porque multiplicados crecen muy rapido

    public float getDamageMultiplier () {return DamageMultiplier;}
    public void setDamageMultiplier (float dm){DamageMultiplier = dm;}
    public void resetDamageMultiplier () {DamageMultiplier = 1;}

    public void AddDamageMultiplier (float percent) //Añadir porcentaje como numero -> 30% = 30
    {
        DamageMultiplier += (percent/100);
    }

    public void RestDamageMultiplier (float percent)
    {
        DamageMultiplier -= (percent/100);
    }

//DAMAGE REDUCTION --------------------------------------------

    public float getDamageReductionFlat() {return DamageReductionFlat;}
    public void setDamageReductionFlat(float dr) {DamageReductionFlat = dr;}
    public void resetDamageReductionFlat() {DamageReductionFlat = 0;}

    public void AddDamageReductionFlat (float drf){DamageReductionFlat += drf;}
    public void RestDamageReductionFlat (float drf){DamageReductionFlat -= drf;}

    public float getDamageReductionPerc() {return DamageReductionPerc;}
    public void setDamageReductionPerc(float dr) {DamageReductionPerc = dr;}
    public void resetDamageReductionPerc() {DamageReductionPerc = 1;}

    public void AddDamageReductionPerc (float percent){DamageReductionPerc = DamageReductionPerc * (1-(percent/100));}

    public void RestDamageReduction (float percent){DamageReductionPerc = DamageReductionPerc / (1-(percent/100));}

//CONTADOR DE HITS ------------------------------------------------------
    public int getHitCant (){return HitCant;}
    public void setHitCant (int n){HitCant = n;}
    public void resetHitCant (){HitCant = 1;}
    public void AddHitCant (int n){HitCant+=n;UpdateHitCant();}
    public void RestHitCant (int n){HitCant-=n;UpdateHitCant();}

    void UpdateHitCant ()
    {
        if(HitCant < 1)
            HitCant = 1;
        if(HitCant > 5)
            HitCant = 5;
    }

//REDUCTOR DE HITS ------------------------------------------------------
    public int getHitCantReduction () {return HitCantReduction;}
    public void setHitCantReduction (int n){HitCantReduction = n;}
    public void resetHitCantReduction (){HitCant = 0;}
    public void AddHitCantReduction (int n){HitCantReduction+=n;UpdateHitCantReduction();}
    public void RestHitCantReduction (int n){HitCantReduction-=n;UpdateHitCantReduction();}

      void UpdateHitCantReduction ()
    {
        if(HitCant < 0)
            HitCant = 0;
        if(HitCant > 5)
            HitCant = 5;
    }

//HIT CHANCE ------------------------------
    public float getHITChance (){return ((3*PRE_total)+(AGL_total/2))/2;}

//AVOID CHANCE ------------------
    public float getAVOChance (){return ((VEL_total)+(AGL_total/2))/2;}

//BASE CRIT CHANCE
    public float getCRITChance () {return (PRE_total * (1+(AGL_total/100)));}
}
