Shader "CameraFilterPack/Edge_Sobel"
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
          float stepx_1;
          stepx_1 = (1 / _ScreenResolution.x);
          float stepy_2;
          stepy_2 = (1 / _ScreenResolution.y);
          float2 center_3;
          center_3 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_4;
          tmpvar_4.x = (-stepx_1);
          tmpvar_4.y = stepy_2;
          float4 tmpvar_5;
          float2 P_6;
          P_6 = (center_3 + tmpvar_4);
          tmpvar_5 = tex2D(_MainTex, P_6);
          float4 color_7;
          color_7 = tmpvar_5;
          float tmpvar_8;
          tmpvar_8 = sqrt((((color_7.x * color_7.x) + (color_7.y * color_7.y)) + (color_7.z * color_7.z)));
          float2 tmpvar_9;
          tmpvar_9.y = 0;
          tmpvar_9.x = (-stepx_1);
          float4 tmpvar_10;
          float2 P_11;
          P_11 = (center_3 + tmpvar_9);
          tmpvar_10 = tex2D(_MainTex, P_11);
          float4 color_12;
          color_12 = tmpvar_10;
          float2 tmpvar_13;
          tmpvar_13.x = (-stepx_1);
          tmpvar_13.y = (-stepy_2);
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (center_3 + tmpvar_13);
          tmpvar_14 = tex2D(_MainTex, P_15);
          float4 color_16;
          color_16 = tmpvar_14;
          float tmpvar_17;
          tmpvar_17 = sqrt((((color_16.x * color_16.x) + (color_16.y * color_16.y)) + (color_16.z * color_16.z)));
          float2 tmpvar_18;
          tmpvar_18.x = 0;
          tmpvar_18.y = stepy_2;
          float4 tmpvar_19;
          float2 P_20;
          P_20 = (center_3 + tmpvar_18);
          tmpvar_19 = tex2D(_MainTex, P_20);
          float4 color_21;
          color_21 = tmpvar_19;
          float2 tmpvar_22;
          tmpvar_22.x = 0;
          tmpvar_22.y = (-stepy_2);
          float4 tmpvar_23;
          float2 P_24;
          P_24 = (center_3 + tmpvar_22);
          tmpvar_23 = tex2D(_MainTex, P_24);
          float4 color_25;
          color_25 = tmpvar_23;
          float2 tmpvar_26;
          tmpvar_26.x = stepx_1;
          tmpvar_26.y = stepy_2;
          float4 tmpvar_27;
          float2 P_28;
          P_28 = (center_3 + tmpvar_26);
          tmpvar_27 = tex2D(_MainTex, P_28);
          float4 color_29;
          color_29 = tmpvar_27;
          float tmpvar_30;
          tmpvar_30 = sqrt((((color_29.x * color_29.x) + (color_29.y * color_29.y)) + (color_29.z * color_29.z)));
          float2 tmpvar_31;
          tmpvar_31.y = 0;
          tmpvar_31.x = stepx_1;
          float4 tmpvar_32;
          float2 P_33;
          P_33 = (center_3 + tmpvar_31);
          tmpvar_32 = tex2D(_MainTex, P_33);
          float4 color_34;
          color_34 = tmpvar_32;
          float2 tmpvar_35;
          tmpvar_35.x = stepx_1;
          tmpvar_35.y = (-stepy_2);
          float4 tmpvar_36;
          float2 P_37;
          P_37 = (center_3 + tmpvar_35);
          tmpvar_36 = tex2D(_MainTex, P_37);
          float4 color_38;
          color_38 = tmpvar_36;
          float tmpvar_39;
          tmpvar_39 = sqrt((((color_38.x * color_38.x) + (color_38.y * color_38.y)) + (color_38.z * color_38.z)));
          float tmpvar_40;
          tmpvar_40 = (((((tmpvar_8 + (2 * sqrt((((color_12.x * color_12.x) + (color_12.y * color_12.y)) + (color_12.z * color_12.z))))) + tmpvar_17) - tmpvar_30) - (2 * sqrt((((color_34.x * color_34.x) + (color_34.y * color_34.y)) + (color_34.z * color_34.z))))) - tmpvar_39);
          float tmpvar_41;
          tmpvar_41 = ((((((-tmpvar_8) - (2 * sqrt((((color_21.x * color_21.x) + (color_21.y * color_21.y)) + (color_21.z * color_21.z))))) - tmpvar_30) + tmpvar_17) + (2 * sqrt((((color_25.x * color_25.x) + (color_25.y * color_25.y)) + (color_25.z * color_25.z))))) + tmpvar_39);
          float tmpvar_42;
          tmpvar_42 = sqrt(((tmpvar_40 * tmpvar_40) + (tmpvar_41 * tmpvar_41)));
          float3 tmpvar_43;
          tmpvar_43.x = tmpvar_42;
          tmpvar_43.y = tmpvar_42;
          tmpvar_43.z = tmpvar_42;
          float4 tmpvar_44;
          tmpvar_44.w = 1;
          tmpvar_44.xyz = float3(tmpvar_43);
          out_f.color = tmpvar_44;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
