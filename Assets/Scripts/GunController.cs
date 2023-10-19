using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public GameObject[] pistols;
    public GameObject[] staffs;
    public int pistolBullets = 0;
    public int staffBullets = 0;
    public bool havePistol = false;
    public bool haveStaff = false;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Text pistolBulletsText;
    [SerializeField] private Text staffBulletsText;
    [SerializeField] private GameObject pistolBulletPrefab;
    [SerializeField] private GameObject staffBulletPrefab;
    public float reloadTimer = 0;

    private void Update() {
        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;
        else {
            if (Input.touchCount > 0) {

                RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                foreach (Touch touch in Input.touches) {
                    Vector2 touchPosition = touch.position;
                    if (touch.phase == TouchPhase.Began && touchPosition.x < canvasRect.rect.width / 2 && touchPosition.y > 180) {
                        if (pistols[0].activeSelf && pistolBullets > 0) {
                            GameObject bullet1 = Instantiate(pistolBulletPrefab, transform.position, Quaternion.identity);
                            bullet1.transform.parent = this.gameObject.transform;
                            bullet1.transform.position = new Vector3(pistols[0].transform.position.x + 1, pistols[0].transform.position.y, 0);

                            GameObject bullet2 = Instantiate(pistolBulletPrefab, transform.position, Quaternion.identity);
                            bullet2.transform.parent = this.gameObject.transform;
                            bullet2.transform.position = new Vector3(pistols[1].transform.position.x + 1, pistols[1].transform.position.y, 0);

                            GameObject bullet3 = Instantiate(pistolBulletPrefab, transform.position, Quaternion.identity);
                            bullet3.transform.parent = this.gameObject.transform;
                            bullet3.transform.position = new Vector3(pistols[2].transform.position.x + 1, pistols[2].transform.position.y, 0);

                            pistolBulletsText.text = "" + --pistolBullets;
                            reloadTimer = 1.5f;
                        } else if (pistols[0].activeSelf && pistolBullets == 0) {
                            GameObject.Find("Pistol Bullet Count").GetComponent<Animator>().SetTrigger("Shake");
                        }

                        if (staffs[0].activeSelf && staffBullets > 0) {
                            GameObject bullet1 = Instantiate(staffBulletPrefab, transform.position, Quaternion.identity);
                            bullet1.transform.parent = this.gameObject.transform;
                            bullet1.transform.position = new Vector3(staffs[0].transform.position.x, staffs[0].transform.position.y + 0.8f, 0);

                            GameObject bullet2 = Instantiate(staffBulletPrefab, transform.position, Quaternion.identity);
                            bullet2.transform.parent = this.gameObject.transform;
                            bullet2.transform.position = new Vector3(staffs[1].transform.position.x, staffs[1].transform.position.y + 0.8f, 0);

                            GameObject bullet3 = Instantiate(staffBulletPrefab, transform.position, Quaternion.identity);
                            bullet3.transform.parent = this.gameObject.transform;
                            bullet3.transform.position = new Vector3(staffs[2].transform.position.x, staffs[2].transform.position.y + 0.8f, 0);

                            staffBulletsText.text = "" + --staffBullets;
                            reloadTimer = 1.5f;
                        } else if (staffs[0].activeSelf && staffBullets == 0) {
                            GameObject.Find("Staff Bullet Count").GetComponent<Animator>().SetTrigger("Shake");
                        }

                        CheckCountOfBullets();
                    }
                }
            }
        }
    }

    public void AddBullets(string weaponName, int count) {
        if (weaponName == "pistol") {
            pistolBullets += count;
            pistolBulletsText.text = "" + pistolBullets;
        } else if (weaponName == "staff") {
            staffBullets += count;
            staffBulletsText.text = "" + staffBullets;
        }
        CheckCountOfBullets();
    }

    public void CheckCountOfBullets() {
        if (!GameObject.Find("Bullets").GetComponent<Animator>().GetBool("RenderOn") && (pistolBullets != 0 || staffBullets != 0))
            GameObject.Find("Bullets").GetComponent<Animator>().SetBool("RenderOn", true);
        else if (GameObject.Find("Bullets").GetComponent<Animator>().GetBool("RenderOn") && pistolBullets == 0 && staffBullets == 0)
            GameObject.Find("Bullets").GetComponent<Animator>().SetBool("RenderOn", false);
    }
}
