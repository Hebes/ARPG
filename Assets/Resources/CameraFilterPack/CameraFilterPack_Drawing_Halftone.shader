Shader "CameraFilterPack/Drawing_Halftone"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _DotSize ("_DotSize", Range(1, 16)) = 10
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
      uniform float _Distortion;
      uniform float _DotSize;
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
          float rasterPattern_1;
          float threshold_2;
          threshold_2 = _Distortion;
          float tmpvar_3;
          float2 sh_4;
          sh_4 = in_f.xlv_TEXCOORD0;
          float d_5;
          d_5 = (2136.281 / _DotSize);
          float2 tmpvar_6;
          tmpvar_6 = (sh_4 * 0.7071064);
          tmpvar_3 = ((0.5 + (0.25 * cos(((tmpvar_6.x + tmpvar_6.y) * d_5)))) + (0.25 * cos(((tmpvar_6.x - tmpvar_6.y) * d_5))));
          rasterPattern_1 = tmpvar_3;
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          float tmpvar_8;
          tmpvar_8 = ((((rasterPattern_1 * threshold_2) + (((0.2125 * tmpvar_7.x) + (0.7154 * tmpvar_7.y)) + (0.0721 * tmpvar_7.z))) - threshold_2) / (1 - threshold_2));
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          tmpvar_9.x = tmpvar_8;
          tmpvar_9.y = tmpvar_8;
          tmpvar_9.z = tmpvar_8;
          out_f.color = tmpvar_9;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
