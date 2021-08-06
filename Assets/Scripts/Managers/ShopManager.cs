using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Button shipsButton;
    [SerializeField] Button gunsButton;

    [SerializeField] GameObject ships;
    [SerializeField] GameObject guns;

    [SerializeField] Transform raycast;

    [SerializeField] Text moneyText;

    void Awake()
    {
        shipsButton.onClick.AddListener(OnClickShipsButton);
        gunsButton.onClick.AddListener(OnClickGunsButton);
    }

    void Update()
    {
        moneyText.text = "MONEY:" + SerializeManager.Instance.GetFloat(FloatType.Money).ToString();
        
        CheckSelectables();
    }

    void OnClickShipsButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        ships.SetActive(true);
        guns.SetActive(false);
    }

    void OnClickGunsButton()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        ships.SetActive(false);
        guns.SetActive(true);
    }

    void CheckSelectables()
    {
        Ray ray = new Ray(raycast.position, raycast.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
                Selectable.DeselectAll();
                selectable.Select();
            }
        }
    }
}
