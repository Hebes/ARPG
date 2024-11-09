Shader "CameraFilterPack/FX_superDot"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
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
      uniform float4 _ScreenResolution;
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
          float4 color_1;
          float2 tmpvar_2;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          tmpvar_2 = (floor((tmpvar_3 / 8)) * 8);
          float2 tmpvar_4;
          tmpvar_4 = ((tmpvar_3 - tmpvar_2) / 8);
          float4 tmpvar_5;
          float2 P_6;
          P_6 = (tmpvar_2 / _ScreenResolution.xy);
          tmpvar_5 = tex2D(_MainTex, P_6);
          color_1 = tmpvar_5;
          float4 tmpvar_7;
          tmpvar_7 = ((color_1 * ((32 * ((((0.25 * tmpvar_4.x) * ((tmpvar_4.x * tmpvar_4.x) * tmpvar_4.x)) - ((0.5 * tmpvar_4.x) * (tmpvar_4.x * tmpvar_4.x))) + ((0.25 * tmpvar_4.x) * tmpvar_4.x))) + 0.5)) * ((32 * ((((0.25 * tmpvar_4.y) * ((tmpvar_4.y * tmpvar_4.y) * tmpvar_4.y)) - ((0.5 * tmpvar_4.y) * (tmpvar_4.y * tmpvar_4.y))) + ((0.25 * tmpvar_4.y) * tmpvar_4.y))) + 0.5));
          out_f.color = tmpvar_7;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
