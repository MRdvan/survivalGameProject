using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ihuntable
{
    void TakeDamage(int damage);
    void Die();
    void SpawnResources();

}

