using UnityEngine;
using System.Collections;

public class GunsInventory : MonoBehaviour
{


    public GameObject[] gunsList = new GameObject[10];
    private bool[] guns = new bool[] { false, true, false, false, false, false, false, false, false, false };
    private KeyCode[] keys = new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    private int maxGuns = 1;

    public void addGun(int number)
    {
        guns[number] = true;
        maxGuns++;
    }




    void Update()
    {
        //gunsList[2].SetActive(false);
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && guns[i])
            {
                hideGuns();
                gunsList[i].SetActive(true);
            }
        }

    }


    private void hideGuns()
    {
        for (int i = 1; i < maxGuns + 1; i++)
        {
            gunsList[i].SetActive(false);
        }
    }


}