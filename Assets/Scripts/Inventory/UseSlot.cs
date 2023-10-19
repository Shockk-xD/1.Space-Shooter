using UnityEngine;
using UnityEngine.UI;

public class UseSlot : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    public Inventory _inventory;
    public int lastClickedSlotId = 0;

    private void Start() {
        lastClickedSlotId = 0;
        SlotClicked(lastClickedSlotId);
    }

    public void SlotClicked(int i) {
        foreach (GameObject slot in _inventory.slots)
            slot.GetComponent<Image>().color = new Color(1, 1, 1);
        _inventory.slots[i].GetComponent<Image>().color = new Color(0, 170f / 255f, 1);
        lastClickedSlotId = i;

        foreach (GameObject pistols in gunController.pistols)
            pistols.SetActive(false);
        foreach (GameObject staff in gunController.staffs)
            staff.SetActive(false);
        if (_inventory.slots[i].transform.Find("Pistol Slot(Clone)") != null) {
            foreach (GameObject pistol in gunController.pistols) {
                pistol.SetActive(true);
                pistol.GetComponent<SpriteRenderer>().enabled = true;
            }
        } else if (_inventory.slots[i].transform.Find("Staff Slot(Clone)") != null) {
            foreach (GameObject staff in gunController.staffs) {
                staff.SetActive(true);
                staff.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
