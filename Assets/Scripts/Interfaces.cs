using UnityEngine;

public interface IEnemy
{
    void Kill();
    void DealDamage(GameObject targetObject);
}

public interface IDamagable
{
    void Damage(int damageToTake, GameObject origin = null);
}
