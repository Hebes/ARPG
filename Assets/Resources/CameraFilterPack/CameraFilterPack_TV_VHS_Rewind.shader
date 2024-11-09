Shader "CameraFilterPack/TV_VHS_Rewind"
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
      uniform float _TimeX;
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
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
          float4 tmpvar_1;
          float3 text_2;
          float3 aberration_3;
          float2 uv_4;
          float2 q_5;
          q_5 = in_f.xlv_TEXCOORD0;
          aberration_3 = (float3(0.019, 0, (-0.019)) * (abs(((0.5 + ((q_5 - 0.5) * (0.9 + (0.1 * sin((0.2 * _TimeX)))))).x - 0.5)) * _Value));
          float2 tmpvar_6;
          tmpvar_6.x = (q_5.x * (_ScreenResolution.x / _ScreenResolution.y));
          tmpvar_6.y = (q_5.y * _Value);
          float tmpvar_7;
          float tmpvar_8;
          tmpvar_8 = (-_TimeX);
          tmpvar_7 = (clamp(((tmpvar_6.y - frac((tmpvar_8 * _Value2))) - 0.05), 0, 0.1) * 10);
          float2 tmpvar_9;
          tmpvar_9.y = 0;
          tmpvar_9.x = ((sin((tmpvar_7 * 10)) * ((-4 * (tmpvar_7 - 0.5)) + 2)) * 0.02);
          uv_4 = (q_5 + tmpvar_9);
          float2 tmpvar_10;
          tmpvar_10.x = (uv_4.x + aberration_3.x);
          tmpvar_10.y = uv_4.y;
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, tmpvar_10);
          text_2.x = tmpvar_11.x;
          float2 tmpvar_12;
          tmpvar_12.x = (uv_4.x + aberration_3.y);
          tmpvar_12.y = uv_4.y;
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, tmpvar_12);
          text_2.y = tmpvar_13.y;
          float2 tmpvar_14;
          tmpvar_14.x = (uv_4.x + aberration_3.z);
          tmpvar_14.y = uv_4.y;
          float4 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex, tmpvar_14);
          text_2.z = tmpvar_15.z;
          float _tmp_dvx_76 = ((clamp(((tmpvar_6.y - frac((tmpvar_8 * _Value3))) - 0.05), 0, 0.1) * 10) * (1 - uv_4.y));
          text_2 = lerp(text_2, text_2.xxx, float3(_tmp_dvx_76, _tmp_dvx_76, _tmp_dvx_76));
          float _tmp_dvx_77 = (tmpvar_7 * (1 - uv_4.y));
          text_2 = lerp(text_2, text_2.xxx, float3(_tmp_dvx_77, _tmp_dvx_77, _tmp_dvx_77));
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = float3(text_2);
          tmpvar_1 = tmpvar_16;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
