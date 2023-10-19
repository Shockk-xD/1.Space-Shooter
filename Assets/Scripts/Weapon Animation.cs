using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    private bool isGrowing;

    private void Update() {
        if (transform.localScale.x >= minScale && !isGrowing)
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime / decreaseSpeed, transform.localScale.y - Time.deltaTime / decreaseSpeed, transform.localScale.z - Time.deltaTime / decreaseSpeed);
        else if (transform.localScale.x <= maxScale && isGrowing)
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime / decreaseSpeed, transform.localScale.y + Time.deltaTime / decreaseSpeed, transform.localScale.z + Time.deltaTime / decreaseSpeed);
        if (transform.localScale.x >= maxScale)
            isGrowing = false;
        else if (transform.localScale.x <= minScale)
            isGrowing = true;
    }
}
