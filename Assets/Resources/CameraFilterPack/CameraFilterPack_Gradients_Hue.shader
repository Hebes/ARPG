Shader "CameraFilterPack/Gradients_Hue"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
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
      uniform float _Value;
      uniform float _Value2;
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
          float4 tc_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2);
          tc_1 = tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (((0.2126 * tc_1.x) + (0.7152 * tc_1.y)) + (0.0722 * tc_1.z));
          float4 tmpvar_5;
          tmpvar_5.w = 0;
          tmpvar_5.xyz = float3(clamp((abs(((frac((lerp(tmpvar_4, (1 - tmpvar_4), _Value) + float3(1, 0.6666667, 0.3333333))) * 6) - 3)) - 1), float3(0, 0, 0), float3(1, 1, 1)));
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = lerp(tc_1, tmpvar_5, float4(_Value2, _Value2, _Value2, _Value2)).xyz;
          tc_1 = tmpvar_6;
          out_f.color = tmpvar_6;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
