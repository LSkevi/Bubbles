using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class BreakableWall : MonoBehaviour, IDamageable
    {
        public void OnTakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}
