using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Bubbles
{
    internal class ExplosiveBubble : BubbleBase
    {
        [Header("Own Variables")]
        private float moveDirection;
        public float xThrowForce = 1f;
        public float yThrowForce = 1f;

        private bool hasExploded;
        public float explosionRadius = 5f;
        public int damage = 1;

        private void Awake()
        {
            friendlyTag.Remove("Player");
        }

        private void Start()
        {
            SetDirection();

            if (myRb != null)
                myRb.linearVelocity = new Vector2(moveDirection * xThrowForce, yThrowForce);
        }

        protected override void BubbleLogic() { }

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
                IDamageable breakable = collider.GetComponent<IDamageable>();
                if (breakable != null) breakable.OnTakeDamage(damage);
            }
        }

        private void Collide()
        {
            // Instancia a VFX de explosao
            hasExploded = true;
            Explode();
            isStuck = false;
            PopBubble();
        }

        // Garantindo que a logica do trigger do bubble base não sera executada
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!friendlyTag.Contains(collision.gameObject.tag) && !hasExploded)
            {
                Collide();
                Debug.Log($"Quem me estourou foi: {collision.gameObject.tag}");
            }

            if (collision.gameObject.CompareTag("StickySurface"))
                StuckBubble(collision.transform);
        }

        // Apenas para debug
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
