using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameController : MonoBehaviour {
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gunController;
    public SpriteController spriteController;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject bossChmonya;
    [SerializeField] private Text restartScoreText;
    [SerializeField] private GameObject UIUX;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text lastScoreText;

    [SerializeField] private GameObject bossFightMusic;
    [SerializeField] private GameObject afterBossFightMusic;
    [SerializeField] private GameObject musicPlayer;

    [SerializeField] private GameObject playersGameObject;
    [SerializeField] private GameObject joystickGameObject;

    private float timer = 1;
    private bool firstEnable = true;
    private bool isFirstBossFight = true;
    private bool onAfterBossfight = false;

    public bool bossFighting = false;
    public GameObject[] players;
    public GameObject[] additionalSpritesForPlayer;
    public GameObject[] livesImage;
    public Text scoreText;
    public int score = 0;
    public int lives = 3;

    private void Start() {
        musicPlayer = GameObject.Find("MusicPlayer - Unna Mattina");

        playersGameObject.SetActive(false);
        joystickGameObject.SetActive(false);
        bossFightMusic.SetActive(false);
        afterBossFightMusic.SetActive(false);
        musicPlayer.SetActive(true);
        menu.SetActive(true);
        highScoreText.text = "–екорд: " + PlayerPrefs.GetInt("HighScore", 0);
        lastScoreText.text = "ѕоследн€€\r\nигра: " + PlayerPrefs.GetInt("LastScore", 0);
        this.GetComponent<CreateObject>().enabled = false;
        this.GetComponent<ChangeScaleOverTime>().enabled = false;
        UIUX.SetActive(false);
        timer = 9999999;
    }
    private void Update() {
        if (timer > 0)
            timer -= Time.deltaTime;
        else {
            timer = 1;
            if (!bossFighting) score++;
            scoreText.text = "—чЄт: " + score;
            if (score > 30) {
                this.GetComponent<CreateObject>().difficult = 13;
                this.GetComponent<CreateObject>().enemyMaxSize = 1.5f;
            }
            if (score > 100) {
                this.GetComponent<CreateObject>().difficult = 17;
                this.GetComponent<CreateObject>().enemyMaxSize = 2f;
                if (firstEnable) {
                    this.GetComponent<ChangeScaleOverTime>().enabled = true;
                    firstEnable = false;
                }
            }
            if (score > 200) 
                this.GetComponent<CreateObject>().difficult = 20;
            if (score > 250 && isFirstBossFight) {
                bossFightMusic.SetActive(true);
                if (musicPlayer != null)
                    musicPlayer.SetActive(false);

                Instantiate(bossChmonya, new Vector3(14, 0, 0), transform.rotation);

                bossFighting = true;
                isFirstBossFight = false;
                this.GetComponent<CreateObject>().bossFighting = true;

                this.GetComponent<ChangeScaleOverTime>().enabled = false;
                GameObject.Find("player").GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                GameObject.Find("player (1)").GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                GameObject.Find("player (2)").GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);

            }
            if (bossFighting) {
                GameObject chmonya = GameObject.Find("chmonya(Clone)");
                if (chmonya == null)
                    bossFighting = false;

                GameObject.Find("Inventory").GetComponent<Animator>().SetBool("RenderOn", false);
                GameObject.Find("Bullets").GetComponent<Animator>().SetBool("RenderOn", false);
                GameObject.Find("Inventory").GetComponent<UseSlot>().SlotClicked(3);
            }
            if (!bossFighting && !isFirstBossFight && !onAfterBossfight) {
                afterBossFightMusic.SetActive(true);
                bossFightMusic.SetActive(false);

                onAfterBossfight = true;
                this.GetComponent<ChangeScaleOverTime>().enabled = true;
                this.GetComponent<CreateObject>().bossFighting = false;

                if (gunController.GetComponent<GunController>().havePistol || gunController.GetComponent<GunController>().haveStaff) {
                    GameObject.Find("Inventory").GetComponent<Animator>().SetBool("RenderOn", true);
                    GameObject.Find("Inventory").GetComponent<UseSlot>().SlotClicked(0);
                }
                gunController.GetComponent<GunController>().CheckCountOfBullets();
            }
        }
    }
    public void StartGame() {
        playersGameObject.SetActive(true);
        joystickGameObject.SetActive(true);
        menu.SetActive(false);
        UIUX.SetActive(true);
        this.GetComponent<CreateObject>().enabled = true;
        gunController.SetActive(true);
        timer = 1;
    }

    public void RestartScene() {
        if (musicPlayer != null)
            musicPlayer.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }

    public void StopGame() {
        restartMenu.SetActive(true);
        playersGameObject.SetActive(false);
        joystickGameObject.SetActive(false);
        this.GetComponent<CreateObject>().enabled = false;
        UIUX.SetActive(false);
        gunController.SetActive(false);
        restartScoreText.text = "—чЄт: " + score;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
        foreach(GameObject friend in friends)
            Destroy(friend);
        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();
    }

    public IEnumerator Fading() {
        for (int i = 0; i < 3; i++) {
            for (int j = 2 - lives; j < 3; j++)
                livesImage[j].SetActive(false);
            for (int j = 0; j < players.Length; j++)
                players[j].GetComponent<SpriteRenderer>().enabled = false;
            if (spriteController.isUsingNewSprites) {
                for (int j = 0; j < additionalSpritesForPlayer.Length; j++)
                    additionalSpritesForPlayer[j].GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject pistol in gunController.GetComponent<GunController>().pistols) {
                if (pistol.activeSelf)
                    pistol.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject staff in gunController.GetComponent<GunController>().staffs) {
                if (staff.activeSelf)
                    staff.GetComponent<SpriteRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(0.3f);
            for (int j = 2 - lives; j < 3; j++)
                livesImage[j].SetActive(true);
            for (int j = 0; j < players.Length; j++)
                players[j].GetComponent<SpriteRenderer>().enabled = true;
            if (spriteController.isUsingNewSprites) {
                for (int j = 0; j < additionalSpritesForPlayer.Length; j++)
                    additionalSpritesForPlayer[j].GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (GameObject pistol in gunController.GetComponent<GunController>().pistols) {
                if (pistol.activeSelf)
                    pistol.GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (GameObject staff in gunController.GetComponent<GunController>().staffs) {
                if (staff.activeSelf)
                    staff.GetComponent<SpriteRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(0.3f);
        }

    }

}
