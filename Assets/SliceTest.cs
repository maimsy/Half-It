using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class SliceTest : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSlice;

    private void Start()
    {
        var objects = Slice(Vector3.zero, Vector3.forward);
        print(objects.Length);
    }

    private GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        return _objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection);
    }
}
