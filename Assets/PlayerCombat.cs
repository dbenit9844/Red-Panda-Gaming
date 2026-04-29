using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Hitbox lightHitbox;
    public Hitbox heavyHitbox;
    public float lightAttackDuration = 0.2f;
    public float heavyAttackDuration = 0.3f;
    private bool isAttacking;

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
