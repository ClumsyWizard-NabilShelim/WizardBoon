using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : CardEffect
{
    public GameObject burnEffect;
    public float burnDamage;
    public int effectTurnCount;

    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("Fire");

        foreach (GridSquare square in effectedGridSquares)
        {
            if (square.enemyStats != null)
            {
                Effect effect = Instantiate(burnEffect, square.enemyStats.transform.position, burnEffect.transform.rotation).GetComponent<Effect>();
                square.enemyStats.enemyAI.Burn(effectTurnCount, burnDamage, effect);
            }
        }

        EffectMultiple();
    }
}
