using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
   Idel,
   Selection,
   Danger,
}

public class GridSquare : MonoBehaviour
{
    public bool hasPlayer = false;
    public EnemyStats enemyStats;
    public ColorType colorType = ColorType.Idel;
    public Color selectedColor;
    public Color dangerColor;

    SpriteRenderer gridRenderer;

    private void Start()
    {
        gridRenderer = GetComponent<SpriteRenderer>();
        gridRenderer.enabled = false;
    }

    public void ChangeColor(ColorType type)
    {
        colorType = type;

        if (colorType == ColorType.Idel)
        {
            gridRenderer.enabled = false;
        }
        else if (colorType == ColorType.Selection)
        {
            gridRenderer.enabled = true;
            gridRenderer.color = selectedColor;
        }
        else if (colorType == ColorType.Danger)
        {
            gridRenderer.enabled = true;
            gridRenderer.color = dangerColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hasPlayer = true;
        else if (collision.CompareTag("Enemy"))
            enemyStats = collision.GetComponent<EnemyStats>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hasPlayer = false;
        else if (collision.CompareTag("Enemy"))
            enemyStats = null;
    }
}
