Shader "CameraFilterPack/Sharpen_Sharpen"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Value ("Value", Range(0, 1)) = 1
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
      uniform float _Value;
      uniform float _Value2;
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
          float2 uv_1;
          uv_1 = in_f.xlv_TEXCOORD0;
          float4 c4_2;
          float4 c3_3;
          float4 c2_4;
          float4 c1_5;
          float4 c0_6;
          float tmpvar_7;
          tmpvar_7 = (_Value2 / _ScreenResolution.x);
          float tmpvar_8;
          tmpvar_8 = (9 * _Value);
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, uv_1);
          c0_6 = tmpvar_9;
          float2 tmpvar_10;
          tmpvar_10.y = 0;
          tmpvar_10.x = tmpvar_7;
          float4 tmpvar_11;
          float2 P_12;
          P_12 = (uv_1 - tmpvar_10);
          tmpvar_11 = tex2D(_MainTex, P_12);
          c1_5 = tmpvar_11;
          float2 tmpvar_13;
          tmpvar_13.y = 0;
          tmpvar_13.x = tmpvar_7;
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_1 + tmpvar_13);
          tmpvar_14 = tex2D(_MainTex, P_15);
          c2_4 = tmpvar_14;
          float2 tmpvar_16;
          tmpvar_16.x = 0;
          tmpvar_16.y = tmpvar_7;
          float4 tmpvar_17;
          float2 P_18;
          P_18 = (uv_1 - tmpvar_16);
          tmpvar_17 = tex2D(_MainTex, P_18);
          c3_3 = tmpvar_17;
          float2 tmpvar_19;
          tmpvar_19.x = 0;
          tmpvar_19.y = tmpvar_7;
          float4 tmpvar_20;
          float2 P_21;
          P_21 = (uv_1 + tmpvar_19);
          tmpvar_20 = tex2D(_MainTex, P_21);
          c4_2 = tmpvar_20;
          float4 tmpvar_22;
          tmpvar_22 = clamp(min(min(c0_6, c1_5), min(min(c2_4, c3_3), c4_2)), (((tmpvar_8 + 1) * c0_6) - ((((c0_6 + c1_5) + ((c2_4 + c3_3) + c4_2)) * 0.2) * tmpvar_8)), max(max(c0_6, c1_5), max(max(c2_4, c3_3), c4_2)));
          out_f.color = tmpvar_22;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
