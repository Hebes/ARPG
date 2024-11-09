Shader "CameraFilterPack/Drawing_EnhancedComics"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _DotSize ("_DotSize", Range(0, 1)) = 0
    _ColorRGB ("_ColorRGB", Color) = (1,0,0,1)
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
      uniform float4 _ColorRGB;
      uniform float _DotSize;
      uniform float _ColorR;
      uniform float _ColorG;
      uniform float _ColorB;
      uniform float _Blood;
      uniform float _SmoothStart;
      uniform float _SmoothEnd;
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
          float3 brtColor_1;
          float3 color_2;
          float3 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz;
          color_2 = tmpvar_3;
          brtColor_1 = color_2;
          float edge0_4;
          edge0_4 = (_DotSize + _SmoothStart);
          float tmpvar_5;
          tmpvar_5 = clamp((((((color_2.x + color_2.y) + color_2.z) / 3) - edge0_4) / (((0.1 + _DotSize) + _SmoothEnd) - edge0_4)), 0, 1);
          float _tmp_dvx_109 = (tmpvar_5 * (tmpvar_5 * (3 - (2 * tmpvar_5))));
          color_2 = float3(_tmp_dvx_109, _tmp_dvx_109, _tmp_dvx_109);
          float3 tmpvar_6;
          tmpvar_6.x = 1;
          tmpvar_6.y = _Blood;
          tmpvar_6.z = _Blood;
          float3 tmpvar_7;
          tmpvar_7 = lerp(tmpvar_6, (brtColor_1 * float3(8.88, 8.88, 8.88)), float3(8.39, 8.39, 8.39));
          if((((tmpvar_7.x>_ColorR) && (tmpvar_7.y<_ColorG)) && (tmpvar_7.z<_ColorB)))
          {
              color_2 = _ColorRGB.xyz;
          }
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          tmpvar_8.xyz = float3(color_2);
          out_f.color = tmpvar_8;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
