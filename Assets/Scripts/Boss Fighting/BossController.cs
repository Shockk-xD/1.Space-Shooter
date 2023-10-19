using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject miniChmonyaPrefab;
    [SerializeField] private GameObject secondChmonya;
    private float timer;
    private bool isAttacked = false;

    private void Update() {
        timer += Time.deltaTime;
        if (transform.position.x > 8.41f) {
            transform.Translate(-2 * Time.deltaTime, 0, 0);
            timer = 0;
        }
        if (timer > 10 && timer < 20 && !isAttacked) {
            StartCoroutine(FirstAttack());
            isAttacked = true;
        } else if (timer > 20 && timer < 37) {
            transform.Translate(0, ((GameObject.Find("player").transform.position.y > transform.position.y) ? 3 : -3) * Time.deltaTime, 0);
            if (isAttacked) {
                StartCoroutine(SecondAttack());
                isAttacked = false;
            }
        } else if (timer > 37 && timer < 42) {
            if (transform.position.y < 2.6f)
                transform.Translate(0, 2 * Time.deltaTime, 0);
        } else if (timer > 42 && timer < 46) {
            if (transform.position.x > -14)
                transform.Translate(-10 * Time.deltaTime, 0, 0);
            if (transform.position.x < -14)
                transform.position = new Vector3(-14, -2.6f, 0);
        } else if (timer > 46 && timer < 48) {
            if (transform.position.x < 8.4f)
                transform.Translate(10 * Time.deltaTime, 0, 0);
        } else if (timer > 48 && timer < 50) {
            if (transform.position.y < 0)
                transform.Translate(0, 2 * Time.deltaTime, 0);
        } else if (timer > 52 && timer < 62 && !isAttacked) {
            StartCoroutine(FirstAttack());
            isAttacked = true;
        } else if (timer > 62 && timer < 75) {
            transform.Translate(0, ((GameObject.Find("player").transform.position.y > transform.position.y) ? 3 : -3) * Time.deltaTime, 0);
            if (isAttacked) {
                StartCoroutine(SecondAttack());
                isAttacked = false;
            }
        } else if (timer > 75 && timer < 77) {
            if (transform.position.y > 0.3f)
                transform.Translate(0, -2 * Time.deltaTime, 0);
            else if (transform.position.y < -0.3f)
                transform.Translate(0, 2 * Time.deltaTime, 0);
            GameObject.Find("brawl smile").GetComponent<SpriteRenderer>().enabled = true;
        } else if (timer > 77 && timer < 80) {
            transform.Translate(-10 * Time.deltaTime, 0, 0);
            if (transform.position.x < -14)
                Destroy(gameObject);    
        }
    }

    private IEnumerator FirstAttack() {
        for (int i = 0; i < 3; i++) {
            Instantiate(miniChmonyaPrefab, new Vector3(6, 2.5f, 0), transform.rotation);
            yield return new WaitForSeconds(0.5f);
            Instantiate(miniChmonyaPrefab, new Vector3(6, 0, 0), transform.rotation);
            yield return new WaitForSeconds(0.5f);
            Instantiate(miniChmonyaPrefab, new Vector3(6, -2.5f, 0), transform.rotation);
            yield return new WaitForSeconds(1.25f);
        }
    }
    
    private IEnumerator SecondAttack() {
        for (int i = 0; i < 30; i++) {
            Instantiate(secondChmonya, this.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameController gm = GameObject.Find("GameController").GetComponent<GameController>();
            gm.lives--;
            gm.StartCoroutine(gm.Fading());
            gm.livesImage[2 - gm.lives].GetComponent<Image>().enabled = false;
            if (gm.lives == 0)
                gm.StopGame();
        }
    }
}
