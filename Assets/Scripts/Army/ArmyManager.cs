using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;

/*
Pr�parer un terrain o� toutes les terrasses sont accessibles
*/

public abstract class ArmyManager : MonoBehaviour
{
    [SerializeField] string m_ArmyTag;
    [SerializeField] Color m_ArmyColor;
    protected List<IArmyElement> m_ArmyElements = new List<IArmyElement>();

    [SerializeField] TMP_Text m_NDronesText;
    [SerializeField] TMP_Text m_NTurretsText;
    [SerializeField] TMP_Text m_HealthText;

    [SerializeField] UnityEvent m_OnArmyIsDead;

    protected List<T> GetAllEnemiesOfType<T>(bool sortRandom) where T : ArmyElement
    {
        var enemies = GameObject.FindObjectsOfType<T>().Where(element => !element.gameObject.CompareTag(m_ArmyTag)).ToList();
        if (sortRandom) enemies.Sort((a, b) => Random.value.CompareTo(.5f));
        return enemies;
    }

    public GameObject GetRandomEnemy<T>(Vector3 centerPos, float minRadius, float maxRadius) where T : ArmyElement
    {
        var enemies = GetAllEnemiesOfType<T>(true).Where(
            item=>  Vector3.Distance(centerPos,item.transform.position)>minRadius
                    && Vector3.Distance(centerPos, item.transform.position) < maxRadius);

        return enemies.FirstOrDefault()?.gameObject;
    }

    public GameObject GetFarestEnemy<T>(Vector3 centerPos, float minRadius, float maxRadius) where T : ArmyElement {
        var enemies = GetAllEnemiesOfType<T>(true);
        if (enemies.Count == 0) {
            Debug.Log("Aucun ennemi trouvé de type " + typeof(T).Name);
        return null; // Retourne null si aucun ennemi n'est trouvé
        }
        var index=0;
        var maxDistance=0f;
        for(int i=0; i<enemies.Count; i++){
            var distance = Vector3.Distance(centerPos, enemies[i].transform.position); 
            if(distance>maxDistance){
                maxDistance=distance;
                index=i;
                Debug.Log("DISTAAANCE" + maxDistance);
            }
        }
        var enemy = enemies[index]; 
        return enemy?.gameObject;
    }

    public GameObject GetNearestEnemy<T>(Vector3 centerPos, float minRadius, float maxRadius) where T : ArmyElement {
        var enemies = GetAllEnemiesOfType<T>(true);
        if (enemies.Count == 0) {
            Debug.Log("Aucun ennemi trouvé de type " + typeof(T).Name);
        return null; // Retourne null si aucun ennemi n'est trouvé
        }
        var index=0;
        var minDistance=100f;
        for(int i=0; i<enemies.Count; i++){
            var distance = Vector3.Distance(centerPos, enemies[i].transform.position); 
            if(distance<minDistance){
                minDistance=distance;
                index=i;
                //Debug.Log("DISTAAANCE" + maxDistance);
            }
        }
        var enemy = enemies[index]; 
        return enemy?.gameObject;
    }

    protected void ComputeStatistics(ref int nDrones,ref int nTurrets,ref int cumulatedHealth)
	{
        nDrones = m_ArmyElements.Count(item => item is Drone);
        nTurrets = m_ArmyElements.Count(item => item is Turret);
        cumulatedHealth = (int)m_ArmyElements.Sum(item => item.Health);
    }

    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {
        yield return null; // on attend une frame que tous les objets aient �t� instanci�s ...

        GameObject[] allArmiesElements = GameObject.FindGameObjectsWithTag(m_ArmyTag);
        foreach (var item in allArmiesElements)
        {
            IArmyElement armyElement = item.GetComponent<IArmyElement>();
            armyElement.ArmyManager = this;
            m_ArmyElements.Add(armyElement);
        }

        RefreshHudDisplay();

        yield break;
    }

    public void RefreshHudDisplay()
	{
        int nDrones=0, nTurrets=0, health=0;
        ComputeStatistics(ref nDrones, ref nTurrets, ref health);

        m_NDronesText.text = nDrones.ToString();
        m_NTurretsText.text = nTurrets.ToString() ;
        m_HealthText.text = health.ToString();
    }

    public virtual void ArmyElementHasBeenKilled(GameObject go)
    {
        m_ArmyElements.Remove(go.GetComponent<IArmyElement>());
        RefreshHudDisplay();

        if (m_ArmyElements.Count == 0 & m_OnArmyIsDead!=null) m_OnArmyIsDead.Invoke();
    }

    // mes ajouts
    public static ArmyManager Instance { get; private set; }

       public void AddArmyElement(GameObject go)
    {
        IArmyElement armyElement = go.GetComponent<IArmyElement>();
        if (armyElement != null && !m_ArmyElements.Contains(armyElement))
        {
            m_ArmyElements.Add(armyElement); 
            RefreshHudDisplay(); 
        }
    }

    
 public List<Drone> drones;
    public List<T> GetArmyElements<T>() where T : IArmyElement
{
     return m_ArmyElements.OfType<T>().ToList(); 

}   
}


//QUARANTINE
/*
 *     Dictionary<GameObject, GameObject> m_DicoWhoTargetsWhom = new Dictionary<GameObject, GameObject>();

        if (m_DicoWhoTargetsWhom.ContainsKey(go))
            m_DicoWhoTargetsWhom.Remove(go);

public GameObject GetRandomNonTargetedEnemy<T>() where T : ArmyElement
{
    var enemies = GetAllEnemiesOfType<T>(true);
    return enemies.Where(item => 
            !m_DicoWhoTargetsWhom.ContainsValue(item.gameObject)
            ).FirstOrDefault()?.gameObject;
}

public GameObject LockArmyElementOnRandomNonTargetedEnemy<T>(GameObject locker) where T : ArmyElement
{
    GameObject rndGO = GetRandomNonTargetedEnemy<T>();
    if (rndGO)
    {
        m_DicoWhoTargetsWhom[locker] = rndGO;
    }
    return rndGO;
}

public void UnlockArmyElement(GameObject locker)
{
    if (m_DicoWhoTargetsWhom.ContainsKey(locker))
        m_DicoWhoTargetsWhom.Remove(locker);
}
*/