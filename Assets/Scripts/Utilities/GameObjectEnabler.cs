using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Currently no used
public class GameObjectEnabler : MonoBehaviour
{
    [SerializeField] Transform[] toEnable;
    [Tooltip("if object to disable also selected for this object, then place it in last")]
    [SerializeField] Transform[] toDisable;
    Button btn;
    [SerializeField] bool isDisableOnStart = false;
    private void Start()
    {
        if (isDisableOnStart)
        {
            SetGameObjectState();
        }
    }
    private void OnEnable()
    {
        if (this.TryGetComponent<Button>(out btn))
        {
            btn.onClick.AddListener(SetGameObjectState);
        }


    }
    public void SetGameObjectState()
    {
        foreach (Transform e in toEnable)
            e.gameObject.SetActive(true);
        foreach (Transform d in toDisable)
            d.gameObject.SetActive(false);
    }
}
