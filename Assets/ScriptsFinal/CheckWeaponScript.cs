using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeaponScript : MonoBehaviour
{

   /* // ID de l'arme actuelle
    public int weaponID;

    // Liste de nos armes (Objets se trouvant dans la main du personnage)
    [SerializeField]
    public List<GameObject> weaponList = new List<GameObject>();

    void Update()
    {
        if (transform.childCount > 0)
        {
            weaponID = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else
        {
            weaponID = 0;

            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetActive(false);
            }
        }

        // Copier / Coller le bloc suivant pour chacune de vos armes.
        // WeaponID correspond à l'ID de l'arme dans la Base de données (BDD).
        // "i = X" et "i == X" correspondent à l'ID (ou index) de l'arme dans la LISTE.

        // épée en fer
        if (weaponID == 1 && transform.childCount > 0)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (i == 0)
                {
                    weaponList[i].SetActive(true);
                }

            }
        }

        // katana
        if (weaponID == 4 && transform.childCount > 0)
        {
            for (int i = 1; i < weaponList.Count; i++)
            {
                if (i == 1)
                {
                    weaponList[i].SetActive(true);
                }
            }
        }
    }*/
}

