using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("BowPull");
        EffectMultiple();
    }
}
