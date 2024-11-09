Shader "CameraFilterPack/Edge_Golden"
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
          float3 texD_1;
          float3 texC_2;
          float3 texB_3;
          float3 texA_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_6;
          tmpvar_6 = (1 / _ScreenResolution.xy);
          float2 P_7;
          P_7 = (uv_5 + ((-tmpvar_6) * 1.5));
          float3 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, P_7).xyz;
          texA_4 = tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9.x = tmpvar_6.x;
          tmpvar_9.y = (-tmpvar_6.y);
          float2 P_10;
          P_10 = (uv_5 + (tmpvar_9 * 1.5));
          float3 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, P_10).xyz;
          texB_3 = tmpvar_11;
          float2 tmpvar_12;
          tmpvar_12.x = (-tmpvar_6.x);
          tmpvar_12.y = tmpvar_6.y;
          float2 P_13;
          P_13 = (uv_5 + (tmpvar_12 * 1.5));
          float3 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex, P_13).xyz;
          texC_2 = tmpvar_14;
          float2 P_15;
          P_15 = (uv_5 + (tmpvar_6 * 1.5));
          float3 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, P_15).xyz;
          texD_1 = tmpvar_16;
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          float _tmp_dvx_41 = (15 * pow(max(abs((dot(texA_4, float3(0.333333, 0.333333, 0.333333)) - dot(texD_1, float3(0.333333, 0.333333, 0.333333)))), abs((dot(texB_3, float3(0.333333, 0.333333, 0.333333)) - dot(texC_2, float3(0.333333, 0.333333, 0.333333))))), 0.5));
          tmpvar_17.xyz = float3(lerp(float3(0.1, 0.18, 0.3), float3(0.4, 0.3, 0.2), float3(_tmp_dvx_41, _tmp_dvx_41, _tmp_dvx_41)));
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
