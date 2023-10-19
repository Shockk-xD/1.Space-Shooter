using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject newEnemyPrefab;
    [SerializeField] private GameObject backsideEnemy;
    [SerializeField] private GameObject pistolPrefab;
    [SerializeField] private GameObject staffPrefab;
    [SerializeField] private GameObject luckyBlockPrefab;
    [SerializeField] private float timeToSpawn = 3;
    private float timer;
    public bool bossFighting = false;

    public GameObject heartPrefab;
    public GameObject pistolBulletPrefab;
    public GameObject staffBulletPrefab;
    public int difficult = 9;
    public float enemyMaxSize = 1.2f;

    private void Update() {
        if (timer < 0) {
            int randValue = Random.Range(0, difficult);
            if (randValue == 0) {
                GameObject friend = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], transform.position, transform.rotation);
                friend.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
                friend.transform.localScale = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1f), Random.Range(0.5f, 1));
            } else if (randValue == 1 && GameObject.Find("GameController").GetComponent<GameController>().score > 30) {
                GameObject bullet = Instantiate(pistolBulletPrefab, transform.position, transform.rotation);
                bullet.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 2 && GameObject.Find("GameController").GetComponent<GameController>().score > 70) {
                GameObject bullet = Instantiate(staffBulletPrefab, transform.position, transform.rotation);
                bullet.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 3 && GameObject.Find("GameController").GetComponent<GameController>().score > 50 && !GameObject.Find("GunController").GetComponent<GunController>().havePistol) {
                GameObject pistol = Instantiate(pistolPrefab, transform.position, transform.rotation);
                pistol.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 4 && GameObject.Find("GameController").GetComponent<GameController>().score > 150 && !GameObject.Find("GunController").GetComponent<GunController>().haveStaff) {
                GameObject staff = Instantiate(staffPrefab, transform.position, transform.rotation);
                staff.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 5 && GameObject.Find("GameController").GetComponent<GameController>().score > 45 && GameObject.Find("GameController").GetComponent<GameController>().lives < 3) {
                GameObject heart = Instantiate(heartPrefab, transform.position, transform.rotation);
                heart.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 6 && GameObject.Find("GameController").GetComponent<GameController>().score > 15) {
                GameObject block = Instantiate(luckyBlockPrefab, transform.position, transform.rotation);
                block.transform.position = new Vector3(12.71f, Random.Range(-4.25f, 4.25f), 0);
            } else if (randValue == 7 && GameObject.Find("GameController").GetComponent<GameController>().score > 15 && !bossFighting) {
                GameObject enemy = Instantiate(backsideEnemy, transform.position, transform.rotation);
                enemy.transform.position = new Vector3(-13.32185f, Random.Range(-3.3f, 3.17f), 0);
            } else {
                if (!bossFighting) {
                    GameObject enemy;
                    if (this.GetComponent<GameController>().spriteController.isUsingNewSprites) {
                        enemy = Instantiate(newEnemyPrefab, transform.position, transform.rotation);
                        enemy.transform.localScale = new Vector3(Random.Range(2f, enemyMaxSize + 1f), Random.Range(2f, enemyMaxSize + 1f), Random.Range(2f, enemyMaxSize + 1f));
                    } else {
                        enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                        enemy.transform.localScale = new Vector3(Random.Range(1f, enemyMaxSize), Random.Range(1f, enemyMaxSize), Random.Range(1f, enemyMaxSize));
                    }
                        enemy.transform.position = new Vector3(12.71f, Random.Range(-3.3f, 3.17f), 0);
                    if (Random.Range(0, 4) == 0)
                        enemy.AddComponent<EnemyAI>();
                }
            }
            timer = timeToSpawn;
        } else
            timer -= Time.deltaTime;
    }
}
