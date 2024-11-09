Shader "CameraFilterPack/FX_Drunk2"
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
          float4 tU_1;
          float4 tD_2;
          float4 tR_3;
          float4 tl_4;
          float4 tc_5;
          float2 uv_6;
          uv_6 = in_f.xlv_TEXCOORD0;
          float tmpvar_7;
          tmpvar_7 = (sin(_TimeX) * 0.05);
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, uv_6);
          tc_5 = tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9.y = 0;
          tmpvar_9.x = sin(tmpvar_7);
          float4 tmpvar_10;
          float2 P_11;
          P_11 = (uv_6 - tmpvar_9);
          tmpvar_10 = tex2D(_MainTex, P_11);
          tl_4 = tmpvar_10;
          float2 tmpvar_12;
          tmpvar_12.y = 0;
          tmpvar_12.x = sin(tmpvar_7);
          float4 tmpvar_13;
          float2 P_14;
          P_14 = (uv_6 + tmpvar_12);
          tmpvar_13 = tex2D(_MainTex, P_14);
          tR_3 = tmpvar_13;
          float2 tmpvar_15;
          tmpvar_15.x = 0;
          tmpvar_15.y = sin(tmpvar_7);
          float4 tmpvar_16;
          float2 P_17;
          P_17 = (uv_6 - tmpvar_15);
          tmpvar_16 = tex2D(_MainTex, P_17);
          tD_2 = tmpvar_16;
          float2 tmpvar_18;
          tmpvar_18.x = 0;
          tmpvar_18.y = sin(tmpvar_7);
          float4 tmpvar_19;
          float2 P_20;
          P_20 = (uv_6 + tmpvar_18);
          tmpvar_19 = tex2D(_MainTex, P_20);
          tU_1 = tmpvar_19;
          float4 tmpvar_21;
          tmpvar_21 = (((tc_5 + tl_4) + ((tR_3 + tD_2) + tU_1)) / 5);
          out_f.color = tmpvar_21;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
