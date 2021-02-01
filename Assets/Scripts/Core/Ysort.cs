using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ysort : MonoBehaviour
{
	[SerializeField] private bool runOnce = true;

	[SerializeField] private SpriteRenderer spriteRenderer = null;

	void LateUpdate()
    {
		float basePos = spriteRenderer.bounds.min.y;
		spriteRenderer.sortingOrder = (int)(basePos * -100);
		if (runOnce)
			Destroy(this);
    }
}
