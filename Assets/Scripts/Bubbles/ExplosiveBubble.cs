using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Bubbles
{
    internal class ExplosiveBubble : BubbleBase
    {
        private float moveDirection;
        public float explosionRadius = 5f;
        public int damage = 1;

        private void Awake()
        {
            friendlyTag.Add("Ground");
            friendlyTag.Remove("Player");
        }

        protected override void BubbleLogic() 
        {
            
        }

        void SetDirection()
        {
            if (PlayerManager.Instance != null
                && PlayerManager.Instance.PlayerMovement != null)
            {
                // Define a direção com base no valor de isFacingRight
                bool direction = PlayerManager.Instance.PlayerMovement.isFacingRight;

                // Agora, definimos a direção com base no isFacingRight
                moveDirection = direction ? 1f : -1f;
            }
        }

        private void Explode()
        {
            // Explosão afeta objetos próximos
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D collider in colliders)
            {
                // Verifica se o objeto implementa a interface IBreakable
                IDamageable breakable = collider.GetComponent<IDamageable>();
                if (breakable != null)
                {
                    breakable.OnTakeDamage(damage); // Chama o método de quebra
                }
            }

            Debug.Log("Explosão realizada!");
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (!friendlyTag.Contains(collision.gameObject.tag))
            {
                // Instancia a VFX de explosao
                Explode();

                Debug.Log($"Quem me estourou foi: {collision.gameObject.tag}");
                PopBubble();
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision) { }

        // Apenas para debug
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
