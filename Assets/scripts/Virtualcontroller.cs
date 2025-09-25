using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VirtualKeysController : MonoBehaviour
{
    [SerializeField] Button left;
    [SerializeField] Button right;
    [SerializeField] Button space;
    private void Start()
    {
        left.onClick.AddListener(OnLeftClicked);
        right.onClick.AddListener(OnRightClicked);
        space.onClick.AddListener(OnSpaceClicked);
    }
    void OnLeftClicked()
    {

    }
    void OnRightClicked()
    {

    }
    void OnSpaceClicked()
    {

    }
}

