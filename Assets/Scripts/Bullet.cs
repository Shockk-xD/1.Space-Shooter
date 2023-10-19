using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject playerPistol; 

    private void Update() {
        transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x > 11)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            if (this.gameObject.name == "bullet(Clone)")
                collision.GetComponent<EnemyController>().lives -= 1.5f;
            else if (this.gameObject.name == "fire(Clone)")
                collision.GetComponent<EnemyController>().lives -= Random.Range(2, 4);
            Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            transform.parent = collision.transform;
            transform.position = new Vector3(0.858f, 0.27f, 0);
            if (collision.GetComponent<EnemyController>().lives <= 0) {
                if (Random.Range(0, 4) == 0)
                    Instantiate(GameObject.Find("GameController").GetComponent<CreateObject>().pistolBulletPrefab, collision.transform.position, Quaternion.identity);
                else if (Random.Range(0, 4) == 1)
                    Instantiate(GameObject.Find("GameController").GetComponent<CreateObject>().staffBulletPrefab, collision.transform.position, Quaternion.identity);
                else if (Random.Range(0, 6) == 2)
                    Instantiate(GameObject.Find("GameController").GetComponent<CreateObject>().heartPrefab, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
