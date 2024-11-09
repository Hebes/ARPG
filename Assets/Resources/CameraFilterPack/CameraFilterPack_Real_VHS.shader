Shader "CameraFilterPack/Real_VHS"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    VHS ("Base (RGB)", 2D) = "white" {}
    VHS2 ("Base (RGB)", 2D) = "white" {}
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform sampler2D VHS;
      uniform sampler2D VHS2;
      uniform float TRACKING;
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
          float tm_1;
          float wave_2;
          float3 col_3;
          float uvx_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          if((uv_5.y<0.025))
          {
              uv_5.x = (uv_5.x + ((uv_5.y - 0.05) * sin(((uv_5.y * 512) + (_Time * 12)))).x);
          }
          if((uv_5.y<0.015))
          {
              uv_5.x = (uv_5.x + ((uv_5.y - 0.05) * sin(((uv_5.y * 512) + (_Time * 64)))).x);
          }
          float tmpvar_6;
          tmpvar_6 = (sin(((uv_5.y * 150) + (_Time * 48))) / 64).x;
          float tmpvar_7;
          float _tmp_dvx_97 = (_Time.x * 20);
          tmpvar_7 = (dot(float2(_tmp_dvx_97, _tmp_dvx_97), float2(12.9898, 78.233)) / 3.14);
          float tmpvar_8;
          tmpvar_8 = (frac(abs(tmpvar_7)) * 3.14);
          float tmpvar_9;
          if((tmpvar_7>=0))
          {
              tmpvar_9 = tmpvar_8;
          }
          else
          {
              tmpvar_9 = (-tmpvar_8);
          }
          float tmpvar_10;
          tmpvar_10 = (frac((sin(tmpvar_9) * 43758.55)) * 15);
          float tmpvar_11;
          tmpvar_11 = clamp(((uv_5.y - tmpvar_10) / ((tmpvar_10 + 0.03) - tmpvar_10)), 0, 1);
          float edge0_12;
          edge0_12 = (tmpvar_10 + 0.06);
          float tmpvar_13;
          tmpvar_13 = clamp(((uv_5.y - edge0_12) / ((tmpvar_10 + 0.09) - edge0_12)), 0, 1);
          uv_5.x = (uv_5.x + (tmpvar_6 * ((tmpvar_11 * (tmpvar_11 * (3 - (2 * tmpvar_11)))) - (tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13)))))));
          uvx_4 = 0;
          float tmpvar_14;
          tmpvar_14 = (floor((uv_5.y * 288)) / 288);
          float2 tmpvar_15;
          tmpvar_15.x = (_Time.x * 0.013);
          tmpvar_15.y = (tmpvar_14 * 0.42);
          float tmpvar_16;
          tmpvar_16 = (dot(tmpvar_15, float2(12.9898, 78.233)) / 3.14);
          float tmpvar_17;
          tmpvar_17 = (frac(abs(tmpvar_16)) * 3.14);
          float tmpvar_18;
          if((tmpvar_16>=0))
          {
              tmpvar_18 = tmpvar_17;
          }
          else
          {
              tmpvar_18 = (-tmpvar_17);
          }
          uvx_4 = (frac((sin(tmpvar_18) * 43758.55)) * 0.004);
          float2 tmpvar_19;
          tmpvar_19.x = (_Time.x * 0.4);
          tmpvar_19.y = tmpvar_14;
          float tmpvar_20;
          tmpvar_20 = (dot(tmpvar_19, float2(12.9898, 78.233)) / 3.14);
          float tmpvar_21;
          tmpvar_21 = (frac(abs(tmpvar_20)) * 3.14);
          float tmpvar_22;
          if((tmpvar_20>=0))
          {
              tmpvar_22 = tmpvar_21;
          }
          else
          {
              tmpvar_22 = (-tmpvar_21);
          }
          uvx_4 = (uvx_4 + (sin(frac((sin(tmpvar_22) * 43758.55))) * 0.005));
          uv_5.x = (uv_5.x + (uvx_4 * (1 - uv_5.x)));
          float3 tmpvar_23;
          tmpvar_23 = tex2D(_MainTex, uv_5).xyz;
          col_3 = tmpvar_23;
          float3 tmpvar_24;
          tmpvar_24 = clamp(col_3, float3(0.08, 0.08, 0.08), float3(0.95, 0.95, 0.95));
          float3 tmpvar_25;
          tmpvar_25.x = (((0.299 * tmpvar_24.x) + (0.587 * tmpvar_24.y)) + (0.114 * tmpvar_24.z));
          tmpvar_25.y = (((-0.147 * tmpvar_24.x) - (0.289 * tmpvar_24.y)) + (0.436 * tmpvar_24.z));
          tmpvar_25.z = (((0.615 * tmpvar_24.x) - (0.515 * tmpvar_24.y)) - (0.1 * tmpvar_24.z));
          float tmpvar_26;
          float4 tmpvar_27;
          tmpvar_27 = (_Time * 128);
          tmpvar_26 = (sin(tmpvar_27) / 128).x;
          uv_5.x = (floor((uv_5.x * 52)) / 52);
          uv_5.y = (floor((uv_5.y * 288)) / 288);
          float2 tmpvar_28;
          tmpvar_28.x = (-0.01 + tmpvar_26);
          tmpvar_28.y = (cos(tmpvar_27) / 128).x;
          float4 tmpvar_29;
          float2 P_30;
          P_30 = (uv_5 + (tmpvar_28 * tmpvar_26));
          tmpvar_29 = tex2D(_MainTex, P_30);
          col_3 = tmpvar_29.xyz;
          float3 tmpvar_31;
          tmpvar_31.x = (((0.299 * col_3.x) + (0.587 * col_3.y)) + (0.114 * col_3.z));
          tmpvar_31.y = (((-0.147 * col_3.x) - (0.289 * col_3.y)) + (0.436 * col_3.z));
          tmpvar_31.z = (((0.615 * col_3.x) - (0.515 * col_3.y)) - (0.1 * col_3.z));
          float3 tmpvar_32;
          tmpvar_32 = (tmpvar_31 / 2);
          wave_2 = (max(sin(((uv_5.y * 24) + (_Time * 64))), float4(0, 0, 0, 0)).x + max(sin(((uv_5.y * 14) + (_Time * 16))), float4(0, 0, 0, 0)).x);
          wave_2 = (wave_2 / 2);
          float U_33;
          U_33 = (tmpvar_32.y * (wave_2 + 0.5));
          float V_34;
          V_34 = (tmpvar_32.z * (wave_2 + 0.5));
          float3 tmpvar_35;
          tmpvar_35.x = (tmpvar_25.x + (1.14 * V_34));
          tmpvar_35.y = ((tmpvar_25.x - (0.395 * U_33)) - (0.581 * V_34));
          tmpvar_35.z = (tmpvar_25.x + (2.032 * U_33));
          col_3 = (clamp(tmpvar_35, float3(0.08, 0.08, 0.08), float3(0.95, 0.95, 0.95)) * 1.05);
          uv_5 = (in_f.xlv_TEXCOORD0 / 8);
          float tmpvar_36;
          tmpvar_36 = (_Time * 30).x;
          tm_1 = tmpvar_36;
          float tmpvar_37;
          tmpvar_37 = frac(abs(tmpvar_36));
          float tmpvar_38;
          if((tmpvar_36>=0))
          {
              tmpvar_38 = tmpvar_37;
          }
          else
          {
              tmpvar_38 = (-tmpvar_37);
          }
          uv_5.x = (uv_5.x + (floor((tmpvar_38 * 8)) / 8));
          float tmpvar_39;
          tmpvar_39 = (tmpvar_36 / 8);
          float tmpvar_40;
          tmpvar_40 = frac(abs(tmpvar_39));
          float tmpvar_41;
          if((tmpvar_39>=0))
          {
              tmpvar_41 = tmpvar_40;
          }
          else
          {
              tmpvar_41 = (-tmpvar_40);
          }
          uv_5.y = (uv_5.y + (1 - (floor((tmpvar_41 * 8)) / 8)));
          float4 tmpvar_42;
          tmpvar_42 = tex2D(VHS, uv_5);
          float3 d_43;
          d_43 = tmpvar_42.xyz;
          float3 c_44;
          float tmpvar_45;
          if((col_3.x<0.5))
          {
              tmpvar_45 = ((2 * col_3.x) * d_43.x);
          }
          else
          {
              tmpvar_45 = (1 - ((2 * (1 - col_3.x)) * (1 - d_43.x)));
          }
          c_44.x = tmpvar_45;
          float tmpvar_46;
          if((col_3.y<0.5))
          {
              tmpvar_46 = ((2 * col_3.y) * d_43.y);
          }
          else
          {
              tmpvar_46 = (1 - ((2 * (1 - col_3.y)) * (1 - d_43.y)));
          }
          c_44.y = tmpvar_46;
          float tmpvar_47;
          if((col_3.z<0.5))
          {
              tmpvar_47 = ((2 * col_3.z) * d_43.z);
          }
          else
          {
              tmpvar_47 = (1 - ((2 * (1 - col_3.z)) * (1 - d_43.z)));
          }
          c_44.z = tmpvar_47;
          col_3 = c_44;
          uv_5 = (in_f.xlv_TEXCOORD0 / 8);
          uv_5.y = (1 - uv_5.y);
          tm_1 = (_Time * 30).x;
          float tmpvar_48;
          tmpvar_48 = frac(abs(tm_1));
          float tmpvar_49;
          if((tm_1>=0))
          {
              tmpvar_49 = tmpvar_48;
          }
          else
          {
              tmpvar_49 = (-tmpvar_48);
          }
          uv_5.x = (uv_5.x + (floor((tmpvar_49 * 8)) / 8));
          float tmpvar_50;
          tmpvar_50 = (tm_1 / 8);
          float tmpvar_51;
          tmpvar_51 = frac(abs(tmpvar_50));
          float tmpvar_52;
          if((tmpvar_50>=0))
          {
              tmpvar_52 = tmpvar_51;
          }
          else
          {
              tmpvar_52 = (-tmpvar_51);
          }
          uv_5.y = (uv_5.y + (1 - (floor((tmpvar_52 * 8)) / 8)));
          float4 tmpvar_53;
          tmpvar_53 = tex2D(VHS2, uv_5);
          uv_5 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_54;
          float _tmp_dvx_98 = (TRACKING * (1 - uv_5.y));
          tmpvar_54 = lerp(c_44, (c_44 + tmpvar_53.xyz), float3(_tmp_dvx_98, _tmp_dvx_98, _tmp_dvx_98));
          col_3 = tmpvar_54;
          float4 tmpvar_55;
          tmpvar_55.w = 1;
          tmpvar_55.xyz = float3(tmpvar_54);
          out_f.color = tmpvar_55;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
