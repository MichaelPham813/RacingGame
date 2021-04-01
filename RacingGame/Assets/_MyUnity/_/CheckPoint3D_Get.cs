using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3D_Get : MonoBehaviour
{
    public Transform t_First;

    private void Start()
    {
        t_Next = t_First;
    }

    private Transform t_Next;

    public void Set_Next(Transform t_NewNext)
    {
        this.t_Next = t_NewNext;
    }

    //private void Update()
    //{
    //    //Class_Vector cl_Vector = new Class_Vector();

    //    //if (t_Next != null)
    //    //{
    //    //    float f_Distance = Vector3.Distance(transform.position, t_Next.position);
    //    //    float f_Deg = cl_Vector.Get_Rot_TransformToVector(transform.rotation).y;

    //    //    Vector3 v3_DirStart = cl_Vector.Get_Dir_XZ(transform.position, transform.position + cl_Vector.Get_DegToVector_XZ(-f_Deg, f_Distance));
    //    //    Vector3 v3_DirEnd = cl_Vector.Get_Dir_XZ(transform.position, transform.position + cl_Vector.Get_Dir_XZ(transform.position, t_Next.position) * f_Distance);

    //    //    Debug.Log(cl_Vector.Get_DirToDeg_XZ(v3_DirStart, v3_DirEnd));
    //    //}

    //    //if (t_Next != null)
    //    //{
    //    //    Debug.Log(cl_Vector.Get_DirToDeg_XZ_RotateFromTransform(this.transform, t_Next.transform));
    //    //}
    //}

    public float Get_OffsetRotate()
    {
        Class_Vector cl_Vector = new Class_Vector();

        return cl_Vector.Get_DirToDeg_XZ_RotateFromTransform(this.transform, t_Next.transform);
    }

    public bool Get_RonWay(float f_AngleHigher)
    {
        return Get_OffsetRotate() >= f_AngleHigher;
    }

    public bool Get_RightWay(float f_AngleLower)
    {
        return Get_OffsetRotate() <= f_AngleLower;
    }

    //private void OnDrawGizmos()
    //{
    //    Class_Vector cl_Vector = new Class_Vector();

    //    if (t_Next != null)
    //    {
    //        Gizmos.color = Color.black;

    //        //float f_Distance = Vector3.Distance(transform.position, t_Next.position);

    //        float f_Distance = cl_Vector.Get_Distance(this.transform.position, t_Next.transform.position);

    //        Gizmos.DrawLine(
    //            transform.position, 
    //            transform.position + cl_Vector.Get_Dir_XZ(transform.position, t_Next.position) * f_Distance);
    //    }

    //    if (t_Next != null)
    //    {
    //        Gizmos.color = Color.blue;

    //        float f_Distance = Vector3.Distance(transform.position, t_Next.position);

    //        //Gizmos.DrawWireSphere(transform.position, f_Distance);

    //        float f_Deg = cl_Vector.Get_Rot_TransformToVector(transform.rotation).y;

    //        Gizmos.DrawLine(transform.position, transform.position + cl_Vector.Get_DegToVector_XZ(-f_Deg, f_Distance));
    //    }
    //}
}
