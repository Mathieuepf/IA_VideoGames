using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;

public class Reanimate : MonoBehaviour
{
  public DroneReanimator droneReanimator; 

    public void ReanimateRed()
    {
            droneReanimator.ReanimateDrones();
        
    }
}
