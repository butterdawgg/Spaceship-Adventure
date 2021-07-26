using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    void Awake()
    {
        Player player = Instantiate(ItemManager.Instance.GetActiveItemPrefab(IntType.ActiveShipId)).GetComponent<Player>();

        foreach(GameObject h in player.hardpoints)
        {
            if (h.transform.childCount > 0)
                Destroy(h.transform.GetChild(0));

            Instantiate(ItemManager.Instance.GetActiveItemPrefab(IntType.ActiveGunId), h.transform);
        }
    }
}
