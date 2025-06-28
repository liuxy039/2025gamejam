using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testclick : MonoBehaviour
{
    private bool isalive=false;
    private void OnMouseDown()
    {
        // 点击后执行的行为
        Debug.Log("物体被点击了！");
        isalive=true; 


    }

    private void Update()
    {
        if(isalive)
        {
            Debug.Log(Time.time);
        }
    }
    
}
