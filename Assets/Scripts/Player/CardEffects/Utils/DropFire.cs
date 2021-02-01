using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFire : CardEffect
{
    public override void CardActivated(float value)
    {
        base.CardActivated(value);
        AudioManager.instance.PlayAudio("WeaponDrop");
        FindObjectOfType<Deck>().RemoveCards("FireMagic");
    }
}
