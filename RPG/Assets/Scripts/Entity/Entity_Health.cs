using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamgable
{
    private Slider healthBar;

    private Entity_VFX entityVFX;
    private Entity entity;
    private Entity_Stats stats;

    protected float currentHp;
    protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private Vector2 onDamageKncokback = new Vector2(1.5f, 2.5f);
    [Range(0, 1)]
    [SerializeField] private float heavyDamageThreshold = .3f;
    [SerializeField] private float heavyKnockDuration = .5f;
    [SerializeField] private Vector2 onHeavyDamageKncokback = new Vector2(7, 7);

    private void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar  =GetComponentInChildren<Slider>();
        stats= GetComponentInChildren<Entity_Stats>();

        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        float duration = CalculateDuration(damage);

        Vector2 knockback = CalculeKnockback(damage, damageDealer);

        entityVFX?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        reduceHp(damage);
    }

    protected void reduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp < 0)
            Die();
    }

    private void Die()
    {
        isDead = true;

        entity.EntityDeath();
    }

    private void UpdateHealthBar() => healthBar.value = currentHp / stats.GetMaxHealth();

    private Vector2 CalculeKnockback(float damage, Transform damageDealer)
    {

        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsheavyDamage(damage) ? onHeavyDamageKncokback : onDamageKncokback;
        knockback.x = knockback.x * direction;

        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsheavyDamage(damage) ? heavyKnockDuration : knockbackDuration;
    }

    private bool IsheavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
}
