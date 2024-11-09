Shader "CameraFilterPack/TV_Old_Movie_2"
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
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
      uniform float _Value4;
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
          float3 oldImage_2;
          float3 image_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_5;
          tmpvar_5 = float(int((_TimeX * _Value)));
          float2 tmpvar_6;
          tmpvar_6.y = 1;
          tmpvar_6.x = tmpvar_5;
          float2 tmpvar_7;
          tmpvar_7.y = 1;
          tmpvar_7.x = (tmpvar_5 + 23);
          float2 tmpvar_8;
          tmpvar_8.x = frac((sin(dot(tmpvar_6, float2(12.9898, 78.233))) * 43758.55));
          tmpvar_8.y = frac((sin(dot(tmpvar_7, float2(12.9898, 78.233))) * 43758.55));
          float2 tmpvar_9;
          tmpvar_9 = (uv_4 + (0.002 * tmpvar_8));
          float3 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9).xyz;
          image_3 = tmpvar_10;
          float3 tmpvar_11;
          tmpvar_11.z = 0.7;
          float tmpvar_12;
          tmpvar_12 = (0.7 + _Value3);
          tmpvar_11.x = tmpvar_12;
          tmpvar_11.y = (0.7 + (_Value3 / 2));
          float3 tmpvar_13;
          tmpvar_13.z = 0.7;
          tmpvar_13.x = tmpvar_12;
          tmpvar_13.y = (0.7 + (_Value3 / 8));
          oldImage_2 = ((((dot(float3(0.2126, 0.7152, 0.0722), image_3) * tmpvar_11) * _Value2) * tmpvar_13) * _Value2);
          float2 tmpvar_14;
          tmpvar_14.y = 1;
          tmpvar_14.x = (tmpvar_5 + 8);
          float tmpvar_15;
          tmpvar_15 = frac((sin(dot(tmpvar_14, float2(12.9898, 78.233))) * 43758.55));
          vI_1 = ((16 * (((uv_4.x * (1 - uv_4.x)) * uv_4.y) * (1 - uv_4.y))) * lerp(0.7, 1, (tmpvar_15 + 0.5)));
          vI_1 = (vI_1 + (1 + (0.4 * tmpvar_15)));
          vI_1 = (vI_1 * pow(((((16 * uv_4.x) * (1 - uv_4.x)) * uv_4.y) * (1 - uv_4.y)), 0.4));
          int tmpvar_16;
          tmpvar_16 = int((8 * tmpvar_15));
          if((0<tmpvar_16))
          {
              float l_17;
              float2 tmpvar_18;
              tmpvar_18.y = 1;
              tmpvar_18.x = (7 + tmpvar_5);
              float tmpvar_19;
              tmpvar_19 = frac((sin(dot(tmpvar_18, float2(12.9898, 78.233))) * 43758.55));
              float tmpvar_20;
              tmpvar_20 = (0.01 * tmpvar_19);
              float tmpvar_21;
              tmpvar_21 = (tmpvar_19 - 0.5);
              if((tmpvar_19>0.2))
              {
                  l_17 = pow(abs((((tmpvar_19 * uv_4.x) + (tmpvar_20 * uv_4.y)) + tmpvar_21)), 0.125);
              }
              else
              {
                  l_17 = (2 - pow(abs((((tmpvar_19 * uv_4.x) + (tmpvar_20 * uv_4.y)) + tmpvar_21)), 0.125));
              }
              vI_1 = (vI_1 * lerp((0.5 - _Value4), 1, l_17));
          }
          if((1<tmpvar_16))
          {
              float l_22;
              float2 tmpvar_23;
              tmpvar_23.y = 1;
              tmpvar_23.x = (24 + tmpvar_5);
              float tmpvar_24;
              tmpvar_24 = frac((sin(dot(tmpvar_23, float2(12.9898, 78.233))) * 43758.55));
              float tmpvar_25;
              tmpvar_25 = (0.01 * tmpvar_24);
              float tmpvar_26;
              tmpvar_26 = (tmpvar_24 - 0.5);
              if((tmpvar_24>0.2))
              {
                  l_22 = pow(abs((((tmpvar_24 * uv_4.x) + (tmpvar_25 * uv_4.y)) + tmpvar_26)), 0.125);
              }
              else
              {
                  l_22 = (2 - pow(abs((((tmpvar_24 * uv_4.x) + (tmpvar_25 * uv_4.y)) + tmpvar_26)), 0.125));
              }
              vI_1 = (vI_1 * lerp((0.5 - _Value4), 1, l_22));
          }
          float2 tmpvar_27;
          tmpvar_27.y = 1;
          tmpvar_27.x = (tmpvar_5 + 18);
          int tmpvar_28;
          tmpvar_28 = int(max(((8 * frac((sin(dot(tmpvar_27, float2(12.9898, 78.233))) * 43758.55))) - 2), 0));
          if((0<tmpvar_28))
          {
              float seed_29;
              seed_29 = (tmpvar_5 + 6);
              float v_30;
              float2 tmpvar_31;
              tmpvar_31.y = 1;
              tmpvar_31.x = seed_29;
              float tmpvar_32;
              tmpvar_32 = frac((sin(dot(tmpvar_31, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_33;
              tmpvar_33.y = 1;
              tmpvar_33.x = (seed_29 + 1);
              float2 tmpvar_34;
              tmpvar_34.y = 1;
              tmpvar_34.x = (seed_29 + 2);
              float tmpvar_35;
              tmpvar_35 = (0.01 * frac((sin(dot(tmpvar_34, float2(12.9898, 78.233))) * 43758.55)));
              float2 tmpvar_36;
              tmpvar_36.x = tmpvar_32;
              tmpvar_36.y = frac((sin(dot(tmpvar_33, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_37;
              tmpvar_37 = (tmpvar_36 - uv_4);
              float y_over_x_38;
              y_over_x_38 = (tmpvar_37.y / tmpvar_37.x);
              float tmpvar_39;
              tmpvar_39 = (min(abs(y_over_x_38), 1) / max(abs(y_over_x_38), 1));
              float tmpvar_40;
              tmpvar_40 = (tmpvar_39 * tmpvar_39);
              tmpvar_40 = (((((((((((-0.01213232 * tmpvar_40) + 0.05368138) * tmpvar_40) - 0.1173503) * tmpvar_40) + 0.1938925) * tmpvar_40) - 0.3326756) * tmpvar_40) + 0.9999793) * tmpvar_39);
              tmpvar_40 = (tmpvar_40 + (float((abs(y_over_x_38)>1)) * ((tmpvar_40 * (-2)) + 1.570796)));
              v_30 = 1;
              float tmpvar_41;
              tmpvar_41 = ((tmpvar_35 * tmpvar_35) * ((sin(((6.2831 * (tmpvar_40 * sign(y_over_x_38))) * tmpvar_32)) * 0.1) + 1));
              float tmpvar_42;
              tmpvar_42 = dot(tmpvar_37, tmpvar_37);
              if((tmpvar_42<tmpvar_41))
              {
                  v_30 = 0.2;
              }
              else
              {
                  v_30 = pow((dot(tmpvar_37, tmpvar_37) - tmpvar_41), 0.0625);
              }
              vI_1 = (vI_1 * lerp(((0.3 + (0.2 * (1 - (tmpvar_35 / 0.02)))) - _Value4), 1, v_30));
          }
          if((1<tmpvar_28))
          {
              float seed_43;
              seed_43 = (25 + tmpvar_5);
              float v_44;
              float2 tmpvar_45;
              tmpvar_45.y = 1;
              tmpvar_45.x = seed_43;
              float tmpvar_46;
              tmpvar_46 = frac((sin(dot(tmpvar_45, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_47;
              tmpvar_47.y = 1;
              tmpvar_47.x = (seed_43 + 1);
              float2 tmpvar_48;
              tmpvar_48.y = 1;
              tmpvar_48.x = (seed_43 + 2);
              float tmpvar_49;
              tmpvar_49 = (0.01 * frac((sin(dot(tmpvar_48, float2(12.9898, 78.233))) * 43758.55)));
              float2 tmpvar_50;
              tmpvar_50.x = tmpvar_46;
              tmpvar_50.y = frac((sin(dot(tmpvar_47, float2(12.9898, 78.233))) * 43758.55));
              float2 tmpvar_51;
              tmpvar_51 = (tmpvar_50 - uv_4);
              float y_over_x_52;
              y_over_x_52 = (tmpvar_51.y / tmpvar_51.x);
              float tmpvar_53;
              tmpvar_53 = (min(abs(y_over_x_52), 1) / max(abs(y_over_x_52), 1));
              float tmpvar_54;
              tmpvar_54 = (tmpvar_53 * tmpvar_53);
              tmpvar_54 = (((((((((((-0.01213232 * tmpvar_54) + 0.05368138) * tmpvar_54) - 0.1173503) * tmpvar_54) + 0.1938925) * tmpvar_54) - 0.3326756) * tmpvar_54) + 0.9999793) * tmpvar_53);
              tmpvar_54 = (tmpvar_54 + (float((abs(y_over_x_52)>1)) * ((tmpvar_54 * (-2)) + 1.570796)));
              v_44 = 1;
              float tmpvar_55;
              tmpvar_55 = ((tmpvar_49 * tmpvar_49) * ((sin(((6.2831 * (tmpvar_54 * sign(y_over_x_52))) * tmpvar_46)) * 0.1) + 1));
              float tmpvar_56;
              tmpvar_56 = dot(tmpvar_51, tmpvar_51);
              if((tmpvar_56<tmpvar_55))
              {
                  v_44 = 0.2;
              }
              else
              {
                  v_44 = pow((dot(tmpvar_51, tmpvar_51) - tmpvar_55), 0.0625);
              }
              vI_1 = (vI_1 * lerp(((0.3 + (0.2 * (1 - (tmpvar_49 / 0.02)))) - _Value4), 1, v_44));
          }
          float4 tmpvar_57;
          tmpvar_57.w = 1;
          tmpvar_57.xyz = float3((oldImage_2 * vI_1));
          out_f.color = tmpvar_57;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
