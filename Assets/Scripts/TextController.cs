using UnityEngine;

public class TextController : MonoBehaviour
{
    private void Update() {
        if (transform.localScale.x < 1)
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime, transform.localScale.z + Time.deltaTime);
        else
            transform.Translate(0, 2, 0);
        if (transform.localPosition.y > 600) 
            Destroy(gameObject);
    }
}
