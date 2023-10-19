using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private void Update() {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x < -33.8) {
            transform.position = new Vector3(33.9f, transform.position.y, 0);
        }
    }
}
