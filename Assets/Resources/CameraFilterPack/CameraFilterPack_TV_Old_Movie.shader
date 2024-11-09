Shader "CameraFilterPack/TV_Old_Movie"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(1, 10)) = 1
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
          float vI_1;
          float3 image_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float tmpvar_4;
          tmpvar_4 = float(int((_TimeX * 15)));
          float2 tmpvar_5;
          tmpvar_5.y = 1;
          tmpvar_5.x = tmpvar_4;
          float2 tmpvar_6;
          tmpvar_6.y = 1;
          tmpvar_6.x = (tmpvar_4 + 23);
          float2 tmpvar_7;
          tmpvar_7.x = frac((sin(dot(tmpvar_5, float2(12.9898, 78.233))) * 43758.55));
          tmpvar_7.y = frac((sin(dot(tmpvar_6, float2(12.9898, 78.233))) * 43758.55));
          float2 tmpvar_8;
          tmpvar_8 = (uv_3 + (0.002 * tmpvar_7));
          float3 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, tmpvar_8).xyz;
          image_2 = tmpvar_9;
          float3 tmpvar_10;
          tmpvar_10 = (dot(float3(0.2126, 0.7152, 0.0722), image_2) * float3(0.7, 0.7, 0.7));
          float2 tmpvar_11;
          tmpvar_11.y = 1;
          tmpvar_11.x = (tmpvar_4 + 8);
          float tmpvar_12;
          tmpvar_12 = frac((sin(dot(tmpvar_11, float2(12.9898, 78.233))) * 43758.55));
          vI_1 = ((16 * (((uv_3.x * (1 - uv_3.x)) * uv_3.y) * (1 - uv_3.y))) * lerp(0.7, 1, (tmpvar_12 + 0.5)));
          vI_1 = (vI_1 + (1 + (0.4 * tmpvar_12)));
          vI_1 = (vI_1 * pow(((((16 * uv_3.x) * (1 - uv_3.x)) * uv_3.y) * (1 - uv_3.y)), 0.4));
          int tmpvar_13;
          tmpvar_13 = int((8 * tmpvar_12));
          if((0<tmpvar_13))
          {
              float l_14;
              float2 tmpvar_15;
              tmpvar_15.y = 1;
              tmpvar_15.x = (7 + tmpvar_4);
              float tmpvar_16;
              tmpvar_16 = frac((sin(dot(tmpvar_15, float2(12.9898, 78.233))) * 43758.55));
              float tmpvar_17;
              tmpvar_17 = (0.01 * tmpvar_16);
              float tmpvar_18;
              tmpvar_18 = (tmpvar_16 - 0.5);
              if((tmpvar_16>0.2))
              {
                  l_14 = pow(abs((((tmpvar_16 * uv_3.x) + (tmpvar_17 * uv_3.y)) + tmpvar_18)), 0.125);
              }
              else
              {
                  l_14 = (2 - pow(abs((((tmpvar_16 * uv_3.x) + (tmpvar_17 * uv_3.y)) + tmpvar_18)), 0.125));
              }
              vI_1 = (vI_1 * lerp(0.5, 1, l_14));
          }
          if((1<tmpvar_13))
          {
              float l_19;
              float2 tmpvar_20;
              tmpvar_20.y = 1;
              tmpvar_20.x = (24 + tmpvar_4);
              float tmpvar_21;
              tmpvar_21 = frac((sin(dot(tmpvar_20, float2(12.9898, 78.233))) * 43758.55));
              float tmpvar_22;
              tmpvar_22 = (0.01 * tmpvar_21);
              float tmpvar_23;
              tmpvar_23 = (tmpvar_21 - 0.5);
              if((tmpvar_21>0.2))
              {
                  l_19 = pow(abs((((tmpvar_21 * uv_3.x) + (tmpvar_22 * uv_3.y)) + tmpvar_23)), 0.125);
              }
              else
              {
                  l_19 = (2 - pow(abs((((tmpvar_21 * uv_3.x) + (tmpvar_22 * uv_3.y)) + tmpvar_23)), 0.125));
              }
              vI_1 = (vI_1 * lerp(0.5, 1, l_19));
          }
          float2 tmpvar_24;
          tmpvar_24.y = 1;
          tmpvar_24.x = (tmpvar_4 + 18);
          int tmpvar_25;
          tmpvar_25 = int(max(((8 * frac((sin(dot(tmpvar_24, float2(12.9898, 78.233))) * 43758.55))) - 2), 0));
          if((0<tmpvar_25))
          {
              float seed_26;
              seed_26 = (tmpvar_4 + 6);
              float v_27;
              float2 tmpvar_28;
              tmpvar_28.y = 1;
              tmpvar_28.x = seed_26;
              float tmpvar_29;
              tmpvar_29 = frac((sin(dot(tmpvar_28, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_30;
              tmpvar_30.y = 1;
              tmpvar_30.x = (seed_26 + 1);
              float2 tmpvar_31;
              tmpvar_31.y = 1;
              tmpvar_31.x = (seed_26 + 2);
              float tmpvar_32;
              tmpvar_32 = (0.01 * frac((sin(dot(tmpvar_31, float2(12.9898, 78.233))) * 43758.55)));
              float2 tmpvar_33;
              tmpvar_33.x = tmpvar_29;
              tmpvar_33.y = frac((sin(dot(tmpvar_30, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_34;
              tmpvar_34 = (tmpvar_33 - uv_3);
              float y_over_x_35;
              y_over_x_35 = (tmpvar_34.y / tmpvar_34.x);
              float tmpvar_36;
              tmpvar_36 = (min(abs(y_over_x_35), 1) / max(abs(y_over_x_35), 1));
              float tmpvar_37;
              tmpvar_37 = (tmpvar_36 * tmpvar_36);
              tmpvar_37 = (((((((((((-0.01213232 * tmpvar_37) + 0.05368138) * tmpvar_37) - 0.1173503) * tmpvar_37) + 0.1938925) * tmpvar_37) - 0.3326756) * tmpvar_37) + 0.9999793) * tmpvar_36);
              tmpvar_37 = (tmpvar_37 + (float((abs(y_over_x_35)>1)) * ((tmpvar_37 * (-2)) + 1.570796)));
              v_27 = 1;
              float tmpvar_38;
              tmpvar_38 = ((tmpvar_32 * tmpvar_32) * ((sin(((6.2831 * (tmpvar_37 * sign(y_over_x_35))) * tmpvar_29)) * 0.1) + 1));
              float tmpvar_39;
              tmpvar_39 = dot(tmpvar_34, tmpvar_34);
              if((tmpvar_39<tmpvar_38))
              {
                  v_27 = 0.2;
              }
              else
              {
                  v_27 = pow((dot(tmpvar_34, tmpvar_34) - tmpvar_38), 0.0625);
              }
              vI_1 = (vI_1 * lerp((0.3 + (0.2 * (1 - (tmpvar_32 / 0.02)))), 1, v_27));
          }
          if((1<tmpvar_25))
          {
              float seed_40;
              seed_40 = (25 + tmpvar_4);
              float v_41;
              float2 tmpvar_42;
              tmpvar_42.y = 1;
              tmpvar_42.x = seed_40;
              float tmpvar_43;
              tmpvar_43 = frac((sin(dot(tmpvar_42, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_44;
              tmpvar_44.y = 1;
              tmpvar_44.x = (seed_40 + 1);
              float2 tmpvar_45;
              tmpvar_45.y = 1;
              tmpvar_45.x = (seed_40 + 2);
              float tmpvar_46;
              tmpvar_46 = (0.01 * frac((sin(dot(tmpvar_45, float2(12.9898, 78.233))) * 43758.55)));
              float2 tmpvar_47;
              tmpvar_47.x = tmpvar_43;
              tmpvar_47.y = frac((sin(dot(tmpvar_44, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_48;
              tmpvar_48 = (tmpvar_47 - uv_3);
              float y_over_x_49;
              y_over_x_49 = (tmpvar_48.y / tmpvar_48.x);
              float tmpvar_50;
              tmpvar_50 = (min(abs(y_over_x_49), 1) / max(abs(y_over_x_49), 1));
              float tmpvar_51;
              tmpvar_51 = (tmpvar_50 * tmpvar_50);
              tmpvar_51 = (((((((((((-0.01213232 * tmpvar_51) + 0.05368138) * tmpvar_51) - 0.1173503) * tmpvar_51) + 0.1938925) * tmpvar_51) - 0.3326756) * tmpvar_51) + 0.9999793) * tmpvar_50);
              tmpvar_51 = (tmpvar_51 + (float((abs(y_over_x_49)>1)) * ((tmpvar_51 * (-2)) + 1.570796)));
              v_41 = 1;
              float tmpvar_52;
              tmpvar_52 = ((tmpvar_46 * tmpvar_46) * ((sin(((6.2831 * (tmpvar_51 * sign(y_over_x_49))) * tmpvar_43)) * 0.1) + 1));
              float tmpvar_53;
              tmpvar_53 = dot(tmpvar_48, tmpvar_48);
              if((tmpvar_53<tmpvar_52))
              {
                  v_41 = 0.2;
              }
              else
              {
                  v_41 = pow((dot(tmpvar_48, tmpvar_48) - tmpvar_52), 0.0625);
              }
              vI_1 = (vI_1 * lerp((0.3 + (0.2 * (1 - (tmpvar_46 / 0.02)))), 1, v_41));
          }
          float4 tmpvar_54;
          tmpvar_54.w = 1;
          tmpvar_54.xyz = float3((tmpvar_10 * vI_1));
          out_f.color = tmpvar_54;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
