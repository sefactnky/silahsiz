using UnityEngine;

public class DilPopup : MonoBehaviour
{
    public GameObject DilPopups;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DilPopups.SetActive(false);
    }

    public void OnClickPopup()
    {
        gameObject.SetActive(true);
    }

    public void OutClickPopup () 
    { 
        gameObject.SetActive(false); 
    }

}
