using UnityEngine;

public class FixScale : MonoBehaviour
{
    void Start()
    {

        transform.localScale = new Vector3(
            1f / transform.parent.lossyScale.x, 
            1f / transform.parent.lossyScale.y, 
            1f / transform.parent.lossyScale.z
        );
    }
}