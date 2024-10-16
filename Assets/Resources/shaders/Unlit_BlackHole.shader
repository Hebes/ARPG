Shader "Unlit/BlackHole"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
    _Absorb ("Absorb", Range(0, 0.5)) = 0.005
    _Radius ("Radius", Range(0, 0.5)) = 0.05
    _Scale ("_Scale", Range(0, 1000)) = 0.05
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent"
    }
    LOD 100
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      Fog
      { 
        Mode  Off
      } 
      // m_ProgramMask = 0
      
    } // end phase
    Pass // ind: 2, name: 
    {
      Tags
      { 
        "QUEUE" = "Transparent"
      }
      LOD 100
      ZClip Off
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform sampler2D _BackgroundTexture;
      uniform float _Absorb;
      uniform float _Radius;
      uniform float _Scale;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.vertex;
          float4 tmpvar_2;
          tmpvar_2.w = 1;
          tmpvar_2.xyz = tmpvar_1.xyz;
          float4 tmpvar_3;
          float4 tmpvar_4;
          tmpvar_4.w = 1;
          tmpvar_4.xyz = tmpvar_1.xyz;
          tmpvar_3 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_4));
          float4 o_5;
          float4 tmpvar_6;
          tmpvar_6 = (tmpvar_3 * 0.5);
          o_5.xy = (tmpvar_6.xy + tmpvar_6.w);
          o_5.zw = tmpvar_3.zw;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_2));
          out_v.xlv_TEXCOORD1 = o_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float4 grapPos_2;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 - float2(0.5, 0.5));
          float tmpvar_4;
          tmpvar_4 = sqrt(((tmpvar_3.x * tmpvar_3.x) + (tmpvar_3.y * tmpvar_3.y)));
          if((tmpvar_4<_Absorb))
          {
              tmpvar_1 = float4(0, 0, 0, 1);
          }
          else
          {
              float3 tmpvar_5;
              tmpvar_5.z = 0;
              tmpvar_5.xy = float2(tmpvar_3);
              float3 tmpvar_6;
              tmpvar_6 = (-normalize(tmpvar_5));
              grapPos_2 = in_f.xlv_TEXCOORD1;
              if((tmpvar_4<_Radius))
              {
                  float4 tmpvar_7;
                  tmpvar_7.w = 0;
                  tmpvar_7.xyz = float3((tmpvar_6 * ((((_Radius - tmpvar_4) * _Scale) * 100) * _Absorb)));
                  grapPos_2 = (in_f.xlv_TEXCOORD1 + tmpvar_7);
              }
              tmpvar_1 = tex2D(_BackgroundTexture, grapPos_2);
          }
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
