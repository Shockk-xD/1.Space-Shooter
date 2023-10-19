using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private void Update() {
        transform.Translate(3 * Time.deltaTime, 7 * Time.deltaTime, 0);
        transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime / 2, transform.localScale.y - Time.deltaTime / 2, transform.localScale.z - Time.deltaTime / 2);
        if (transform.localScale.x < 0)
            Destroy(gameObject);
    }
}
