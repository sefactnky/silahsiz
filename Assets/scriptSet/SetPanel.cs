using UnityEngine;

public class SetPanel : MonoBehaviour
{
    public GameObject SetÝçi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetÝçi.SetActive(false);
        
    }

   public void SetOnClick()
    {
        SetÝçi.SetActive(true );
    }
    public void SetOutClick()
    {
        SetÝçi.SetActive(false);
    }
}
