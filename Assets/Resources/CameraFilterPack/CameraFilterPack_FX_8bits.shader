Shader "CameraFilterPack/FX_8bits"
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
      uniform float _Distortion;
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
          float best1_1;
          float best0_2;
          float3 dst1_3;
          float3 dst0_4;
          float3 src_5;
          float2 q_6;
          q_6 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex, q_6).xyz;
          src_5 = tmpvar_7;
          dst0_4 = float3(0, 0, 0);
          dst1_3 = float3(0, 0, 0);
          best0_2 = 1000;
          best1_1 = 1000;
          src_5 = (src_5 + _Distortion);
          float3 tmpvar_8;
          tmpvar_8 = (src_5 * (src_5 * src_5));
          float tmpvar_9;
          tmpvar_9 = dot(tmpvar_8, tmpvar_8);
          if((tmpvar_9<1000))
          {
              best1_1 = 1000;
              dst1_3 = float3(0, 0, 0);
              best0_2 = tmpvar_9;
              dst0_4 = float3(0, 0, 0);
          }
          float3 tmpvar_10;
          tmpvar_10 = ((src_5 * (src_5 * src_5)) - float3(0.248747, 0.0272115, 0.01792688));
          float tmpvar_11;
          tmpvar_11 = dot(tmpvar_10, tmpvar_10);
          if((tmpvar_11<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_11;
              dst0_4 = float3(0.6289063, 0.3007813, 0.2617188);
          }
          float3 tmpvar_12;
          tmpvar_12 = ((src_5 * (src_5 * src_5)) - float3(0.07099009, 0.4285013, 0.4768372));
          float tmpvar_13;
          tmpvar_13 = dot(tmpvar_12, tmpvar_12);
          if((tmpvar_13<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_13;
              dst0_4 = float3(0.4140625, 0.7539063, 0.78125);
          }
          float3 tmpvar_14;
          tmpvar_14 = ((src_5 * (src_5 * src_5)) - float3(0.2534108, 0.03924986, 0.2677516));
          float tmpvar_15;
          tmpvar_15 = dot(tmpvar_14, tmpvar_14);
          if((tmpvar_15<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_15;
              dst0_4 = float3(0.6328125, 0.3398438, 0.6445313);
          }
          float3 tmpvar_16;
          tmpvar_16 = ((src_5 * (src_5 * src_5)) - float3(0.04641342, 0.3086161, 0.05110356));
          float tmpvar_17;
          tmpvar_17 = dot(tmpvar_16, tmpvar_16);
          if((tmpvar_17<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_17;
              dst0_4 = float3(0.359375, 0.6757813, 0.3710938);
          }
          float3 tmpvar_18;
          tmpvar_18 = ((src_5 * (src_5 * src_5)) - float3(0.02938743, 0.01874161, 0.226284));
          float tmpvar_19;
          tmpvar_19 = dot(tmpvar_18, tmpvar_18);
          if((tmpvar_19<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_19;
              dst0_4 = float3(0.3085938, 0.265625, 0.609375);
          }
          float3 tmpvar_20;
          tmpvar_20 = ((src_5 * (src_5 * src_5)) - float3(0.4986184, 0.584146, 0.1532646));
          float tmpvar_21;
          tmpvar_21 = dot(tmpvar_20, tmpvar_20);
          if((tmpvar_21<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_21;
              dst0_4 = float3(0.7929688, 0.8359375, 0.5351563);
          }
          float3 tmpvar_22;
          tmpvar_22 = ((src_5 * (src_5 * src_5)) - float3(0.2581327, 0.06704712, 0.01162958));
          float tmpvar_23;
          tmpvar_23 = dot(tmpvar_22, tmpvar_22);
          if((tmpvar_23<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_23;
              dst0_4 = float3(0.6367188, 0.40625, 0.2265625);
          }
          float3 tmpvar_24;
          tmpvar_24 = ((src_5 * (src_5 * src_5)) - float3(0.07933378, 0.03408118, 7.933378E-05));
          float tmpvar_25;
          tmpvar_25 = dot(tmpvar_24, tmpvar_24);
          if((tmpvar_25<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_25;
              dst0_4 = float3(0.4296875, 0.3242188, 0.04296875);
          }
          float3 tmpvar_26;
          tmpvar_26 = ((src_5 * (src_5 * src_5)) - float3(0.5060234, 0.1220932, 0.09793234));
          float tmpvar_27;
          tmpvar_27 = dot(tmpvar_26, tmpvar_26);
          if((tmpvar_27<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_27;
              dst0_4 = float3(0.796875, 0.4960938, 0.4609375);
          }
          float3 tmpvar_28;
          tmpvar_28 = ((src_5 * (src_5 * src_5)) - float3(0.05783435, 0.05783435, 0.05783435));
          float tmpvar_29;
          tmpvar_29 = dot(tmpvar_28, tmpvar_28);
          if((tmpvar_29<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_29;
              dst0_4 = float3(0.3867188, 0.3867188, 0.3867188);
          }
          float3 tmpvar_30;
          tmpvar_30 = ((src_5 * (src_5 * src_5)) - float3(0.1600754, 0.1600754, 0.1600754));
          float tmpvar_31;
          tmpvar_31 = dot(tmpvar_30, tmpvar_30);
          if((tmpvar_31<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_31;
              dst0_4 = float3(0.5429688, 0.5429688, 0.5429688);
          }
          float3 tmpvar_32;
          tmpvar_32 = ((src_5 * (src_5 * src_5)) - float3(0.2219603, 0.6972007, 0.2306637));
          float tmpvar_33;
          tmpvar_33 = dot(tmpvar_32, tmpvar_32);
          if((tmpvar_33<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_33;
              dst0_4 = float3(0.6054688, 0.8867188, 0.6132813);
          }
          float3 tmpvar_34;
          tmpvar_34 = ((src_5 * (src_5 * src_5)) - float3(0.1566453, 0.1220932, 0.5135016));
          float tmpvar_35;
          tmpvar_35 = dot(tmpvar_34, tmpvar_34);
          if((tmpvar_35<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_35;
              dst0_4 = float3(0.5390625, 0.4960938, 0.8007813);
          }
          float3 tmpvar_36;
          tmpvar_36 = ((src_5 * (src_5 * src_5)) - float3(0.3194437, 0.3194437, 0.3194437));
          float tmpvar_37;
          tmpvar_37 = dot(tmpvar_36, tmpvar_36);
          if((tmpvar_37<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_37;
              dst0_4 = float3(0.6835938, 0.6835938, 0.6835938);
          }
          float3 tmpvar_38;
          tmpvar_38 = ((src_5 * (src_5 * src_5)) - float3(1, 1, 1));
          float tmpvar_39;
          tmpvar_39 = dot(tmpvar_38, tmpvar_38);
          if((tmpvar_39<best0_2))
          {
              best1_1 = best0_2;
              dst1_3 = dst0_4;
              best0_2 = tmpvar_39;
              dst0_4 = float3(1, 1, 1);
          }
          float tmpvar_40;
          float x_41;
          x_41 = (q_6.x + q_6.y);
          tmpvar_40 = (x_41 - (floor((x_41 * 0.5)) * 2));
          float2 p_42;
          p_42 = (q_6 * 0.5);
          float tmpvar_43;
          tmpvar_43 = frac(((10000 * sin(((17 * p_42.x) + (p_42.y * 0.1)))) * (0.1 + abs(sin(((p_42.y * 13) + p_42.x))))));
          float3 tmpvar_44;
          if((tmpvar_40>((tmpvar_43 * 0.75) + (best1_1 / (best0_2 + best1_1)))))
          {
              tmpvar_44 = dst1_3;
          }
          else
          {
              tmpvar_44 = dst0_4;
          }
          float4 tmpvar_45;
          tmpvar_45.w = 1;
          tmpvar_45.xyz = float3(tmpvar_44);
          out_f.color = tmpvar_45;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
