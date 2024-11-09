Shader "CameraFilterPack/Distortion_Water_Drop"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _CenterX ("_CenterX", Range(-1, 1)) = 0
    _CenterY ("_CenterY", Range(-1, 1)) = 0
    _WaveIntensity ("_WaveIntensity", Range(0, 10)) = 0
    _NumberOfWaves ("_NumberOfWaves", Range(0, 10)) = 0
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
      uniform float _CenterX;
      uniform float _CenterY;
      uniform float _WaveIntensity;
      uniform int _NumberOfWaves;
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
          float4 tmpvar_1;
          float2 tmpvar_2;
          tmpvar_2.x = (0.5 + (_CenterX * 0.5));
          tmpvar_2.y = (0.5 - (_CenterY * 0.5));
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 - tmpvar_2);
          float tmpvar_4;
          tmpvar_4 = sqrt(dot(tmpvar_3, tmpvar_3));
          float wave_5;
          wave_5 = ((sin((((8 * float((3 + _NumberOfWaves))) * tmpvar_4) + ((-_TimeX) * 5))) + 1) * 0.5);
          wave_5 = (wave_5 - 0.3);
          wave_5 = (wave_5 * (wave_5 * wave_5));
          float4 tmpvar_6;
          float2 P_7;
          P_7 = (in_f.xlv_TEXCOORD0 + (((-normalize(tmpvar_3)) * ((wave_5 * _WaveIntensity) / 3)) / (1 + (5 * tmpvar_4))));
          tmpvar_6 = tex2D(_MainTex, P_7);
          tmpvar_1 = tmpvar_6;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
