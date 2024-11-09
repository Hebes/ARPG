Shader "CameraFilterPack/Blur_Radial"
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
      float xlat_mutable_Value;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float scale_1;
          float4 color_2;
          float2 uv_3;
          float2 tmpvar_4;
          tmpvar_4.x = _Value2;
          tmpvar_4.y = _Value3;
          uv_3 = in_f.xlv_TEXCOORD0;
          uv_3 = (uv_3 - tmpvar_4);
          xlat_mutable_Value = (_Value * 0.075);
          float4 tmpvar_5;
          float2 P_6;
          P_6 = (uv_3 + tmpvar_4);
          tmpvar_5 = tex2D(_MainTex, P_6);
          color_2 = tmpvar_5;
          scale_1 = (1 + xlat_mutable_Value);
          float4 tmpvar_7;
          float2 P_8;
          P_8 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_7 = tex2D(_MainTex, P_8);
          color_2 = (color_2 + tmpvar_7);
          scale_1 = (1 + (2 * xlat_mutable_Value));
          float4 tmpvar_9;
          float2 P_10;
          P_10 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_9 = tex2D(_MainTex, P_10);
          color_2 = (color_2 + tmpvar_9);
          scale_1 = (1 + (3 * xlat_mutable_Value));
          float4 tmpvar_11;
          float2 P_12;
          P_12 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_11 = tex2D(_MainTex, P_12);
          color_2 = (color_2 + tmpvar_11);
          scale_1 = (1 + (4 * xlat_mutable_Value));
          float4 tmpvar_13;
          float2 P_14;
          P_14 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_13 = tex2D(_MainTex, P_14);
          color_2 = (color_2 + tmpvar_13);
          scale_1 = (1 + (5 * xlat_mutable_Value));
          float4 tmpvar_15;
          float2 P_16;
          P_16 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_15 = tex2D(_MainTex, P_16);
          color_2 = (color_2 + tmpvar_15);
          scale_1 = (1 + (6 * xlat_mutable_Value));
          float4 tmpvar_17;
          float2 P_18;
          P_18 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_17 = tex2D(_MainTex, P_18);
          color_2 = (color_2 + tmpvar_17);
          scale_1 = (1 + (7 * xlat_mutable_Value));
          float4 tmpvar_19;
          float2 P_20;
          P_20 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_19 = tex2D(_MainTex, P_20);
          color_2 = (color_2 + tmpvar_19);
          scale_1 = (1 + (8 * xlat_mutable_Value));
          float4 tmpvar_21;
          float2 P_22;
          P_22 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_21 = tex2D(_MainTex, P_22);
          color_2 = (color_2 + tmpvar_21);
          scale_1 = (1 + (9 * xlat_mutable_Value));
          float4 tmpvar_23;
          float2 P_24;
          P_24 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_23 = tex2D(_MainTex, P_24);
          color_2 = (color_2 + tmpvar_23);
          scale_1 = (1 + (10 * xlat_mutable_Value));
          float4 tmpvar_25;
          float2 P_26;
          P_26 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_25 = tex2D(_MainTex, P_26);
          color_2 = (color_2 + tmpvar_25);
          scale_1 = (1 + (11 * xlat_mutable_Value));
          float4 tmpvar_27;
          float2 P_28;
          P_28 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_27 = tex2D(_MainTex, P_28);
          color_2 = (color_2 + tmpvar_27);
          scale_1 = (1 + (12 * xlat_mutable_Value));
          float4 tmpvar_29;
          float2 P_30;
          P_30 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_29 = tex2D(_MainTex, P_30);
          color_2 = (color_2 + tmpvar_29);
          scale_1 = (1 + (13 * xlat_mutable_Value));
          float4 tmpvar_31;
          float2 P_32;
          P_32 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_31 = tex2D(_MainTex, P_32);
          color_2 = (color_2 + tmpvar_31);
          scale_1 = (1 + (14 * xlat_mutable_Value));
          float4 tmpvar_33;
          float2 P_34;
          P_34 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_33 = tex2D(_MainTex, P_34);
          color_2 = (color_2 + tmpvar_33);
          scale_1 = (1 + (15 * xlat_mutable_Value));
          float4 tmpvar_35;
          float2 P_36;
          P_36 = ((uv_3 * scale_1) + tmpvar_4);
          tmpvar_35 = tex2D(_MainTex, P_36);
          color_2 = (color_2 + tmpvar_35);
          color_2 = (color_2 / 16);
          out_f.color = color_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
