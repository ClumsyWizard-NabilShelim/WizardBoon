using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : CardEffect
{
    public int healAmount;
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        FindObjectOfType<PlayerStats>().Heal(healAmount);
        //EffectSingle();
    }
}
