using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpIce : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("Ice");
        EffectSingle();
    }
}
