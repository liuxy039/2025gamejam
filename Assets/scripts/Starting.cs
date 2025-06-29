using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject box;
    public GameObject player;
    void Start()
    {
        player.SetActive(false);
        box.SetActive(true);
    }
    private void OnMouseDown()
    {
        Debug.Log("1");
        player.gameObject.SetActive(true);
        box.gameObject.SetActive(false);
    }
}
