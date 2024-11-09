Shader "CameraFilterPack/Drawing_Curve"
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
          float4 color_2;
          float3 datosPatron_3[6];
          float grosorInicial_4;
          float divisor_5;
          float gris_6;
          float2 xy_7;
          xy_7 = in_f.xlv_TEXCOORD0;
          gris_6 = 1;
          float tmpvar_8;
          tmpvar_8 = (_Value / 512);
          divisor_5 = tmpvar_8;
          grosorInicial_4 = (tmpvar_8 * 0.2);
          datosPatron_3[0] = float3(-0.7071, 0.7071, 3);
          datosPatron_3[1] = float3(0, 1, 0.6);
          datosPatron_3[2] = float3(0, 1, 0.5);
          datosPatron_3[3] = float3(1, 0, 0.4);
          datosPatron_3[4] = float3(1, 0, 0.3);
          datosPatron_3[5] = float3(0, 1, 0.2);
          float2 tmpvar_9;
          tmpvar_9.x = in_f.xlv_TEXCOORD0.x;
          tmpvar_9.y = xy_7.y;
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9);
          color_2 = tmpvar_10;
          int i_1_1 = 0;
          while((i_1_1<6))
          {
              float tmpvar_11;
              tmpvar_11 = datosPatron_3[i_1_1].x;
              float tmpvar_12;
              tmpvar_12 = datosPatron_3[i_1_1].y;
              float2 tmpvar_13;
              tmpvar_13.x = ((xy_7.x * tmpvar_11) - (xy_7.y * tmpvar_12));
              tmpvar_13.y = ((xy_7.x * tmpvar_12) + (xy_7.y * tmpvar_11));
              float tmpvar_14;
              tmpvar_14 = (grosorInicial_4 * float((i_1_1 + 1)));
              float tmpvar_15;
              tmpvar_15 = (((tmpvar_13.y + (tmpvar_14 * 0.5)) - (sin((tmpvar_13.x * 10)) * 0.03)) / divisor_5);
              float tmpvar_16;
              tmpvar_16 = (frac(abs(tmpvar_15)) * divisor_5);
              float tmpvar_17;
              if((tmpvar_15>=0))
              {
                  tmpvar_17 = tmpvar_16;
              }
              else
              {
                  tmpvar_17 = (-tmpvar_16);
              }
              float tmpvar_18;
              tmpvar_18 = (((0.3 * color_2.x) + (0.4 * color_2.y)) + (0.3 * color_2.z));
              if(((tmpvar_17<tmpvar_14) && (tmpvar_18<(0.75 - (0.12 * float(i_1_1))))))
              {
                  float tmpvar_19;
                  tmpvar_19 = datosPatron_3[i_1_1].z;
                  gris_6 = min((abs(((((tmpvar_14 - tmpvar_17) / tmpvar_14) - 0.5) / tmpvar_19)) - ((0.5 - tmpvar_19) / tmpvar_19)), gris_6);
              }
              i_1_1 = (i_1_1 + 1);
          }
          float4 tmpvar_20;
          tmpvar_20.w = 1;
          tmpvar_20.x = gris_6;
          tmpvar_20.y = gris_6;
          tmpvar_20.z = gris_6;
          out_f.color = tmpvar_20;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
