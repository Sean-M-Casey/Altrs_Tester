using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteSwitch : MonoBehaviour
{
    public CameraOrientation camOrientation;

    //Sprites for each angle
    //Sprites for Top/Bottom
    public Sprite spriteTop;
    public Sprite spriteBottom;

    //Sprites for Cardinal directions
    public Sprite spriteFront;
    public Sprite spriteLeft;
    public Sprite spriteBack;
    public Sprite spriteRight;

    //Sprites for Subcardinal
    public Sprite spriteFLeft;
    public Sprite spriteFRight;
    public Sprite spriteBLeft;
    public Sprite spriteBRight;

    private Transform objTrans;
    private SpriteRenderer spriteRenderer;
    private Transform spriteTrans;

    private Vector3 objRotAngles = Vector3.zero;
    private Vector3 objPos = Vector3.zero;

    public Vector2 ObjRotation { get { return objRotAngles; } }
    public Vector3 ObjPosition { get { return objPos; } }

    private Orientation objDir = Orientation.North;
    private Orientation offsetDir = Orientation.North;


    public void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        objTrans = transform;
        objRotAngles = objTrans.rotation.eulerAngles;
        objPos = objTrans.position;
        HelperMethods.ClampAngle(ref objRotAngles);
        spriteTrans = spriteRenderer.transform;
    }

    private void Update()
    {
        
        spriteRenderer.flipX = false;

        objRotAngles = objTrans.rotation.eulerAngles;
        objPos = objTrans.position;

        HelperMethods.ClampAngle(ref objRotAngles);

        HelperMethods.FindOrientation(ObjRotation, ref objDir);

        float rawRatio = HelperMethods.FindDistToHeightRatio(ObjPosition, camOrientation.CameraPosition);
        float trueRatio = Mathf.Abs(rawRatio);

        int offsetY = objDir - camOrientation.CamOrientation;

        if (offsetY < 0) offsetY += 8;

        offsetDir = (Orientation)offsetY;


        Vector3 billboardFacing; 
        

        Debug.Log($"SpriteObj Orientation is {objDir}.");
        Debug.Log($"Offset Orientation is {offsetDir}.");

        if (trueRatio < 0.5)
        {
            billboardFacing = new Vector3(camOrientation.transform.position.x, objTrans.position.y, camOrientation.transform.position.z) - objTrans.position;
            spriteTrans.rotation = Quaternion.LookRotation(-(billboardFacing + objTrans.up), objTrans.forward);
            Debug.Log($"Billboard Vector is: {billboardFacing}");

            spriteRenderer.sprite = spriteTop;
        }
        else 
        {
            billboardFacing = new Vector3(camOrientation.transform.position.x, objTrans.position.y, camOrientation.transform.position.z) - objTrans.position;
            spriteTrans.rotation = Quaternion.LookRotation(-billboardFacing, Vector3.up);

            //Makes adjustments to offsetDir based on which sprites are assigned.
            switch (offsetDir)
            {
                case Orientation.North:
                    if (spriteFront == null)
                    {
                        offsetDir = Orientation.South;
                        spriteRenderer.flipX = true;
                    }
                    break;
                case Orientation.South:
                    if (spriteBack == null)
                    {
                        offsetDir = Orientation.North;
                        spriteRenderer.flipX = true;
                    }
                    break;

                case Orientation.NorthEast:
                    if (spriteFRight == null)
                    {
                        if (spriteFLeft == null)
                        {
                            offsetDir = Orientation.East;
                        }
                        else
                        {
                            offsetDir = Orientation.NorthWest;
                            spriteRenderer.flipX = true;
                        }
                    }
                    break;
                case Orientation.NorthWest:
                    if (spriteFLeft == null)
                    {
                        if (spriteFRight == null)
                        {
                            offsetDir = Orientation.West;
                        }
                        else
                        {
                            offsetDir = Orientation.NorthEast;
                            spriteRenderer.flipX = true;
                        }
                    }
                    break;

                case Orientation.East:
                    if (spriteRight == null)
                    {
                        offsetDir = Orientation.West;
                        spriteRenderer.flipX = true;
                    }
                    break;
                case Orientation.West:
                    if (spriteLeft == null)
                    {
                        offsetDir = Orientation.East;
                        spriteRenderer.flipX = true;
                    }
                    break;

                case Orientation.SouthEast:
                    if (spriteBRight == null)
                    {
                        if (spriteBLeft == null)
                        {
                            offsetDir = Orientation.East;
                        }
                        else
                        {
                            offsetDir = Orientation.SouthWest;
                            spriteRenderer.flipX = true;
                        }
                    }
                    break;
                case Orientation.SouthWest:
                    if (spriteBLeft == null)
                    {
                        if (spriteBRight == null)
                        {
                            offsetDir = Orientation.West;
                        }
                        else
                        {
                            offsetDir = Orientation.SouthEast;
                            spriteRenderer.flipX = true;
                        }
                    }
                    break;
            }

            switch (offsetDir)
            {
                case Orientation.North:
                    spriteRenderer.sprite = spriteFront;

                    break;
                case Orientation.South:
                    spriteRenderer.sprite = spriteBack;

                    break;
                case Orientation.East:
                    spriteRenderer.sprite = spriteRight;

                    break;
                case Orientation.West:
                    spriteRenderer.sprite = spriteLeft;

                    break;
                case Orientation.NorthEast:
                    spriteRenderer.sprite = spriteFRight;

                    break;
                case Orientation.NorthWest:
                    spriteRenderer.sprite = spriteFLeft;

                    break;
                case Orientation.SouthEast:
                    spriteRenderer.sprite = spriteBRight;

                    break;
                case Orientation.SouthWest:
                    spriteRenderer.sprite = spriteBLeft;

                    break;
            }
        }

        
    }

    
}
