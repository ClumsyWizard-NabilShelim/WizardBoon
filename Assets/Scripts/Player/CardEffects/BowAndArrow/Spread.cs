using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("BowPull");
        EffectMultiple();
    }
}
