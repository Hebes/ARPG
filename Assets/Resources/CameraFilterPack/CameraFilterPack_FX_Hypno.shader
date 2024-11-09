Shader "CameraFilterPack/FX_Hypno"
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
      uniform float _Value4;
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
          float3 res_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          uv_2 = ((uv_2 * 2) - 1);
          uv_2 = (uv_2 * uv_2);
          float tmpvar_3;
          tmpvar_3 = (_TimeX * _Value);
          float a_4;
          a_4 = (sin(tmpvar_3) + tmpvar_3);
          float2 tmpvar_5;
          tmpvar_5.x = ((cos(a_4) * uv_2.x) - (sin(a_4) * uv_2.y));
          tmpvar_5.y = ((sin(a_4) * uv_2.x) + (cos(a_4) * uv_2.y));
          uv_2 = tmpvar_5;
          float2 uv_6;
          uv_6 = (tmpvar_5 * (sin((tmpvar_3 * 3)) + 2));
          float3 tt_7;
          float tmpvar_8;
          tmpvar_8 = (uv_6.x - (floor((uv_6.x * 6.666667)) * 0.15));
          float tmpvar_9;
          tmpvar_9 = (uv_6.y - (floor((uv_6.y * 6.666667)) * 0.15));
          float tmpvar_10;
          tmpvar_10 = clamp(((tmpvar_8 - 0.02) / 0.03), 0, 1);
          float tmpvar_11;
          tmpvar_11 = clamp(((tmpvar_8 - 0.06) / 0.02), 0, 1);
          float tmpvar_12;
          tmpvar_12 = clamp(((tmpvar_9 - 0.02) / 0.03), 0, 1);
          float tmpvar_13;
          tmpvar_13 = clamp(((tmpvar_9 - 0.06) / 0.02), 0, 1);
          float3 tmpvar_14;
          tmpvar_14.z = 1;
          tmpvar_14.x = (uv_6.x - (floor((uv_6.x * 1.666667)) * 0.6));
          tmpvar_14.y = (uv_6.y - (floor((uv_6.y * 2)) * 0.5));
          float3 tmpvar_15;
          tmpvar_15 = (tmpvar_14 * (((tmpvar_10 * (tmpvar_10 * (3 - (2 * tmpvar_10)))) * (1 - (tmpvar_11 * (tmpvar_11 * (3 - (2 * tmpvar_11)))))) + ((tmpvar_12 * (tmpvar_12 * (3 - (2 * tmpvar_12)))) * (1 - (tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13))))))));
          tt_7.x = (tmpvar_15.x * _Value2);
          tt_7.y = (tmpvar_15.y * _Value3);
          tt_7.z = (tmpvar_15.z * _Value4);
          float3 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz;
          res_1 = tmpvar_16;
          res_1 = (res_1 + abs(tt_7));
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(res_1);
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
