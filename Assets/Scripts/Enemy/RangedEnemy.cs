using UnityEngine;
using System.Collections;

public class RangedEnemy : EnemyAI
{
    public float attackRadius;
    public LayerMask attackableLayer;

    Collider2D playerCol;
    PlayerStats stats;

    public override void EnemyLogic()
    {
        if (playerTransform == null)
            return;

        if (moveToPlayer)
        {
            if (intention == Intention.Attack)
            {
                playerCol = Physics2D.OverlapCircle(transform.position, attackRadius, attackableLayer);
                if (playerCol != false)
                {
                    Attack();
                    moveToPlayer = false;
                }
                else
                {
                    hit = Physics2D.OverlapCircleAll(transform.position, moveAbleRange, hitAble);
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
        if(playerCol != false)
        {
            enemyAnimator.SetTrigger("Attack");
            stats = playerCol.GetComponent<PlayerStats>();
        }
        else
        {
            gameMaster.EnemyUpdateDone();
        }
    }

    public void DamagePlayer()
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
