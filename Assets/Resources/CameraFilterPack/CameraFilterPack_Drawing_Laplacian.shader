Shader "CameraFilterPack/Drawing_Laplacian"
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
          float4 color_1;
          float2 uv_2;
          float2 tmpvar_3;
          tmpvar_3 = (1 / _ScreenResolution.xy);
          float2 tmpvar_4;
          tmpvar_4 = (2 / _ScreenResolution.xy);
          uv_2 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_5;
          tmpvar_5.y = 0;
          tmpvar_5.x = (-tmpvar_4.x);
          float2 P_6;
          P_6 = (uv_2 + tmpvar_5);
          color_1 = (-tex2D(_MainTex, P_6));
          float2 tmpvar_7;
          tmpvar_7.x = (-tmpvar_4.x);
          tmpvar_7.y = tmpvar_3.y;
          float2 P_8;
          P_8 = (uv_2 + tmpvar_7);
          color_1 = (color_1 - tex2D(_MainTex, P_8));
          float2 tmpvar_9;
          tmpvar_9.x = (-tmpvar_4.x);
          tmpvar_9.y = tmpvar_4.y;
          float2 P_10;
          P_10 = (uv_2 + tmpvar_9);
          color_1 = (color_1 - tex2D(_MainTex, P_10));
          float2 tmpvar_11;
          tmpvar_11.x = (-tmpvar_3.x);
          tmpvar_11.y = (-tmpvar_4.y);
          float2 P_12;
          P_12 = (uv_2 + tmpvar_11);
          color_1 = (color_1 - tex2D(_MainTex, P_12));
          float2 P_13;
          P_13 = (uv_2 - tmpvar_3);
          color_1 = (color_1 - tex2D(_MainTex, P_13));
          float2 tmpvar_14;
          tmpvar_14.y = 0;
          tmpvar_14.x = (-tmpvar_3.x);
          float2 P_15;
          P_15 = (uv_2 + tmpvar_14);
          color_1 = (color_1 - tex2D(_MainTex, P_15));
          float2 tmpvar_16;
          tmpvar_16.x = 0;
          tmpvar_16.y = (-tmpvar_4.y);
          float2 P_17;
          P_17 = (uv_2 + tmpvar_16);
          color_1 = (color_1 - tex2D(_MainTex, P_17));
          float2 tmpvar_18;
          tmpvar_18.x = 0;
          tmpvar_18.y = (-tmpvar_3.y);
          float2 P_19;
          P_19 = (uv_2 + tmpvar_18);
          color_1 = (color_1 - tex2D(_MainTex, P_19));
          color_1 = (color_1 + (16 * tex2D(_MainTex, uv_2)));
          float2 tmpvar_20;
          tmpvar_20.x = 0;
          tmpvar_20.y = tmpvar_3.y;
          float2 P_21;
          P_21 = (uv_2 + tmpvar_20);
          color_1 = (color_1 - tex2D(_MainTex, P_21));
          float2 tmpvar_22;
          tmpvar_22.x = 0;
          tmpvar_22.y = tmpvar_4.y;
          float2 P_23;
          P_23 = (uv_2 + tmpvar_22);
          color_1 = (color_1 - tex2D(_MainTex, P_23));
          float2 tmpvar_24;
          tmpvar_24.x = tmpvar_3.x;
          tmpvar_24.y = (-tmpvar_4.y);
          float2 P_25;
          P_25 = (uv_2 + tmpvar_24);
          color_1 = (color_1 - tex2D(_MainTex, P_25));
          float2 tmpvar_26;
          tmpvar_26.x = tmpvar_3.x;
          tmpvar_26.y = (-tmpvar_3.y);
          float2 P_27;
          P_27 = (uv_2 + tmpvar_26);
          color_1 = (color_1 - tex2D(_MainTex, P_27));
          float2 tmpvar_28;
          tmpvar_28.y = 0;
          tmpvar_28.x = tmpvar_3.x;
          float2 P_29;
          P_29 = (uv_2 + tmpvar_28);
          color_1 = (color_1 - tex2D(_MainTex, P_29));
          float2 tmpvar_30;
          tmpvar_30.x = tmpvar_4.x;
          tmpvar_30.y = (-tmpvar_4.y);
          float2 P_31;
          P_31 = (uv_2 + tmpvar_30);
          color_1 = (color_1 - tex2D(_MainTex, P_31));
          float2 tmpvar_32;
          tmpvar_32.x = tmpvar_4.x;
          tmpvar_32.y = (-tmpvar_3.y);
          float2 P_33;
          P_33 = (uv_2 + tmpvar_32);
          color_1 = (color_1 - tex2D(_MainTex, P_33));
          float2 tmpvar_34;
          tmpvar_34.y = 0;
          tmpvar_34.x = tmpvar_4.x;
          float2 P_35;
          P_35 = (uv_2 + tmpvar_34);
          color_1 = (color_1 - tex2D(_MainTex, P_35));
          if(((((color_1.x + color_1.y) + color_1.z) / 3)<0.8))
          {
              color_1 = float4(0, 0, 0, 0);
          }
          out_f.color = color_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
