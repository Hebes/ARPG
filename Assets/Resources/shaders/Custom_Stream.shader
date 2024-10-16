Shader "Custom/Stream"
{
  Properties
  {
    _MainTex ("", 2D) = "" {}
    _Strength ("_Strength", float) = 1
    _Speed ("Speed", Vector) = (0,0,0,0)
    _Seed ("Seed", float) = 1
    _TintColor ("Tint Color (RGB)", Color) = (1,1,1,1)
    _Distortion1 ("Distortion1", float) = 0
    _Distortion2 ("Distortion2", float) = 0
    _Bottom ("Bottom", float) = 0.05
    _Extend ("Extend", float) = 1
    _MyTime ("MyTime", Vector) = (0,0,0,0)
    _Gradient ("Gradient", float) = 0.1
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZTest Always
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
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
      uniform float4 _Speed;
      uniform float4 _TintColor;
      uniform float _Strength;
      uniform float _Seed;
      uniform float _Distortion1;
      uniform float _Distortion2;
      uniform float _Extend;
      uniform float _Bottom;
      uniform float4 _MyTime;
      uniform float _Gradient;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
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
          float2 tmpvar_1;
          tmpvar_1 = in_v.texcoord.xy;
          float2 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          tmpvar_2 = tmpvar_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float2 oldUv_2;
          float4 c_3;
          float4 offsetColor2_4;
          float4 offsetColor1_5;
          float fixBottom_6;
          float n2_7;
          float n1_8;
          float2 t_9;
          float2 uv_10;
          uv_10 = in_f.xlv_TEXCOORD0;
          uv_10.x = (uv_10.x + (_MyTime.y * _Speed.x));
          uv_10.y = (uv_10.y + (_MyTime.y * _Speed.y));
          t_9 = uv_10;
          fixBottom_6 = 1;
          if((in_f.xlv_TEXCOORD0.y<_Bottom))
          {
              float tmpvar_11;
              tmpvar_11 = (in_f.xlv_TEXCOORD0.y / _Bottom);
              fixBottom_6 = (tmpvar_11 + ((1 - tmpvar_11) * 0.5));
          }
          float3 tmpvar_12;
          tmpvar_12.xy = (in_f.xlv_TEXCOORD0 * _Distortion1);
          float tmpvar_13;
          tmpvar_13 = sin(_MyTime.y);
          tmpvar_12.z = ((_MyTime.x + (((tmpvar_13 + 1) / 2) * _Extend)) + _Seed);
          float4 gy1_14;
          float4 gx1_15;
          float4 gy0_16;
          float4 gx0_17;
          float3 tmpvar_18;
          tmpvar_18 = floor(tmpvar_12);
          float3 tmpvar_19;
          tmpvar_19 = (tmpvar_18 + float3(1, 1, 1));
          float3 tmpvar_20;
          tmpvar_20 = (tmpvar_18 - (floor((tmpvar_18 / 289)) * 289));
          float3 tmpvar_21;
          tmpvar_21 = (tmpvar_19 - (floor((tmpvar_19 / 289)) * 289));
          float3 tmpvar_22;
          tmpvar_22 = frac(tmpvar_12);
          float3 tmpvar_23;
          tmpvar_23 = (tmpvar_22 - float3(1, 1, 1));
          float4 tmpvar_24;
          tmpvar_24.x = tmpvar_20.x;
          tmpvar_24.y = tmpvar_21.x;
          tmpvar_24.z = tmpvar_20.x;
          tmpvar_24.w = tmpvar_21.x;
          float4 tmpvar_25;
          tmpvar_25.x = tmpvar_20.y;
          tmpvar_25.y = tmpvar_20.y;
          tmpvar_25.z = tmpvar_21.y;
          tmpvar_25.w = tmpvar_21.y;
          float4 x_26;
          x_26 = (((tmpvar_24 * 34) + 1) * tmpvar_24);
          float4 x_27;
          x_27 = ((x_26 - (floor((x_26 / 289)) * 289)) + tmpvar_25);
          float4 tmpvar_28;
          float4 x_29;
          x_29 = (((x_27 * 34) + 1) * x_27);
          tmpvar_28 = (x_29 - (floor((x_29 / 289)) * 289));
          float4 x_30;
          x_30 = (tmpvar_28 + tmpvar_20.zzzz);
          float4 x_31;
          x_31 = (((x_30 * 34) + 1) * x_30);
          float4 x_32;
          x_32 = (tmpvar_28 + tmpvar_21.zzzz);
          float4 x_33;
          x_33 = (((x_32 * 34) + 1) * x_32);
          float4 tmpvar_34;
          tmpvar_34 = ((x_31 - (floor((x_31 / 289)) * 289)) / 7);
          float4 tmpvar_35;
          tmpvar_35 = (frac((floor(tmpvar_34) / 7)) - 0.5);
          float4 tmpvar_36;
          tmpvar_36 = frac(tmpvar_34);
          float4 tmpvar_37;
          tmpvar_37 = ((float4(0.5, 0.5, 0.5, 0.5) - abs(tmpvar_36)) - abs(tmpvar_35));
          float4 tmpvar_38;
          tmpvar_38 = float4(bool4(float4(0, 0, 0, 0) >= tmpvar_37));
          gx0_17 = (tmpvar_36 - (tmpvar_38 * (float4(bool4(tmpvar_36 >= float4(0, 0, 0, 0))) - 0.5)));
          gy0_16 = (tmpvar_35 - (tmpvar_38 * (float4(bool4(tmpvar_35 >= float4(0, 0, 0, 0))) - 0.5)));
          float4 tmpvar_39;
          tmpvar_39 = ((x_33 - (floor((x_33 / 289)) * 289)) / 7);
          float4 tmpvar_40;
          tmpvar_40 = (frac((floor(tmpvar_39) / 7)) - 0.5);
          float4 tmpvar_41;
          tmpvar_41 = frac(tmpvar_39);
          float4 tmpvar_42;
          tmpvar_42 = ((float4(0.5, 0.5, 0.5, 0.5) - abs(tmpvar_41)) - abs(tmpvar_40));
          float4 tmpvar_43;
          tmpvar_43 = float4(bool4(float4(0, 0, 0, 0) >= tmpvar_42));
          gx1_15 = (tmpvar_41 - (tmpvar_43 * (float4(bool4(tmpvar_41 >= float4(0, 0, 0, 0))) - 0.5)));
          gy1_14 = (tmpvar_40 - (tmpvar_43 * (float4(bool4(tmpvar_40 >= float4(0, 0, 0, 0))) - 0.5)));
          float3 tmpvar_44;
          tmpvar_44.x = gx0_17.x;
          tmpvar_44.y = gy0_16.x;
          tmpvar_44.z = tmpvar_37.x;
          float3 tmpvar_45;
          tmpvar_45.x = gx0_17.y;
          tmpvar_45.y = gy0_16.y;
          tmpvar_45.z = tmpvar_37.y;
          float3 tmpvar_46;
          tmpvar_46.x = gx0_17.z;
          tmpvar_46.y = gy0_16.z;
          tmpvar_46.z = tmpvar_37.z;
          float3 tmpvar_47;
          tmpvar_47.x = gx0_17.w;
          tmpvar_47.y = gy0_16.w;
          tmpvar_47.z = tmpvar_37.w;
          float3 tmpvar_48;
          tmpvar_48.x = gx1_15.x;
          tmpvar_48.y = gy1_14.x;
          tmpvar_48.z = tmpvar_42.x;
          float3 tmpvar_49;
          tmpvar_49.x = gx1_15.y;
          tmpvar_49.y = gy1_14.y;
          tmpvar_49.z = tmpvar_42.y;
          float3 tmpvar_50;
          tmpvar_50.x = gx1_15.z;
          tmpvar_50.y = gy1_14.z;
          tmpvar_50.z = tmpvar_42.z;
          float3 tmpvar_51;
          tmpvar_51.x = gx1_15.w;
          tmpvar_51.y = gy1_14.w;
          tmpvar_51.z = tmpvar_42.w;
          float4 tmpvar_52;
          tmpvar_52.x = dot(tmpvar_44, tmpvar_44);
          tmpvar_52.y = dot(tmpvar_46, tmpvar_46);
          tmpvar_52.z = dot(tmpvar_45, tmpvar_45);
          tmpvar_52.w = dot(tmpvar_47, tmpvar_47);
          float4 tmpvar_53;
          tmpvar_53 = (float4(1.792843, 1.792843, 1.792843, 1.792843) - (tmpvar_52 * 0.8537347));
          float4 tmpvar_54;
          tmpvar_54.x = dot(tmpvar_48, tmpvar_48);
          tmpvar_54.y = dot(tmpvar_50, tmpvar_50);
          tmpvar_54.z = dot(tmpvar_49, tmpvar_49);
          tmpvar_54.w = dot(tmpvar_51, tmpvar_51);
          float4 tmpvar_55;
          tmpvar_55 = (float4(1.792843, 1.792843, 1.792843, 1.792843) - (tmpvar_54 * 0.8537347));
          float3 tmpvar_56;
          tmpvar_56.x = tmpvar_23.x;
          tmpvar_56.yz = tmpvar_22.yz;
          float3 tmpvar_57;
          tmpvar_57.x = tmpvar_22.x;
          tmpvar_57.y = tmpvar_23.y;
          tmpvar_57.z = tmpvar_22.z;
          float3 tmpvar_58;
          tmpvar_58.xy = tmpvar_23.xy;
          tmpvar_58.z = tmpvar_22.z;
          float3 tmpvar_59;
          tmpvar_59.xy = tmpvar_22.xy;
          tmpvar_59.z = tmpvar_23.z;
          float3 tmpvar_60;
          tmpvar_60.x = tmpvar_23.x;
          tmpvar_60.y = tmpvar_22.y;
          tmpvar_60.z = tmpvar_23.z;
          float3 tmpvar_61;
          tmpvar_61.x = tmpvar_22.x;
          tmpvar_61.yz = tmpvar_23.yz;
          float3 tmpvar_62;
          tmpvar_62 = (((tmpvar_22 * tmpvar_22) * tmpvar_22) * ((tmpvar_22 * ((tmpvar_22 * 6) - 15)) + 10));
          float4 tmpvar_63;
          tmpvar_63.x = dot((tmpvar_44 * tmpvar_53.x), tmpvar_22);
          tmpvar_63.y = dot((tmpvar_45 * tmpvar_53.z), tmpvar_56);
          tmpvar_63.z = dot((tmpvar_46 * tmpvar_53.y), tmpvar_57);
          tmpvar_63.w = dot((tmpvar_47 * tmpvar_53.w), tmpvar_58);
          float4 tmpvar_64;
          tmpvar_64.x = dot((tmpvar_48 * tmpvar_55.x), tmpvar_59);
          tmpvar_64.y = dot((tmpvar_49 * tmpvar_55.z), tmpvar_60);
          tmpvar_64.z = dot((tmpvar_50 * tmpvar_55.y), tmpvar_61);
          tmpvar_64.w = dot((tmpvar_51 * tmpvar_55.w), tmpvar_23);
          float4 tmpvar_65;
          tmpvar_65 = lerp(tmpvar_63, tmpvar_64, tmpvar_62.zzzz);
          float2 tmpvar_66;
          tmpvar_66 = lerp(tmpvar_65.xy, tmpvar_65.zw, tmpvar_62.yy);
          n1_8 = ((2.2 * lerp(tmpvar_66.x, tmpvar_66.y, tmpvar_62.x)) * fixBottom_6);
          float3 tmpvar_67;
          tmpvar_67.xy = (in_f.xlv_TEXCOORD0 * _Distortion2);
          tmpvar_67.z = ((_MyTime.y + (((sin((_MyTime.y + 1)) + 1) / 2) * _Extend)) + _Seed);
          float4 gy1_68;
          float4 gx1_69;
          float4 gy0_70;
          float4 gx0_71;
          float3 tmpvar_72;
          tmpvar_72 = floor(tmpvar_67);
          float3 tmpvar_73;
          tmpvar_73 = (tmpvar_72 + float3(1, 1, 1));
          float3 tmpvar_74;
          tmpvar_74 = (tmpvar_72 - (floor((tmpvar_72 / 289)) * 289));
          float3 tmpvar_75;
          tmpvar_75 = (tmpvar_73 - (floor((tmpvar_73 / 289)) * 289));
          float3 tmpvar_76;
          tmpvar_76 = frac(tmpvar_67);
          float3 tmpvar_77;
          tmpvar_77 = (tmpvar_76 - float3(1, 1, 1));
          float4 tmpvar_78;
          tmpvar_78.x = tmpvar_74.x;
          tmpvar_78.y = tmpvar_75.x;
          tmpvar_78.z = tmpvar_74.x;
          tmpvar_78.w = tmpvar_75.x;
          float4 tmpvar_79;
          tmpvar_79.x = tmpvar_74.y;
          tmpvar_79.y = tmpvar_74.y;
          tmpvar_79.z = tmpvar_75.y;
          tmpvar_79.w = tmpvar_75.y;
          float4 x_80;
          x_80 = (((tmpvar_78 * 34) + 1) * tmpvar_78);
          float4 x_81;
          x_81 = ((x_80 - (floor((x_80 / 289)) * 289)) + tmpvar_79);
          float4 tmpvar_82;
          float4 x_83;
          x_83 = (((x_81 * 34) + 1) * x_81);
          tmpvar_82 = (x_83 - (floor((x_83 / 289)) * 289));
          float4 x_84;
          x_84 = (tmpvar_82 + tmpvar_74.zzzz);
          float4 x_85;
          x_85 = (((x_84 * 34) + 1) * x_84);
          float4 x_86;
          x_86 = (tmpvar_82 + tmpvar_75.zzzz);
          float4 x_87;
          x_87 = (((x_86 * 34) + 1) * x_86);
          float4 tmpvar_88;
          tmpvar_88 = ((x_85 - (floor((x_85 / 289)) * 289)) / 7);
          float4 tmpvar_89;
          tmpvar_89 = (frac((floor(tmpvar_88) / 7)) - 0.5);
          float4 tmpvar_90;
          tmpvar_90 = frac(tmpvar_88);
          float4 tmpvar_91;
          tmpvar_91 = ((float4(0.5, 0.5, 0.5, 0.5) - abs(tmpvar_90)) - abs(tmpvar_89));
          float4 tmpvar_92;
          tmpvar_92 = float4(bool4(float4(0, 0, 0, 0) >= tmpvar_91));
          gx0_71 = (tmpvar_90 - (tmpvar_92 * (float4(bool4(tmpvar_90 >= float4(0, 0, 0, 0))) - 0.5)));
          gy0_70 = (tmpvar_89 - (tmpvar_92 * (float4(bool4(tmpvar_89 >= float4(0, 0, 0, 0))) - 0.5)));
          float4 tmpvar_93;
          tmpvar_93 = ((x_87 - (floor((x_87 / 289)) * 289)) / 7);
          float4 tmpvar_94;
          tmpvar_94 = (frac((floor(tmpvar_93) / 7)) - 0.5);
          float4 tmpvar_95;
          tmpvar_95 = frac(tmpvar_93);
          float4 tmpvar_96;
          tmpvar_96 = ((float4(0.5, 0.5, 0.5, 0.5) - abs(tmpvar_95)) - abs(tmpvar_94));
          float4 tmpvar_97;
          tmpvar_97 = float4(bool4(float4(0, 0, 0, 0) >= tmpvar_96));
          gx1_69 = (tmpvar_95 - (tmpvar_97 * (float4(bool4(tmpvar_95 >= float4(0, 0, 0, 0))) - 0.5)));
          gy1_68 = (tmpvar_94 - (tmpvar_97 * (float4(bool4(tmpvar_94 >= float4(0, 0, 0, 0))) - 0.5)));
          float3 tmpvar_98;
          tmpvar_98.x = gx0_71.x;
          tmpvar_98.y = gy0_70.x;
          tmpvar_98.z = tmpvar_91.x;
          float3 tmpvar_99;
          tmpvar_99.x = gx0_71.y;
          tmpvar_99.y = gy0_70.y;
          tmpvar_99.z = tmpvar_91.y;
          float3 tmpvar_100;
          tmpvar_100.x = gx0_71.z;
          tmpvar_100.y = gy0_70.z;
          tmpvar_100.z = tmpvar_91.z;
          float3 tmpvar_101;
          tmpvar_101.x = gx0_71.w;
          tmpvar_101.y = gy0_70.w;
          tmpvar_101.z = tmpvar_91.w;
          float3 tmpvar_102;
          tmpvar_102.x = gx1_69.x;
          tmpvar_102.y = gy1_68.x;
          tmpvar_102.z = tmpvar_96.x;
          float3 tmpvar_103;
          tmpvar_103.x = gx1_69.y;
          tmpvar_103.y = gy1_68.y;
          tmpvar_103.z = tmpvar_96.y;
          float3 tmpvar_104;
          tmpvar_104.x = gx1_69.z;
          tmpvar_104.y = gy1_68.z;
          tmpvar_104.z = tmpvar_96.z;
          float3 tmpvar_105;
          tmpvar_105.x = gx1_69.w;
          tmpvar_105.y = gy1_68.w;
          tmpvar_105.z = tmpvar_96.w;
          float4 tmpvar_106;
          tmpvar_106.x = dot(tmpvar_98, tmpvar_98);
          tmpvar_106.y = dot(tmpvar_100, tmpvar_100);
          tmpvar_106.z = dot(tmpvar_99, tmpvar_99);
          tmpvar_106.w = dot(tmpvar_101, tmpvar_101);
          float4 tmpvar_107;
          tmpvar_107 = (float4(1.792843, 1.792843, 1.792843, 1.792843) - (tmpvar_106 * 0.8537347));
          float4 tmpvar_108;
          tmpvar_108.x = dot(tmpvar_102, tmpvar_102);
          tmpvar_108.y = dot(tmpvar_104, tmpvar_104);
          tmpvar_108.z = dot(tmpvar_103, tmpvar_103);
          tmpvar_108.w = dot(tmpvar_105, tmpvar_105);
          float4 tmpvar_109;
          tmpvar_109 = (float4(1.792843, 1.792843, 1.792843, 1.792843) - (tmpvar_108 * 0.8537347));
          float3 tmpvar_110;
          tmpvar_110.x = tmpvar_77.x;
          tmpvar_110.yz = tmpvar_76.yz;
          float3 tmpvar_111;
          tmpvar_111.x = tmpvar_76.x;
          tmpvar_111.y = tmpvar_77.y;
          tmpvar_111.z = tmpvar_76.z;
          float3 tmpvar_112;
          tmpvar_112.xy = tmpvar_77.xy;
          tmpvar_112.z = tmpvar_76.z;
          float3 tmpvar_113;
          tmpvar_113.xy = tmpvar_76.xy;
          tmpvar_113.z = tmpvar_77.z;
          float3 tmpvar_114;
          tmpvar_114.x = tmpvar_77.x;
          tmpvar_114.y = tmpvar_76.y;
          tmpvar_114.z = tmpvar_77.z;
          float3 tmpvar_115;
          tmpvar_115.x = tmpvar_76.x;
          tmpvar_115.yz = tmpvar_77.yz;
          float3 tmpvar_116;
          tmpvar_116 = (((tmpvar_76 * tmpvar_76) * tmpvar_76) * ((tmpvar_76 * ((tmpvar_76 * 6) - 15)) + 10));
          float4 tmpvar_117;
          tmpvar_117.x = dot((tmpvar_98 * tmpvar_107.x), tmpvar_76);
          tmpvar_117.y = dot((tmpvar_99 * tmpvar_107.z), tmpvar_110);
          tmpvar_117.z = dot((tmpvar_100 * tmpvar_107.y), tmpvar_111);
          tmpvar_117.w = dot((tmpvar_101 * tmpvar_107.w), tmpvar_112);
          float4 tmpvar_118;
          tmpvar_118.x = dot((tmpvar_102 * tmpvar_109.x), tmpvar_113);
          tmpvar_118.y = dot((tmpvar_103 * tmpvar_109.z), tmpvar_114);
          tmpvar_118.z = dot((tmpvar_104 * tmpvar_109.y), tmpvar_115);
          tmpvar_118.w = dot((tmpvar_105 * tmpvar_109.w), tmpvar_77);
          float4 tmpvar_119;
          tmpvar_119 = lerp(tmpvar_117, tmpvar_118, tmpvar_116.zzzz);
          float2 tmpvar_120;
          tmpvar_120 = lerp(tmpvar_119.xy, tmpvar_119.zw, tmpvar_116.yy);
          n2_7 = ((2.2 * lerp(tmpvar_120.x, tmpvar_120.y, tmpvar_116.x)) * fixBottom_6);
          float4 tmpvar_121;
          tmpvar_121.zw = float2(0, 0);
          tmpvar_121.x = n1_8;
          tmpvar_121.y = n1_8;
          offsetColor1_5 = tmpvar_121;
          float4 tmpvar_122;
          tmpvar_122.zw = float2(0, 0);
          tmpvar_122.x = n2_7;
          tmpvar_122.y = n2_7;
          offsetColor2_4 = tmpvar_122;
          t_9.x = (uv_10.x + (((offsetColor1_5.x + offsetColor2_4.x) - 1) * _Strength));
          t_9.y = (uv_10.y + (tmpvar_13 * 0.1));
          float4 tmpvar_123;
          tmpvar_123 = tex2D(_MainTex, t_9);
          c_3 = tmpvar_123;
          c_3 = (c_3 * _TintColor);
          oldUv_2 = in_f.xlv_TEXCOORD0;
          if(((_Gradient>0) && ((1 - oldUv_2.y)<=_Gradient)))
          {
              c_3.w = (c_3.w * ((1 - oldUv_2.y) / _Gradient));
          }
          tmpvar_1 = c_3;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
