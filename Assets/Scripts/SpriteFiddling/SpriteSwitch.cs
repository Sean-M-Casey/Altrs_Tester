using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteSwitch : MonoBehaviour
{
    public CameraOrientation camOrientation;
    public Sprite spriteTop;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public Sprite spriteFront;

    private Orientation objDirection = Orientation.North;

    public void Start()
    {
        
    }
}
