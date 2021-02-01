using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : CardEffect
{
    public GameObject freezeEffect;
    public int effectTurnCount;

    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("Ice");
        
        if(selectedGridSquare.enemyStats != null)
        {
            Effect effect = Instantiate(freezeEffect, selectedGridSquare.transform.position, Quaternion.identity).GetComponent<Effect>();
            selectedGridSquare.enemyStats.enemyAI.Freeze(effectTurnCount, effect);
        }

        EffectSingle();
    }
}
