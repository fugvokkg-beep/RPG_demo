using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;
    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMAterial;
    [SerializeField] private float onDamageVfxDuration = .1f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.red;
    [SerializeField] private GameObject hitVfx;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void CreateOnHitVfx(Transform target)
    {
        GameObject vfx = Instantiate(hitVfx, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(onDamageVfxCo());
    }

    private IEnumerator onDamageVfxCo()
    {
        sr.material = onDamageMAterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;

    }
}

