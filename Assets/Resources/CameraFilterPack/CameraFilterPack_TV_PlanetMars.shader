Shader "CameraFilterPack/TV_PlanetMars"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
          float3 noise_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2).xyz;
          noise_1 = tmpvar_3;
          float _t_4;
          _t_4 = (dot(noise_1, float3(0.2126, 0.7152, 0.0722)) * 4000);
          float3 RGB_5;
          float tmpvar_6;
          tmpvar_6 = (((0.8601177 + (0.0001541183 * _t_4)) + ((1.286412E-07 * _t_4) * _t_4)) / ((1 + (0.0008424202 * _t_4)) + ((7.081451E-07 * _t_4) * _t_4)));
          float tmpvar_7;
          tmpvar_7 = (((0.3173987 + (4.228063E-05 * _t_4)) + ((4.204817E-08 * _t_4) * _t_4)) / ((1 - (2.897418E-05 * _t_4)) + ((1.614561E-07 * _t_4) * _t_4)));
          float tmpvar_8;
          tmpvar_8 = ((3 * tmpvar_6) / (((2 * tmpvar_6) - (8 * tmpvar_7)) + 4));
          float tmpvar_9;
          tmpvar_9 = ((2 * tmpvar_7) / (((2 * tmpvar_6) - (8 * tmpvar_7)) + 4));
          float tmpvar_10;
          tmpvar_10 = ((1 / tmpvar_9) * tmpvar_8);
          float3 tmpvar_11;
          tmpvar_11.x = tmpvar_10;
          tmpvar_11.y = 1;
          tmpvar_11.z = ((1 / tmpvar_9) * ((1 - tmpvar_8) - tmpvar_9));
          RGB_5.x = ((tmpvar_10 * pow(((0.0006 * _t_4) * _Distortion), 4)) / _Distortion);
          RGB_5.y = (pow(((0.0004 * _t_4) * _Distortion), 4) / _Distortion);
          RGB_5.z = ((tmpvar_11.z * pow(((0.0004 * _t_4) * _Distortion), 4)) * _Distortion);
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = float3(RGB_5);
          out_f.color = tmpvar_12;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
