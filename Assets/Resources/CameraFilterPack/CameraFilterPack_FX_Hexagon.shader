Shader "CameraFilterPack/FX_Hexagon"
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
          float2 coord_1;
          coord_1 = in_f.xlv_TEXCOORD0;
          float2 r_2;
          float tmpvar_3;
          tmpvar_3 = floor((coord_1.x / 0.027714));
          float tmpvar_4;
          tmpvar_4 = (tmpvar_3 / 2);
          float tmpvar_5;
          tmpvar_5 = (frac(abs(tmpvar_4)) * 2);
          float tmpvar_6;
          if((tmpvar_4>=0))
          {
              tmpvar_6 = tmpvar_5;
          }
          else
          {
              tmpvar_6 = (-tmpvar_5);
          }
          float tmpvar_7;
          tmpvar_7 = (coord_1.y - (tmpvar_6 * 0.016));
          float tmpvar_8;
          tmpvar_8 = floor((31.25 * tmpvar_7));
          float tmpvar_9;
          tmpvar_9 = (coord_1.x - (tmpvar_3 * 0.027714));
          float tmpvar_10;
          tmpvar_10 = (tmpvar_7 - (tmpvar_8 * 0.032));
          float tmpvar_11;
          if((tmpvar_10>0.016))
          {
              tmpvar_11 = 1;
          }
          else
          {
              tmpvar_11 = 0;
          }
          float tmpvar_12;
          tmpvar_12 = (0.01847609 * abs((0.5 - (tmpvar_10 / 0.032))));
          if((tmpvar_9>tmpvar_12))
          {
              r_2.x = tmpvar_3;
              r_2.y = tmpvar_8;
          }
          else
          {
              r_2.x = (tmpvar_3 - 1);
              float tmpvar_13;
              tmpvar_13 = (r_2.x / 2);
              float tmpvar_14;
              tmpvar_14 = (frac(abs(tmpvar_13)) * 2);
              float tmpvar_15;
              if((tmpvar_13>=0))
              {
                  tmpvar_15 = tmpvar_14;
              }
              else
              {
                  tmpvar_15 = (-tmpvar_14);
              }
              r_2.y = ((tmpvar_8 - tmpvar_15) + tmpvar_11);
          }
          float2 r_16;
          r_16.x = (r_2.x * 0.02771281);
          float tmpvar_17;
          tmpvar_17 = (r_2.x / 2);
          float tmpvar_18;
          tmpvar_18 = (frac(abs(tmpvar_17)) * 2);
          float tmpvar_19;
          if((tmpvar_17>=0))
          {
              tmpvar_19 = tmpvar_18;
          }
          else
          {
              tmpvar_19 = (-tmpvar_18);
          }
          r_16.y = ((r_2.y * 0.032) + ((tmpvar_19 * 0.032) / 2));
          float4 tmpvar_20;
          tmpvar_20 = tex2D(_MainTex, r_16);
          out_f.color = tmpvar_20;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
