Shader "CameraFilterPack/Distortion_Noise"
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
          float4 tmpvar_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_3;
          tmpvar_3.z = 0;
          tmpvar_3.xy = float2((uv_2 * 10));
          float4 o3_4;
          float4 k4_5;
          float4 k3_6;
          float4 c_7;
          float4 k2_8;
          float4 k1_9;
          float4 b_10;
          float3 d_11;
          float3 tmpvar_12;
          tmpvar_12 = floor(tmpvar_3);
          float3 tmpvar_13;
          tmpvar_13 = (tmpvar_3 - tmpvar_12);
          d_11 = ((tmpvar_13 * tmpvar_13) * (3 - (2 * tmpvar_13)));
          float4 tmpvar_14;
          tmpvar_14 = (tmpvar_12.xxyy + float4(0, 1, 0, 1));
          b_10 = tmpvar_14;
          float4 x_15;
          x_15 = b_10.xyxy;
          float4 tmpvar_16;
          float4 x_17;
          x_17 = (((x_15 * 34) + 1) * x_15);
          tmpvar_16 = (x_17 - (floor((x_17 * 0.0034602)) * 289));
          k1_9 = tmpvar_16;
          float4 x_18;
          x_18 = (k1_9.xyxy + b_10.zzww);
          float4 tmpvar_19;
          float4 x_20;
          x_20 = (((x_18 * 34) + 1) * x_18);
          tmpvar_19 = (x_20 - (floor((x_20 * 0.0034602)) * 289));
          k2_8 = tmpvar_19;
          float4 tmpvar_21;
          tmpvar_21 = (k2_8 + tmpvar_12.zzzz);
          c_7 = tmpvar_21;
          float4 x_22;
          x_22 = c_7;
          float4 tmpvar_23;
          float4 x_24;
          x_24 = (((x_22 * 34) + 1) * x_22);
          tmpvar_23 = (x_24 - (floor((x_24 * 0.0034602)) * 289));
          k3_6 = tmpvar_23;
          float4 x_25;
          x_25 = (c_7 + 1);
          float4 tmpvar_26;
          float4 x_27;
          x_27 = (((x_25 * 34) + 1) * x_25);
          tmpvar_26 = (x_27 - (floor((x_27 * 0.0034602)) * 289));
          k4_5 = tmpvar_26;
          float4 tmpvar_28;
          tmpvar_28 = frac((k3_6 * 0.0243902));
          float4 tmpvar_29;
          tmpvar_29 = frac((k4_5 * 0.0243902));
          float4 tmpvar_30;
          tmpvar_30 = ((tmpvar_29 * d_11.z) + (tmpvar_28 * (1 - d_11.z)));
          o3_4 = tmpvar_30;
          float2 tmpvar_31;
          tmpvar_31 = ((o3_4.yw * d_11.x) + (o3_4.xz * (1 - d_11.x)));
          float3 tmpvar_32;
          tmpvar_32.z = 1;
          tmpvar_32.xy = float2((uv_2 * 10));
          float4 o3_33;
          float4 k4_34;
          float4 k3_35;
          float4 c_36;
          float4 k2_37;
          float4 k1_38;
          float4 b_39;
          float3 d_40;
          float3 tmpvar_41;
          tmpvar_41 = floor(tmpvar_32);
          float3 tmpvar_42;
          tmpvar_42 = (tmpvar_32 - tmpvar_41);
          d_40 = ((tmpvar_42 * tmpvar_42) * (3 - (2 * tmpvar_42)));
          float4 tmpvar_43;
          tmpvar_43 = (tmpvar_41.xxyy + float4(0, 1, 0, 1));
          b_39 = tmpvar_43;
          float4 x_44;
          x_44 = b_39.xyxy;
          float4 tmpvar_45;
          float4 x_46;
          x_46 = (((x_44 * 34) + 1) * x_44);
          tmpvar_45 = (x_46 - (floor((x_46 * 0.0034602)) * 289));
          k1_38 = tmpvar_45;
          float4 x_47;
          x_47 = (k1_38.xyxy + b_39.zzww);
          float4 tmpvar_48;
          float4 x_49;
          x_49 = (((x_47 * 34) + 1) * x_47);
          tmpvar_48 = (x_49 - (floor((x_49 * 0.0034602)) * 289));
          k2_37 = tmpvar_48;
          float4 tmpvar_50;
          tmpvar_50 = (k2_37 + tmpvar_41.zzzz);
          c_36 = tmpvar_50;
          float4 x_51;
          x_51 = c_36;
          float4 tmpvar_52;
          float4 x_53;
          x_53 = (((x_51 * 34) + 1) * x_51);
          tmpvar_52 = (x_53 - (floor((x_53 * 0.0034602)) * 289));
          k3_35 = tmpvar_52;
          float4 x_54;
          x_54 = (c_36 + 1);
          float4 tmpvar_55;
          float4 x_56;
          x_56 = (((x_54 * 34) + 1) * x_54);
          tmpvar_55 = (x_56 - (floor((x_56 * 0.0034602)) * 289));
          k4_34 = tmpvar_55;
          float4 tmpvar_57;
          tmpvar_57 = frac((k3_35 * 0.0243902));
          float4 tmpvar_58;
          tmpvar_58 = frac((k4_34 * 0.0243902));
          float4 tmpvar_59;
          tmpvar_59 = ((tmpvar_58 * d_40.z) + (tmpvar_57 * (1 - d_40.z)));
          o3_33 = tmpvar_59;
          float2 tmpvar_60;
          tmpvar_60 = ((o3_33.yw * d_40.x) + (o3_33.xz * (1 - d_40.x)));
          float2 tmpvar_61;
          tmpvar_61.x = ((tmpvar_31.y * d_11.y) + (tmpvar_31.x * (1 - d_11.y)));
          tmpvar_61.y = ((tmpvar_60.y * d_40.y) + (tmpvar_60.x * (1 - d_40.y)));
          float4 tmpvar_62;
          float2 P_63;
          P_63 = (uv_2 + ((tmpvar_61 * 0.1) * _Distortion));
          tmpvar_62 = tex2D(_MainTex, P_63);
          tmpvar_1 = tmpvar_62;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
