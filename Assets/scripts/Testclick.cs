using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testclick : MonoBehaviour
{
    private bool isalive=false;
    private void OnMouseDown()
    {
        // �����ִ�е���Ϊ
        Debug.Log("���屻����ˣ�");
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
