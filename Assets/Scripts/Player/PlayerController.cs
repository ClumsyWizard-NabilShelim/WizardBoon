using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [HideInInspector] public PlayerManager PM;

    [Header("Movement")]
    SpriteRenderer playerSprite;
    [HideInInspector] public Transform target;

    public float moveSpeed = 200.0f;
    public float moveAbleRange;

    Animator playerAnimator;
    public virtual void Start()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            if (target.position.x < transform.position.x)
                playerSprite.flipX = true;
            if (target.position.x > transform.position.x)
                playerSprite.flipX = false;

            Vector2 direction = (target.transform.position - transform.position);
            if(direction.magnitude <= 0.2f)
            {
                AudioManager.instance.StopAudio("Movement");
                playerAnimator.SetBool("Walk", false);
                transform.position = target.transform.position;
                PM.MovementDone();
                target = null;
            }
            else
            {
                if(!playerAnimator.GetBool("Walk"))
                {
                    AudioManager.instance.PlayAudio("Movement");
                    playerAnimator.SetBool("Walk", true);
                }
                rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, moveAbleRange);
    }
}
