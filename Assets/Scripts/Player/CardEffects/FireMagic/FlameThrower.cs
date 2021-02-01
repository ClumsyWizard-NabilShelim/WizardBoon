using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : CardEffect
{
    public GameObject burnEffect;
    public float burnDamage;
    public int effectTurnCount;

    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("Fire");

        if(selectedGridSquare.enemyStats != null)
        {
            Effect effect = Instantiate(burnEffect, selectedGridSquare.enemyStats.transform.position, burnEffect.transform.rotation).GetComponent<Effect>();
            selectedGridSquare.enemyStats.enemyAI.Burn(effectTurnCount, burnDamage, effect);
        }

        EffectSingle();
    }
}
