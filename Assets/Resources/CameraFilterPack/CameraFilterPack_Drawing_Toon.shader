Shader "CameraFilterPack/Drawing_Toon"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _DotSize ("_DotSize", Range(0, 1)) = 0
    _ColorLevel ("_ColorLevel", Range(0, 10)) = 7
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
      uniform float _Distortion;
      uniform float _DotSize;
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
          float3 color_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_3;
          tmpvar_3 = (normalize(tex2D(_MainTex, uv_2)).xyz * 2.5);
          float3 color_4;
          color_4 = tmpvar_3;
          color_4 = (floor((pow(color_4, float3(0.65, 0.65, 0.65)) * 5)) / 5);
          float3 tmpvar_5;
          tmpvar_5 = pow(color_4, float3(1.538462, 1.538462, 1.538462));
          color_4 = tmpvar_5;
          color_1 = (tmpvar_5 * _Distortion);
          float2 uv_6;
          uv_6 = uv_2;
          float3 col_7;
          float tmpvar_8;
          tmpvar_8 = (0.001953125 * _DotSize);
          float2 tmpvar_9;
          tmpvar_9.x = (-tmpvar_8);
          tmpvar_9.y = (-tmpvar_8);
          float4 tmpvar_10;
          float2 P_11;
          P_11 = (uv_6 + tmpvar_9);
          tmpvar_10 = tex2D(_MainTex, P_11);
          float tmpvar_12;
          tmpvar_12 = dot(tmpvar_10, float4(0.1125, 0.22125, 0.04125, 0.25));
          float2 tmpvar_13;
          tmpvar_13.x = tmpvar_8;
          tmpvar_13.y = (-tmpvar_8);
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_6 + tmpvar_13);
          tmpvar_14 = tex2D(_MainTex, P_15);
          float tmpvar_16;
          tmpvar_16 = dot(tmpvar_14, float4(0.1125, 0.22125, 0.04125, 0.25));
          float2 tmpvar_17;
          tmpvar_17.y = 0;
          tmpvar_17.x = (-tmpvar_8);
          float4 tmpvar_18;
          float2 P_19;
          P_19 = (uv_6 + tmpvar_17);
          tmpvar_18 = tex2D(_MainTex, P_19);
          float tmpvar_20;
          tmpvar_20 = dot(tmpvar_18, float4(0.1125, 0.22125, 0.04125, 0.25));
          float2 tmpvar_21;
          tmpvar_21.x = (-tmpvar_8);
          tmpvar_21.y = tmpvar_8;
          float4 tmpvar_22;
          float2 P_23;
          P_23 = (uv_6 + tmpvar_21);
          tmpvar_22 = tex2D(_MainTex, P_23);
          float tmpvar_24;
          tmpvar_24 = dot(tmpvar_22, float4(0.1125, 0.22125, 0.04125, 0.25));
          float2 tmpvar_25;
          tmpvar_25.x = 0;
          tmpvar_25.y = tmpvar_8;
          float4 tmpvar_26;
          float2 P_27;
          P_27 = (uv_6 + tmpvar_25);
          tmpvar_26 = tex2D(_MainTex, P_27);
          float4 tmpvar_28;
          float2 P_29;
          P_29 = (uv_6 + float2(tmpvar_8, tmpvar_8));
          tmpvar_28 = tex2D(_MainTex, P_29);
          float tmpvar_30;
          tmpvar_30 = dot(tmpvar_28, float4(0.1125, 0.22125, 0.04125, 0.25));
          float tmpvar_31;
          tmpvar_31 = (((((tmpvar_16 + tmpvar_30) + (2 * tmpvar_20)) - tmpvar_12) - (2 * tmpvar_20)) - tmpvar_24);
          float tmpvar_32;
          tmpvar_32 = (((((tmpvar_24 + (2 * dot(tmpvar_26, float4(0.1125, 0.22125, 0.04125, 0.25)))) + tmpvar_30) - tmpvar_12) - (2 * tmpvar_12)) - tmpvar_16);
          if((((tmpvar_31 * tmpvar_31) + (tmpvar_32 * tmpvar_32))>0.04))
          {
              col_7 = float3(-1, (-1), (-1));
          }
          else
          {
              col_7 = float3(0, 0, 0);
          }
          color_1 = (color_1 + col_7);
          float3 color_33;
          color_33 = color_1;
          if((color_33.y>((color_33.x + color_33.z) * 12.8)))
          {
              color_33 = float3(0, 0, 0);
          }
          color_33 = (((0.2126 * color_33.xxx) + (0.7152 * color_33.yyy)) + (0.0722 * color_33.zzz));
          if((color_33.x<=0.95))
          {
              if((color_33.x>0.75))
              {
                  color_33.x = (color_33.x * 0.9);
              }
              else
              {
                  if((color_33.x>0.5))
                  {
                      color_33.x = (color_33.x * 0.7);
                      color_33.y = (color_33.y * 0.9);
                  }
                  else
                  {
                      if((color_33.x>0.25))
                      {
                          color_33.x = (color_33.x * 0.5);
                          color_33.y = (color_33.y * 0.75);
                      }
                      else
                      {
                          color_33.x = (color_33.x * 0.25);
                          color_33.y = (color_33.y * 0.5);
                      }
                  }
              }
          }
          color_1 = color_33;
          float4 tmpvar_34;
          tmpvar_34.w = 1;
          tmpvar_34.xyz = float3(color_1);
          out_f.color = tmpvar_34;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
