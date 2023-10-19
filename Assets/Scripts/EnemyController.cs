using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float lives = 3;

    private void Update() {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x < -13)
            Destroy(gameObject);
    }
}
