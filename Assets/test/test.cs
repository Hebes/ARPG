using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class test : BaseMeshEffect
{
    public Image Image;
    [Header("Only Image")] public int subdivision = 2; //有的材质细分2次，有的需3-4次。
    public float perspectiveScale = 1.0f;
    public Transform referencePoint;//z轴参考对象，赋予自身即可
    public bool alwaysRefresh = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void CalcPerspectiveScale(ref Vector3 point)
    {
        if (referencePoint)
        {
            var vertexWS = transform.TransformPoint(point);
            vertexWS.z -= referencePoint.position.z;
            var vertexZ = vertexWS.z * perspectiveScale;
            point *= 1f + vertexZ;
        }
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        // CalcPerspectiveScale(ref p0);
        // CalcPerspectiveScale(ref p1);
        // CalcPerspectiveScale(ref p2);
        // CalcPerspectiveScale(ref p3);
        //
        // vh.AddUIVertexQuad(new UIVertex[]
        // {
        //     new() {position = p0, color = graphic.color, uv0 = uv0},
        //     new() {position = p1, color = graphic.color, uv0 = uv1},
        //     new() {position = p2, color = graphic.color, uv0 = uv2},
        //     new() {position = p3, color = graphic.color, uv0 = uv3}
        // });
    }
}
