using System.Collections;
using System.Collections.Generic;
using EzySlice;
using NaughtyAttributes;
using UnityEngine;

public class SliceTest : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSlice;
    private List<GameObject> _slices = new List<GameObject>();

    private void Start()
    {
        _slices = new List<GameObject>();
        _slices.Add(_objectToSlice);
    }

    [Button]
    public void Slice()
    {
        var slices = SliceObject(_objectToSlice, gameObject.transform.position, gameObject.transform.up);

        slices[0].transform.position += gameObject.transform.up * 0.1f;
        slices[1].transform.position -= gameObject.transform.up * 0.1f;
    }

    private Vector3? _mouseDownPos;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && _mouseDownPos.HasValue)
        {
            print("CUT");
            var mouseDownPos = Camera.main.ScreenToWorldPoint(_mouseDownPos.Value);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseDownPos.z = 0f;
            mousePos.z = 0f;

            var midPoint = (mousePos + mouseDownPos) / 2;
            var betweenVec = mousePos - mouseDownPos;
            var normal = Quaternion.AngleAxis(90f, Vector3.forward) * betweenVec;

            print(_mouseDownPos +  " "  + mousePos);
            Debug.DrawLine(mouseDownPos, mousePos, Color.red, 2f);


            for (var i = _slices.Count - 1; i >= 0; i--)
            {
                GameObject slice = _slices[i];
                var slices = SliceObject(slice, midPoint, normal);

                if (slices?.Length > 0)
                {
                    _slices.AddRange(slices);
                    slices[0].transform.position += normal * 0.01f;
                    slices[1].transform.position -= normal * 0.01f;
                    DestroySlice(slice);
                }

                _mouseDownPos = null;
            }
        }
    }

    private void DestroySlice(GameObject slice)
    {
        print("DESTROY " + slice.name);
        _slices.Remove(slice);
        Destroy(slice);
    }

    /**
     * This function will slice the provided object by the plane defined in this
     * GameObject. We use the GameObject this script is attached to define the position
     * and direction of our cutting Plane. Results are then returned to the user.
     */
    public GameObject[] SliceObject(GameObject obj, Vector3 slicerPos, Vector3 slicerNormal, Material crossSectionMaterial = null)
    {
        GameObject[] slices = obj.SliceInstantiate(new EzySlice.Plane(slicerPos, slicerNormal),
            new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
            crossSectionMaterial);

        return slices;
    }
}
