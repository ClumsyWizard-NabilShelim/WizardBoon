using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    public float attackRadius;
    public LayerMask attackableLayer;
    PlayerStats stats;

    public override void EnemyLogic()
    {
        if (moveToPlayer)
        {
            if (intention == Intention.Attack)
            {
                hit = Physics2D.OverlapCircleAll(transform.position, moveAbleRange, hitAble);
                if (hit.Length != 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        GridSquare square = hit[i].GetComponent<GridSquare>();
                        if (square.enemyStats == null && !square.hasPlayer)
                        {
                            selectableTiles.Add(square);
                        }
                    }
                    moveToPlayer = false;
                }
            }
            else
            {
                enemyStats.IncreaseDefence();
                moveToPlayer = false;
                gameMaster.EnemyUpdateDone();
            }
        }
        else if (target == null && intention != Intention.Defend)
        {
            GridSquare closedGrid = null;
            if (selectableTiles.Count != 0)
            {
                float closedDist = Mathf.Infinity;
                for (int i = 0; i < selectableTiles.Count; i++)
                {
                    if (playerTransform == null)
                        break;

                    float dist = (playerTransform.position - selectableTiles[i].transform.position).magnitude;
                    if (dist < closedDist)
                    {
                        closedDist = dist;
                        closedGrid = selectableTiles[i];
                    }
                }

                selectableTiles.Clear();
            }

            if (closedGrid != null)
            {
                target = closedGrid.transform;
            }
        }
    }

    public override void Attack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, attackRadius, attackableLayer);
        if(col != null)
        {
            enemyAnimator.SetTrigger("Attack");
            stats = col.GetComponent<PlayerStats>();
        }
        else
        {
            gameMaster.EnemyUpdateDone();
        }
    }

    void DamagePlayer()
    {
        stats.Damage(enemyStats.damage);
        gameMaster.EnemyUpdateDone();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, moveAbleRange);
    }
}
