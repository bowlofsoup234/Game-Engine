using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponswitching : MonoBehaviour
{
    // Declare public variable for the currently selected weapon
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Call the SelectWeapon() method when the script is first loaded
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // Store the current selected weapon index for comparison
        int previousSelectedWeapon = selectedWeapon;

        // Check if the mouse scroll wheel has been moved up, and change the selected weapon accordingly
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        // Check if the mouse scroll wheel has been moved down, and change the selected weapon accordingly
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Change this condition to only allow scrolling if selectedWeapon is greater than 0
            if (selectedWeapon > 0)
                selectedWeapon--;
            else
                selectedWeapon = transform.childCount - 1;
        }

        // Check if the 1 key has been pressed, and select the first weapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        // Check if the 2 key has been pressed and there are at least 2 weapons, and select the second weapon
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        // Check if the 3 key has been pressed and there are at least 3 weapons, and select the third weapon
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        // If the selected weapon has changed, call the SelectWeapon() method
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    // Method to activate the selected weapon and deactivate all other weapons
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}