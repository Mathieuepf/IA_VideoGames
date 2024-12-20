using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 10f;

    private void OnTriggerStay(Collider other)
    {
        // Cherche le composant Health de l’entité entrant dans le nuage
        
        Health healthComponent = other.GetComponentInChildren<Health>();
        Debug.Log(other.name+" OnTriggerStay "+ name);
        if (healthComponent != null)
        {
            // Applique des dégâts continus
            healthComponent.InflictDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
