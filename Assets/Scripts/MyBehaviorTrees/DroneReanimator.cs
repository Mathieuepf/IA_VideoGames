using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Action = BehaviorDesigner.Runtime.Tasks.Action;


public class DroneReanimator : MonoBehaviour
{
    public Button reanimateButton; 
    public ArmyManagerRed armyManager; 
    public GameObject dronePrefab; 
    private int initialDroneCount; // Nombre initial de drones
    private bool hasBeenUsed = false; 

    private void Start()
    {
        if (reanimateButton != null)
        {
            reanimateButton.interactable = true;
            reanimateButton.onClick.AddListener(ReanimateDrones);
        }
       

    }

    public void ReanimateDrones()
    {
        if (hasBeenUsed) return; 

        List<Drone> drones = armyManager.GetArmyElements<Drone>();
       
        Debug.Log($"Nombre actuel de drones : {drones.Count}");

       
        // Calculer le nombre de drones à générer
        int currentDroneCount = armyManager.GetArmyElements<Drone>().Count;
        int dronesToGenerate = 13 - currentDroneCount;
        Debug.Log($"Drones à générer pour atteindre le nombre initial : {dronesToGenerate}");

        
        for (int i = 0; i < dronesToGenerate; i++)
        {
            GameObject newDrone = Instantiate(dronePrefab, armyManager.transform);
            Drone droneComponent = newDrone.GetComponent<Drone>();

            if (droneComponent != null)
            {
                armyManager.AddArmyElement(newDrone); 
            }
            else
            {
                Debug.LogError("Le prefab du drone ne contient pas de composant Drone !");
                Destroy(newDrone); // Supprimer l'objet mal configuré
            }
        }

      
        if (reanimateButton != null)
        {
            reanimateButton.interactable = false;
        }

        hasBeenUsed = true;

        Debug.Log("Réanimation terminée.");
    }
}
