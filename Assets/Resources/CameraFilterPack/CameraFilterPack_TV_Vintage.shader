Shader "CameraFilterPack/TV_Vintage"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
  }
  SubShader
  {
    Tags
    { 
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZTest Always
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      uniform float _TimeX;
      uniform float _Distortion;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float2 tmpvar_2;
          tmpvar_2 = in_v.texcoord.xy;
          float2 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          tmpvar_3 = tmpvar_2;
          tmpvar_4 = tmpvar_1;
          out_v.xlv_TEXCOORD0 = tmpvar_3;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_COLOR = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 col_1;
          float2 tmpvar_2;
          tmpvar_2 = (0.5 + ((in_f.xlv_TEXCOORD0 - 0.5) * (0.9 + (0.1 * sin((0.1 * _TimeX))))));
          float2 tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (0.003 * _Distortion);
          tmpvar_3.x = (tmpvar_2.x + tmpvar_4);
          tmpvar_3.y = tmpvar_2.y;
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, tmpvar_3);
          col_1.x = tmpvar_5.x;
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, tmpvar_2);
          col_1.y = tmpvar_6.y;
          float2 tmpvar_7;
          tmpvar_7.x = (tmpvar_2.x - tmpvar_4);
          tmpvar_7.y = tmpvar_2.y;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, tmpvar_7);
          col_1.z = tmpvar_8.z;
          col_1 = (clamp(((col_1 * 0.5) + ((0.6 * col_1) * col_1)), float3(0, 0, 0), float3(1, 1, 1)) * (0.5 + ((((8 * tmpvar_2.x) * tmpvar_2.y) * (1 - tmpvar_2.x)) * (1 - tmpvar_2.y))));
          col_1 = (col_1 * float3(0.95, 1.05, 0.95));
          col_1 = (col_1 * (0.9 + (0.1 * sin(((10 * _TimeX) + (tmpvar_2.y * 1000))))));
          col_1 = (col_1 * (0.99 + (0.01 * sin((110 * _TimeX)))));
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          tmpvar_9.xyz = float3(col_1);
          out_f.color = tmpvar_9;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
