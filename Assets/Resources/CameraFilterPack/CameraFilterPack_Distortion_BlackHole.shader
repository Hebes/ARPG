Shader "CameraFilterPack/Distortion_BlackHole"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _Distortion2 ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _PositionX ("_PositionX", Range(-1, 1)) = 1.5
    _PositionY ("_PositionY", Range(-1, 1)) = 30
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
      uniform float _Distortion2;
      uniform float4 _ScreenResolution;
      uniform float _PositionX;
      uniform float _PositionY;
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
          float2 warp_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_3;
          tmpvar_3.x = ((0.5 + (_PositionX / 2)) * _ScreenResolution.x);
          tmpvar_3.y = ((0.5 - (_PositionY / 2)) * _ScreenResolution.y);
          float2 tmpvar_4;
          float2 tmpvar_5;
          tmpvar_5 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          tmpvar_4 = (tmpvar_3 - tmpvar_5);
          float2 tmpvar_6;
          tmpvar_6 = (((normalize((tmpvar_3 - tmpvar_5)) * pow(sqrt(dot(tmpvar_4, tmpvar_4)), (-2))) * _Distortion2) * _Distortion2);
          warp_1.x = tmpvar_6.x;
          warp_1.y = (-tmpvar_6.y);
          uv_2 = (uv_2 + warp_1);
          float2 tmpvar_7;
          tmpvar_7 = (tmpvar_3 - tmpvar_5);
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, uv_2);
          float4 tmpvar_9;
          tmpvar_9 = (tmpvar_8 * clamp(((0.1 * sqrt(dot(tmpvar_7, tmpvar_7))) - _Distortion), 0, 1));
          out_f.color = tmpvar_9;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
