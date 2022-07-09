using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFolllowToStick : MonoBehaviour
{

    [SerializeField] private RectTransform _stickRectTransform;
    [SerializeField] private RectTransform _background;

    void Update()
    {
        FollowToStick();
    }

    private void FollowToStick() 
    {
        _background.position = new Vector3(_stickRectTransform.position.x, _background.position.y, _background.position.z);
    }
}
