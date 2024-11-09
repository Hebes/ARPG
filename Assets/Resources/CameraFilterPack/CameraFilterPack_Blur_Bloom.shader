Shader "CameraFilterPack/Blur_Bloom"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Amount ("_Amount", Range(0, 20)) = 5
    _Glow ("_Glow", Range(0, 20)) = 5
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
      uniform float _Amount;
      uniform float _Glow;
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
          float4 result_1;
          float stepV_2;
          float stepU_3;
          stepU_3 = ((1 / _ScreenResolution.x) * _Amount);
          stepV_2 = ((1 / _ScreenResolution.y) * _Amount);
          float3 tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz;
          float2 tmpvar_5;
          tmpvar_5.x = (-stepU_3);
          tmpvar_5.y = (-stepV_2);
          float2 tmpvar_6;
          tmpvar_6 = (in_f.xlv_TEXCOORD0 + tmpvar_5);
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex, tmpvar_6);
          result_1 = tmpvar_7;
          float2 tmpvar_8;
          tmpvar_8.x = (-stepU_3);
          tmpvar_8.y = 0;
          float2 tmpvar_9;
          tmpvar_9 = (in_f.xlv_TEXCOORD0 + tmpvar_8);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9);
          result_1 = (result_1 + (2 * tmpvar_10));
          float2 tmpvar_11;
          tmpvar_11.x = (-stepU_3);
          tmpvar_11.y = stepV_2;
          float2 tmpvar_12;
          tmpvar_12 = (in_f.xlv_TEXCOORD0 + tmpvar_11);
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, tmpvar_12);
          result_1 = (result_1 + tmpvar_13);
          float2 tmpvar_14;
          tmpvar_14.x = 0;
          tmpvar_14.y = (-stepV_2);
          float2 tmpvar_15;
          tmpvar_15 = (in_f.xlv_TEXCOORD0 + tmpvar_14);
          float4 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, tmpvar_15);
          result_1 = (result_1 + (2 * tmpvar_16));
          float2 tmpvar_17;
          tmpvar_17 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_18;
          tmpvar_18 = tex2D(_MainTex, tmpvar_17);
          result_1 = (result_1 + (4 * tmpvar_18));
          float2 tmpvar_19;
          tmpvar_19.x = 0;
          tmpvar_19.y = stepV_2;
          float2 tmpvar_20;
          tmpvar_20 = (in_f.xlv_TEXCOORD0 + tmpvar_19);
          float4 tmpvar_21;
          tmpvar_21 = tex2D(_MainTex, tmpvar_20);
          result_1 = (result_1 + (2 * tmpvar_21));
          float2 tmpvar_22;
          tmpvar_22.x = stepU_3;
          tmpvar_22.y = (-stepV_2);
          float2 tmpvar_23;
          tmpvar_23 = (in_f.xlv_TEXCOORD0 + tmpvar_22);
          float4 tmpvar_24;
          tmpvar_24 = tex2D(_MainTex, tmpvar_23);
          result_1 = (result_1 + tmpvar_24);
          float2 tmpvar_25;
          tmpvar_25.x = stepU_3;
          tmpvar_25.y = 0;
          float2 tmpvar_26;
          tmpvar_26 = (in_f.xlv_TEXCOORD0 + tmpvar_25);
          float4 tmpvar_27;
          tmpvar_27 = tex2D(_MainTex, tmpvar_26);
          result_1 = (result_1 + (2 * tmpvar_27));
          float2 tmpvar_28;
          tmpvar_28.x = stepU_3;
          tmpvar_28.y = stepV_2;
          float2 tmpvar_29;
          tmpvar_29 = (in_f.xlv_TEXCOORD0 + tmpvar_28);
          float4 tmpvar_30;
          tmpvar_30 = tex2D(_MainTex, tmpvar_29);
          result_1 = (result_1 + tmpvar_30);
          result_1 = (result_1 / 8);
          result_1.xyz = lerp(tmpvar_4, result_1.xyz, float3(_Glow, _Glow, _Glow));
          out_f.color = result_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
