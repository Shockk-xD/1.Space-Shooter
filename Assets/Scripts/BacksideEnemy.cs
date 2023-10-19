using UnityEngine;

public class BacksideEnemy : MonoBehaviour
{
    private float timer = 3;
    private bool isFlying = false;

    private void Update() {
        if (transform.position.x < -10.32f)
            transform.Translate(Time.deltaTime, 0, 0);
        else {
            if (timer > 0)
                timer -= Time.deltaTime;
            else {
                transform.Translate(10 * Time.deltaTime, 0, 0);
                if (transform.position.x > 12.7f)
                    Destroy(gameObject);
                if (!isFlying)
                    isFlying = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") && isFlying) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
