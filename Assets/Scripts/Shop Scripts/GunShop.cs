﻿using UnityEngine;
using TMPro;
using System.Collections;
public class GunShop : MonoBehaviour
{
    //Reference variables for guns
    public GameObject purchaseText;
    public GameObject gun;
    public GunScript gunScript;
    public LivingEntity player;

    //Bool variables to determine whether a player has purchased the item 
    bool purchase = false;
    bool purchased = false;
   
    //Bool variables for which gun collider the player is in
    bool smallGun = false;
    bool mediumGun = false;
    bool heavyGun = false;
    bool playerInRange = false;


    public int gunValue;
    public GunController gunController;

    void Start()//Gets reference to scripts on objects
    {
        gunController = FindObjectOfType<GunController>();
        gunScript = FindObjectOfType<GunScript>();
        GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
    }

    private void OnTriggerEnter(Collider c)//Determines which collider the player is in and acts accordingly
    {
        if (c.CompareTag("Player") && purchased == false)
        {
            purchaseText.SetActive(true);
            playerInRange = true;
        }
        if (gameObject.gameObject.CompareTag("SmallGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase small gun - " + gunValue + "\n " + "    Press E to buy"; 
            smallGun = true;
        }
        if (gameObject.gameObject.CompareTag("MediumGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Medium gun -" + " " + gunValue + "\n " + "    Press E to buy";
            mediumGun = true;
        }
        if (gameObject.gameObject.CompareTag("HeavyGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Heavy gun - " + " " + gunValue + "\n " + "    Press E to buy";
            heavyGun = true;
        
        }

    }
    private void OnTriggerExit(Collider c)//Sets booleans to false on exit of colliders
    {
        if (c.CompareTag("Player"))
        {
            purchaseText.SetActive(false);
            playerInRange = false;
            smallGun = false;
            mediumGun = false;
            heavyGun = false;
        }
    }

    void Update()
    {
        //Checks if the player has enough score to purchase and if he is within the collider
        if (Input.GetKeyDown(KeyCode.E) && LivingEntity.score >= gunValue && playerInRange == true)
        {
            purchaseWeapon();
        }
    }

    void purchaseWeapon()// Determines which weapon is set to active according to the corresponding bool
    {
        gunController.gunToBeEquipped = false;
        if (smallGun == true)
        {
           
            gunController.smallGunPurchased = true;
        }
        if (mediumGun == true)
        {
            {
                gunController.mediumGunPurchased = true;   
            }
        }
        if (heavyGun == true)
        { 
                gunController.heavyGunPurchased = true;
        }

        //Sets the gun to purchased so it cannot be purchased again, destroys the game object and takes away the score
        purchased = true;
        purchaseText.SetActive(false);
        GameObject.Destroy(gun);
        LivingEntity.score -= gunValue;
           
    }
    
}
