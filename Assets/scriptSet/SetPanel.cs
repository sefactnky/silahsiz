using UnityEngine;

public class SetPanel : MonoBehaviour
{
    public GameObject Set��i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Set��i.SetActive(false);
        
    }

   public void SetOnClick()
    {
        Set��i.SetActive(true );
    }
    public void SetOutClick()
    {
        Set��i.SetActive(false);
    }
}
