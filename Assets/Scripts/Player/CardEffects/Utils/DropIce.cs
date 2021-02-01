using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIce : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("WeaponDrop");
        FindObjectOfType<Deck>().RemoveCards("IceMagic");
    }
}
