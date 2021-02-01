using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRain : CardEffect
{
    public GameObject freezeEffect;
    public int effectTurnCount;

    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("Ice");

        foreach (GridSquare square in effectedGridSquares)
        {
            if (square.enemyStats != null)
            {
                Effect effect = Instantiate(freezeEffect, square.enemyStats.transform.position, freezeEffect.transform.rotation).GetComponent<Effect>();
                square.enemyStats.enemyAI.Freeze(effectTurnCount, effect);
            }
        }

        EffectMultiple();
    }
}
