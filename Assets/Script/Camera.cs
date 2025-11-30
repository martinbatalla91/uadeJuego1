using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject John;

    // Update is called once per frame
    void Update()
    {
        if(John != null)
        {
            Vector3 position = transform.position;
            position.x = John.transform.position.x;
            transform.position = position;
        }
        
    }
}
