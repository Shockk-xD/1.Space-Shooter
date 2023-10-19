using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private GameObject slotItemPrefab;

    private void Start() {
        inventory = GameObject.Find("player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (!inventory.isFull[i]) {
                    if (slotItemPrefab.name == "Pistol Slot" && !GameObject.Find("GunController").GetComponent<GunController>().havePistol) {
                        GameObject.Find("GunController").GetComponent<GunController>().havePistol = true;
                        inventory.isFull[i] = true;
                        Instantiate(slotItemPrefab, inventory.slots[i].transform);
                        Destroy(gameObject);
                    } else if (slotItemPrefab.name == "Staff Slot" && !GameObject.Find("GunController").GetComponent<GunController>().haveStaff) {
                        GameObject.Find("GunController").GetComponent<GunController>().haveStaff = true;
                        inventory.isFull[i] = true;
                        Instantiate(slotItemPrefab, inventory.slots[i].transform);
                        Destroy(gameObject);
                    }
                    if (inventory.firstWeapon) {
                        GameObject.Find("Inventory").GetComponent<Animator>().SetBool("RenderOn", true);
                        inventory.firstWeapon = false;
                    }
                    GameObject.Find("Inventory").GetComponent<UseSlot>().SlotClicked(GameObject.Find("Inventory").GetComponent<UseSlot>().lastClickedSlotId);
                    break;
                }
            }
        }
    }
}
