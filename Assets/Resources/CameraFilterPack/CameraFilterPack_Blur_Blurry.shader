Shader "CameraFilterPack/Blur_Blurry"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Amount ("_Amount", Range(0, 20)) = 5
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
          float3 result_1;
          float stepV_2;
          float stepU_3;
          stepU_3 = ((1 / _ScreenResolution.x) * _Amount);
          stepV_2 = ((1 / _ScreenResolution.y) * _Amount);
          float2 tmpvar_4;
          tmpvar_4.x = (-stepU_3);
          tmpvar_4.y = (-stepV_2);
          float2 tmpvar_5;
          tmpvar_5 = (in_f.xlv_TEXCOORD0 + tmpvar_4);
          float3 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, tmpvar_5).xyz;
          result_1 = tmpvar_6;
          float2 tmpvar_7;
          tmpvar_7.x = (-stepU_3);
          tmpvar_7.y = 0;
          float2 tmpvar_8;
          tmpvar_8 = (in_f.xlv_TEXCOORD0 + tmpvar_7);
          float3 tmpvar_9;
          tmpvar_9 = (2 * tex2D(_MainTex, tmpvar_8)).xyz;
          result_1 = (result_1 + tmpvar_9);
          float2 tmpvar_10;
          tmpvar_10.x = (-stepU_3);
          tmpvar_10.y = stepV_2;
          float2 tmpvar_11;
          tmpvar_11 = (in_f.xlv_TEXCOORD0 + tmpvar_10);
          float3 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, tmpvar_11).xyz;
          result_1 = (result_1 + tmpvar_12);
          float2 tmpvar_13;
          tmpvar_13.x = 0;
          tmpvar_13.y = (-stepV_2);
          float2 tmpvar_14;
          tmpvar_14 = (in_f.xlv_TEXCOORD0 + tmpvar_13);
          float3 tmpvar_15;
          tmpvar_15 = (2 * tex2D(_MainTex, tmpvar_14)).xyz;
          result_1 = (result_1 + tmpvar_15);
          float2 tmpvar_16;
          tmpvar_16 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_17;
          tmpvar_17 = (4 * tex2D(_MainTex, tmpvar_16)).xyz;
          result_1 = (result_1 + tmpvar_17);
          float2 tmpvar_18;
          tmpvar_18.x = 0;
          tmpvar_18.y = stepV_2;
          float2 tmpvar_19;
          tmpvar_19 = (in_f.xlv_TEXCOORD0 + tmpvar_18);
          float3 tmpvar_20;
          tmpvar_20 = (2 * tex2D(_MainTex, tmpvar_19)).xyz;
          result_1 = (result_1 + tmpvar_20);
          float2 tmpvar_21;
          tmpvar_21.x = stepU_3;
          tmpvar_21.y = (-stepV_2);
          float2 tmpvar_22;
          tmpvar_22 = (in_f.xlv_TEXCOORD0 + tmpvar_21);
          float3 tmpvar_23;
          tmpvar_23 = tex2D(_MainTex, tmpvar_22).xyz;
          result_1 = (result_1 + tmpvar_23);
          float2 tmpvar_24;
          tmpvar_24.x = stepU_3;
          tmpvar_24.y = 0;
          float2 tmpvar_25;
          tmpvar_25 = (in_f.xlv_TEXCOORD0 + tmpvar_24);
          float3 tmpvar_26;
          tmpvar_26 = (2 * tex2D(_MainTex, tmpvar_25)).xyz;
          result_1 = (result_1 + tmpvar_26);
          float2 tmpvar_27;
          tmpvar_27.x = stepU_3;
          tmpvar_27.y = stepV_2;
          float2 tmpvar_28;
          tmpvar_28 = (in_f.xlv_TEXCOORD0 + tmpvar_27);
          float3 tmpvar_29;
          tmpvar_29 = tex2D(_MainTex, tmpvar_28).xyz;
          result_1 = (result_1 + tmpvar_29);
          float4 tmpvar_30;
          tmpvar_30.w = 1;
          tmpvar_30.xyz = float3((result_1 / 16));
          out_f.color = tmpvar_30;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
