Shader "CameraFilterPack/Vision_Plasma"
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
          float2 uv_1;
          float tmpvar_2;
          tmpvar_2 = (_TimeX * 0.33);
          uv_1 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_3;
          tmpvar_3.y = 0.5;
          tmpvar_3.x = (0.5 + (0.5 * sin(((8.792 * uv_1.x) + (2.8 * tmpvar_2)))));
          float2 tmpvar_4;
          tmpvar_4 = (uv_1 - tmpvar_3);
          float2 tmpvar_5;
          tmpvar_5.x = (1.6 * cos((tmpvar_2 * 2)));
          tmpvar_5.y = sin((tmpvar_2 * 1.7));
          float2 tmpvar_6;
          tmpvar_6 = (uv_1 - tmpvar_5);
          float3 tmpvar_7;
          tmpvar_7.x = (0.5 * (cos((sqrt(dot(tmpvar_4, tmpvar_4)) * 5.6)) + 1));
          tmpvar_7.y = cos(((4.62 * dot(uv_1, uv_1)) + tmpvar_2));
          tmpvar_7.z = cos((sqrt(dot(tmpvar_6, tmpvar_6)) * 1.3));
          float tmpvar_8;
          tmpvar_8 = (dot(tmpvar_7, float3(1, 1, 1)) / _Value3);
          float2 x_9;
          x_9 = (float2(0.5, 0.5) - uv_1);
          float tmpvar_10;
          tmpvar_10 = clamp(((sqrt(dot(x_9, x_9)) - _Value) / (((_Value - 0.05) - _Value2) - _Value)), 0, 1);
          float3 tmpvar_11;
          tmpvar_11.x = (0.5 * (sin(((6.28 * tmpvar_8) + (tmpvar_2 * 3.45))) + 1));
          tmpvar_11.y = (0.5 * (sin(((6.28 * tmpvar_8) + (tmpvar_2 * 3.15))) + 1));
          tmpvar_11.z = (0.4 * (sin(((6.28 * tmpvar_8) + (tmpvar_2 * 1.26))) + 1));
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, uv_1);
          float4 tmpvar_13;
          tmpvar_13.w = 0;
          tmpvar_13.xyz = float3(tmpvar_11);
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          float _tmp_dvx_4 = (1 - (tmpvar_10 * (tmpvar_10 * (3 - (2 * tmpvar_10)))));
          tmpvar_14.xyz = lerp(tmpvar_12, tmpvar_13, float4(_tmp_dvx_4, _tmp_dvx_4, _tmp_dvx_4, _tmp_dvx_4)).xyz;
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
