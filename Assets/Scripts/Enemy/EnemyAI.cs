using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Intention
{
    Attack,
    Defend
};

public abstract class EnemyAI : MonoBehaviour
{
    public Intention intention = Intention.Attack;
    Rigidbody2D rb;
    protected Transform playerTransform;

    [Header("Movement")]
    protected SpriteRenderer enemySprite;
    [HideInInspector] public Transform target;

    public float moveSpeed = 200.0f;
    public float moveAbleRange;

    [HideInInspector] public Animator enemyAnimator;

    protected bool moveToPlayer;
    [HideInInspector] public bool decideIntention;
    public LayerMask hitAble;
    protected Collider2D[] hit;

    protected List<GridSquare> selectableTiles = new List<GridSquare>();

    [HideInInspector] public GameMaster gameMaster;

    [HideInInspector] public EnemyStats enemyStats;

    public SpriteRenderer marker;

    public Sprite delayMarker;
    public Sprite pointerMarker;

    float actionDelayAmount = 0.5f;
    float actionDelay;

    [HideInInspector] public bool selected;

    [Header("Effects")]
    bool freeze;
    int freezeTurnCountDown;
    Effect freezeEffect;

    bool burn;
    float damagePerTurn;
    int burnTurnCountDown;
    Effect burnEffect;

    public void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("PM").GetComponent<GameMaster>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemyStats = GetComponent<EnemyStats>();
        decideIntention = true;
        marker.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x < transform.position.x)
                enemySprite.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (playerTransform.position.x > transform.position.x)
                enemySprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (actionDelay <= 0)
        {
            marker.sprite = pointerMarker;
            if (!freeze)
            {
                enemyAnimator.enabled = true;
                EnemyLogic();
            }
            else
            {
                if (moveToPlayer)
                {
                    moveToPlayer = false;
                    gameMaster.EnemyUpdateDone();
                }
                enemyAnimator.enabled = false;
            }
        }
        else
        {
            marker.sprite = delayMarker;
            actionDelay -= Time.deltaTime;
        }
    }

    public abstract void EnemyLogic();

    public void ActivateEnemy()
    {
        selected = true;
        if (freeze)
        {
            freezeTurnCountDown--;
            if(freezeTurnCountDown <= 0)
            {
                freezeTurnCountDown = 0;
                freeze = false;
                AudioManager.instance.PlayAudio("Ice");
                freezeEffect.Destroy();
            }
        }

        if (burn)
        {
            burnTurnCountDown--;
            enemyStats.Damage(damagePerTurn);
            if(burnTurnCountDown <= 0)
            {
                burnTurnCountDown = 0;
                burn = false;
                burnEffect.Destroy();
            }
        }

        actionDelay = Random.Range(actionDelayAmount - (actionDelayAmount / 2.0f), actionDelayAmount + (actionDelayAmount / 2.0f));

        moveToPlayer = true;
        marker.gameObject.SetActive(true);
    }

    public void DeactivateEnemy()
    {
        selected = false;
        marker.gameObject.SetActive(false);
        decideIntention = true;
    }

    void FixedUpdate()
    {
        if (decideIntention)
        {
            decideIntention = false;
            Random.InitState((int)Time.time);
            int choice = Random.Range(0, 2);
            intention = (Intention)choice;
        }
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position);
            if (direction.magnitude <= 0.2f)
            {
                enemyAnimator.SetBool("Walk", false);
                AudioManager.instance.StopAudio("Movement");
                transform.position = target.transform.position;
                Attack();
                target = null;
            }
            else
            {
                if(!enemyAnimator.GetBool("Walk"))
                {
                    AudioManager.instance.PlayAudio("Movement");
                    enemyAnimator.SetBool("Walk", true);
                }

                rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Freeze(int freezeCount, Effect effect)
    {
        if (freezeEffect != null)
            Destroy(freezeEffect.gameObject);

        effect.transform.parent = transform;
        freeze = true;
        freezeTurnCountDown = freezeCount;
        freezeEffect = effect;
    }

    public void Burn(int burnCount, float damage, Effect effect)
    {
        burn = true;
        effect.transform.parent = transform;
        damagePerTurn = damage;
        burnTurnCountDown = burnCount;
        burnEffect = effect;
    }

    public abstract void Attack();
}
