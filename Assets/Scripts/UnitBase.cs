using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public List<Transform> visiblePoints;
    
    public static bool IsVisibleUnit<T>(T unit, Transform from, float angle, float distance, LayerMask mask) where T : UnitBase
    {
        bool result = false;
        print("-1-1");
        if (unit != null)
        {
            
            print("00");
            foreach (Transform visiblePoint in unit.visiblePoints)
            {
                print(11);
                if (IsVisibleObject(from, visiblePoint.position, unit.gameObject, angle, distance, mask))
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    public static bool IsVisibleObject(Transform from, Vector3 point, GameObject target, float angle, float distance, LayerMask mask)
    {
        bool result = false;
        print(22);
        if (IsAvailablePoint(from, point, angle, distance))
        {
            Vector3 direction = (point - from.position);
            Ray ray = new Ray(from.position, direction);
            RaycastHit hit;
            Debug.DrawRay(from.position, direction, Color.green);
            
            if (Physics.Raycast(ray, out hit, distance, mask.value))
            {
                if (hit.collider.gameObject == target)
                {
                    result = true;
                }
            }
        }
        return result;
    }
    public static bool IsAvailablePoint(Transform from, Vector3 point, float angle, float distance)
    {
        bool result = false;
        print(33);
        if (from != null && Vector3.Distance(from.position, point) <= distance)
        {
            Vector3 direction = (point - from.position);
            float dot = Vector3.Dot(from.forward, direction.normalized);
            if (dot < 1)
            {
                float angleRadians = Mathf.Acos(dot);
                float angleDeg = angleRadians * Mathf.Rad2Deg;
                result = (angleDeg <= angle);
            }
            else
            {
                result = true;
            }
        }
        return result;
    }
}