using UnityEngine;
using System.Collections;

public class Swing : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("SwordHit");
        EffectMultiple();
    }
}
