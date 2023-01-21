using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float distance, height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetFollow();
    }
    private void TargetFollow()
    {
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - distance);
        transform.LookAt(target);
    }
}
