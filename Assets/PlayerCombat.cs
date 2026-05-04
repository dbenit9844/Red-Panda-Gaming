using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Hitbox lightHitbox;
    public Hitbox heavyHitbox;
    public float lightAttackDuration = 0.2f;
    public float heavyAttackDuration = 0.3f;
    private bool isAttacking;

    void Awake()
    {
        if (lightHitbox == null || heavyHitbox == null)
        {
            Hitbox[] hitboxes = GetComponentsInChildren<Hitbox>(true);
            foreach (Hitbox hb in hitboxes)
            {
                if (hb.gameObject.name == "Hitbox") lightHitbox = hb;
                if (hb.gameObject.name == "HeavyHitbox") heavyHitbox = hb;
            }
        }
    }

    public void LightAttack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackRoutine(lightHitbox, lightAttackDuration));
    }

    public void HeavyAttack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackRoutine(heavyHitbox, heavyAttackDuration));
    }

    private System.Collections.IEnumerator AttackRoutine(Hitbox hitbox, float duration)
    {
        isAttacking = true;
        hitbox.EnableHitbox();
        yield return new WaitForSeconds(duration);
        hitbox.DisableHitbox();
        isAttacking = false;
    }
}
