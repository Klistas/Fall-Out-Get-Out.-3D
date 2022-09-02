using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMouse : MonoBehaviour
{
    public static float rotCamYAxisSpeed = 1.5f;
    public Transform Head;

    private float limitMinX = -80;
    private float limitMaxX = 70;

    private float eulerAngleX;
    private float eulerAngleY;


    public void UpdateRotate(float mouseY, float mouseX)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamYAxisSpeed;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(0, eulerAngleY, 0);
        Head.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);


    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
