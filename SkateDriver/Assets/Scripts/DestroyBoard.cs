using UnityEngine;

public class DestroyBoard : MonoBehaviour
{
    void Start()
    {
        Invoke("DestoryBoard", 1);
    }
    private void DestoryBoard()
    {
        Destroy(gameObject);
    }
}
