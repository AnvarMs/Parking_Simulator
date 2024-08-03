using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPathDrower : MonoBehaviour
{
    [SerializeField]
    Color color;
    [SerializeField]
    Transform[] transforms;

   

    private void OnDrawGizmos()
    {
        
          transforms= transform.GetComponentsInChildren<Transform>();
        
        Gizmos.color = color;
        for (int i=0;i<transforms.Length; i++) {
            if (transform.gameObject != transforms[i].gameObject){
                
                Gizmos.DrawWireSphere(transforms[i].position, 1);
            }
         }
    }
}
