using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Action OnFlipped;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected StateMachine stateMachine;


    private bool facingright = true;
    public int facingDer { get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistanse;
    [SerializeField] private float wallCheckDistanse;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    private Coroutine knockbackCo;
    private bool isKnocked;


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void ReciveKnockback(Vector2 knockback, float duration)
    {
        if(knockbackCo != null)
            StopCoroutine(knockbackCo);
        knockbackCo = StartCoroutine(KnockbackCo(knockback, duration));
    }


    private IEnumerator KnockbackCo( Vector2 knockback,float duration)
    {
        isKnocked = true;
        rb.velocity = knockback;

        yield return new WaitForSeconds(duration);

        isKnocked = false;
        rb.velocity = Vector2.zero;
    }

    public virtual void EntityDeath()
    {

    }


    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        HandleFilp(xVelocity);
    }

    public void HandleFilp(float xVelocity)
    {
        if (xVelocity > 0 && facingright == false)
        {
            Filp();
        }
        else if (xVelocity < 0 && facingright == true)
        {
            Filp();
        }
    }

    public void Filp()
    {
        transform.Rotate(0, 180, 0);
        facingright = !facingright;
        facingDer = -facingDer;

        OnFlipped?.Invoke();
    }

    private void HandleCollisionDetection()
    {

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistanse, whatIsGround);

        if (secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDer, wallCheckDistanse, whatIsGround) &&
                           Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDer, wallCheckDistanse, whatIsGround);
        }
        else
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDer, wallCheckDistanse, whatIsGround);
        }
    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistanse, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistanse * facingDer, 0, 0));
        if (secondaryWallCheck != null)
        {
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistanse * facingDer, 0, 0));
        }
    }
}
