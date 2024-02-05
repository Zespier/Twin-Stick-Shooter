using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats {

    /* Possible upgrades
     * 
     * 
     * 10% of not shooting a bullet
     * 100% damage increase
     * 
     */

    [HideInInspector] public float baseHp;
    public float coreHp;
    public float _increasedHp;
    public float hpPerCoreLevel;
    public float AtkHp { get { return baseHp + coreLevel[0] * hpPerCoreLevel; } }
    public float DefHp { get { return baseHp + coreLevel[1] * hpPerCoreLevel; } } //TODO: I think is better if _increasedHp only works on SurvivalState
    public float SurvivalHp { get { return (baseHp + (coreLevel[2] * hpPerCoreLevel) + coreHp) * _increasedHp; } }

    [HideInInspector] public float baseAtk;
    public float coreAtk;
    public float atkPerCoreLevel;
    public float infusionAtk;
    public float Atk { get { return baseAtk + (coreLevel[0] * atkPerCoreLevel) + coreAtk + infusionAtk; } }

    [HideInInspector] public float baseDef;
    public float coreDef;
    public float _increasedDef;
    public float defPerCoreLevel;
    public float AtkDef { get { return baseDef + coreLevel[0] * defPerCoreLevel; } }
    public float DefDef { get { return (baseDef + (coreLevel[1] * defPerCoreLevel) + coreDef) * _increasedDef; } }
    public float SurvivalDef { get { return baseDef + coreLevel[2] * defPerCoreLevel; } }

    [HideInInspector] public float baseCrit;
    public float coreCrit;
    public float Crit { get { return baseCrit + coreCrit; } }

    [HideInInspector] public float baseCritDamage;
    public float coreCritDamage;
    public float CritDamage { get { return baseCritDamage + coreCritDamage; } }

    public int[] coreLevel = new int[3];

    public PlayerStats(float hp, float atk, float def, float crit, float critDamage) {
        baseHp = hp;
        baseAtk = atk;
        baseDef = def;
        baseCrit = crit;
        baseCritDamage = critDamage;

        _increasedDef = 1;
        _increasedHp = 1;
        hpPerCoreLevel = 10f; //TODO: This can change in future rebalance, but I think is nice to have a 1/10 increase from the start base stats
        defPerCoreLevel = 1f; //TODO: This can change in future rebalance
        atkPerCoreLevel = 0.5f; //TODO: This can change in future rebalance
    }

    /// <summary>
    /// True if hits crit, and returns the multiplied damage.
    /// False if doesn't hit crit, and return normal damage.
    /// </summary>
    /// <returns></returns>
    public (bool, float) CritAttack() {
        (bool, float) result;

        float random = Random.Range(0, 100f);
        result.Item1 = random < Crit ? true : false;
        result.Item2 = random < Crit ? Atk * (1 + CritDamage / 100f) : Atk;

        return result;
    }
    /// <summary>
    /// True if hits crit, and returns the multiplied damage.
    /// False if doesn't hit crit, and return normal damage.
    /// </summary>
    /// <returns></returns>
    public (bool, float) CritHp() {
        (bool, float) result;

        float random = Random.Range(0, 100f);
        result.Item1 = random < Crit ? true : false;
        result.Item2 = random < Crit ? SurvivalHp * (1 + CritDamage / 100f) : SurvivalHp;

        return result;
    }
    /// <summary>
    /// True if hits crit, and returns the multiplied damage.
    /// False if doesn't hit crit, and return normal damage.
    /// </summary>
    /// <returns></returns>
    public (bool, float) CritDef() {
        (bool, float) result;

        float random = Random.Range(0, 100f);
        result.Item1 = random < Crit ? true : false;
        result.Item2 = random < Crit ? DefDef * (1 + CritDamage / 100f) : DefDef;

        return result;
    }

    private List<(Coroutine, Enemy)> buffsDef = new List<(Coroutine, Enemy)>();
    public void UpdateIncreaseDef(float percentage, float percentagePerStats, List<(Coroutine, Enemy)> buffsDef) {
        this.buffsDef = buffsDef; //Safe the reference to know how many buffs we have right now.

        float multiplier = percentage / 100f;
        float multiplierPerStat = percentagePerStats / 100f;
        _increasedDef = 1 + ((multiplier + (Atk * multiplierPerStat)) * this.buffsDef.Count);
        //The Def increases proportional to the Atk of the Attack configuration
    }

    private List<(Coroutine, Enemy)> buffsHp = new List<(Coroutine, Enemy)>();
    public void UpdateIncreaseHp(float percentage, float percentagePerStats, List<(Coroutine, Enemy)> buffsHp) {
        this.buffsHp = buffsHp; //Safe the reference to know how many buffs we have right now.

        float multiplier = percentage / 100f;
        float multiplierPerStat = percentagePerStats / 100f;
        _increasedHp = 1 + ((multiplier + (DefDef * multiplierPerStat)) * this.buffsHp.Count);
        //The Hp increases proportional to the Defense of the defense configuration
    }

}
