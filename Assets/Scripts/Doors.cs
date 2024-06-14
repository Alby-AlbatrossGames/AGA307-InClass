using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject doorL;
    public GameObject doorR;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            doorL.SetActive(false);
            doorR.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorL.SetActive(true);
            doorR.SetActive(true);
        }
        
    }
}
