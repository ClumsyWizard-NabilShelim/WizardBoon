using UnityEngine;
using System.Collections.Generic;

public abstract class CardEffect : MonoBehaviour
{
    [HideInInspector] public List<GridSquare> effectedGridSquares = new List<GridSquare>();
    [HideInInspector] public GridSquare selectedGridSquare;
    public GameObject effect;
    float effectValue;

    public virtual void CardActivated(float value)
    {
        effectValue = value;
    }

    public void EffectMultiple()
    {
        foreach (GridSquare square in effectedGridSquares)
        {
            //Instantiate(effect, square.transform.position, Quaternion.identity);
            if (square.enemyStats != null)
                square.enemyStats.Damage(effectValue);
        }
    }

    public void EffectSingle(GridSquare square = null)
    {
        if (square != null)
        {
            //Instantiate(effect, square.transform.position, Quaternion.identity);
            if (square.enemyStats != null)
                square.enemyStats.Damage(effectValue);
        }
        else
        {
            //Instantiate(effect, selectedGridSquare.transform.position, Quaternion.identity);
            if (selectedGridSquare.enemyStats != null)
                selectedGridSquare.enemyStats.Damage(effectValue);
        }
    }
}
