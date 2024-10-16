// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

Shader "Custom/PlayerOnionShader"
{
  Properties
  {
    _Cutoff ("Shadow alpha cutoff", Range(0, 1)) = 0.1
    _Color ("Color", Color) = (1,1,1,1)
    _MainTex ("Main Texture", 2D) = "white" {}
    _GlowTex ("Texture to Illum", 2D) = "black" {}
    _EmissionStrength ("Emission Strength", Range(0, 10)) = 1
    _LightStrength ("Light Strength", Range(0, 4)) = 1
  }
  SubShader
  {
    Tags
    { 
      "CanUseSpriteAtlas" = "true"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        "SHADOWSUPPORT" = "true"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform float _Cutoff;
      uniform float4 _Color;
      uniform sampler2D _MainTex;
      uniform sampler2D _GlowTex;
      uniform float _EmissionStrength;
      uniform float _LightStrength;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
          float2 xlv_TEXCOORD5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
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
          float3 worldNormal_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          float2 tmpvar_5;
          float4 tmpvar_6;
          tmpvar_6.xyz = tmpvar_1.xyz;
          tmpvar_6.w = tmpvar_1.w;
          tmpvar_4 = tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_8;
          tmpvar_8[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_8[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_8[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_9;
          tmpvar_9 = normalize(mul(float3(0, 0, (-1)), tmpvar_8));
          worldNormal_2 = tmpvar_9;
          tmpvar_3 = worldNormal_2;
          float3 normal_10;
          normal_10 = worldNormal_2;
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(normal_10);
          float3 res_12;
          float3 x_13;
          x_13.x = dot(unity_SHAr, tmpvar_11);
          x_13.y = dot(unity_SHAg, tmpvar_11);
          x_13.z = dot(unity_SHAb, tmpvar_11);
          float3 x1_14;
          float4 tmpvar_15;
          tmpvar_15 = (normal_10.xyzz * normal_10.yzzx);
          x1_14.x = dot(unity_SHBr, tmpvar_15);
          x1_14.y = dot(unity_SHBg, tmpvar_15);
          x1_14.z = dot(unity_SHBb, tmpvar_15);
          res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * ((normal_10.x * normal_10.x) - (normal_10.y * normal_10.y)))));
          float3 tmpvar_16;
          float _tmp_dvx_67 = max(((1.055 * pow(max(res_12, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_16 = float3(_tmp_dvx_67, _tmp_dvx_67, _tmp_dvx_67);
          res_12 = tmpvar_16;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_7));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          out_v.xlv_TEXCOORD4 = max(float3(0, 0, 0), tmpvar_16);
          out_v.xlv_TEXCOORD5 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float4 c_3;
          float3 tmpvar_4;
          float3 lightDir_5;
          float4 tmpvar_6;
          tmpvar_6 = in_f.xlv_TEXCOORD3;
          float3 tmpvar_7;
          tmpvar_7 = _WorldSpaceLightPos0.xyz;
          lightDir_5 = tmpvar_7;
          tmpvar_4 = in_f.xlv_TEXCOORD1;
          float3 tmpvar_8;
          float3 tmpvar_9;
          float4 calc_10;
          float4 tmpvar_11;
          tmpvar_11 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * tmpvar_6);
          float x_12;
          x_12 = (tmpvar_11.w - _Cutoff);
          if((x_12<0))
          {
              discard;
          }
          calc_10.w = tmpvar_11.w;
          calc_10.xyz = (tmpvar_11.xyz * _Color.xyz);
          tmpvar_9 = ((tex2D(_GlowTex, in_f.xlv_TEXCOORD0).xyz * tmpvar_6.xyz) * _EmissionStrength);
          tmpvar_8 = (calc_10.xyz * _LightStrength);
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_5;
          float4 c_13;
          float4 c_14;
          float diff_15;
          float tmpvar_16;
          tmpvar_16 = max(0, dot(tmpvar_4, tmpvar_2));
          diff_15 = tmpvar_16;
          c_14.xyz = float3(((tmpvar_8 * tmpvar_1) * diff_15));
          c_14.w = (tmpvar_11.w * _Color.w);
          c_13.w = c_14.w;
          c_13.xyz = (c_14.xyz + (tmpvar_8 * in_f.xlv_TEXCOORD4));
          c_3.w = c_13.w;
          c_3.xyz = (c_13.xyz + tmpvar_9);
          out_f.color = c_3;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: PREPASS
    {
      Name "PREPASS"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "LIGHTMODE" = "PREPASSBASE"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
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
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
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
          float3 worldNormal_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.xyz = tmpvar_1.xyz;
          tmpvar_5.w = tmpvar_1.w;
          tmpvar_4 = tmpvar_5;
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_7;
          tmpvar_7[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_7[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_7[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_8;
          tmpvar_8 = normalize(mul(float3(0, 0, (-1)), tmpvar_7));
          worldNormal_2 = tmpvar_8;
          tmpvar_3 = worldNormal_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 res_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = in_f.xlv_TEXCOORD3;
          tmpvar_2 = in_f.xlv_TEXCOORD1;
          float x_4;
          x_4 = ((tex2D(_MainTex, in_f.xlv_TEXCOORD0) * tmpvar_3).w - _Cutoff);
          if((x_4<0))
          {
              discard;
          }
          res_1.xyz = float3(((tmpvar_2 * 0.5) + 0.5));
          res_1.w = 0;
          out_f.color = res_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: PREPASS
    {
      Name "PREPASS"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "LIGHTMODE" = "PREPASSFINAL"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _ProjectionParams;
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform float4 _Color;
      uniform sampler2D _MainTex;
      uniform sampler2D _GlowTex;
      uniform float _EmissionStrength;
      uniform float _LightStrength;
      uniform sampler2D _LightBuffer;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_TEXCOORD4 :TEXCOORD4;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
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
          float4 tmpvar_2;
          float4 tmpvar_3;
          float3 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.xyz = tmpvar_1.xyz;
          tmpvar_5.w = tmpvar_1.w;
          tmpvar_2 = tmpvar_5;
          float4 tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = in_v.vertex.xyz;
          tmpvar_6 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_7));
          float4 o_8;
          float4 tmpvar_9;
          tmpvar_9 = (tmpvar_6 * 0.5);
          float2 tmpvar_10;
          tmpvar_10.x = tmpvar_9.x;
          tmpvar_10.y = (tmpvar_9.y * _ProjectionParams.x);
          o_8.xy = (tmpvar_10 + tmpvar_9.w);
          o_8.zw = tmpvar_6.zw;
          tmpvar_3.zw = float2(0, 0);
          tmpvar_3.xy = float2(0, 0);
          float3x3 tmpvar_11;
          tmpvar_11[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_11[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_11[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = float3(normalize(mul(float3(0, 0, (-1)), tmpvar_11)));
          float4 normal_13;
          normal_13 = tmpvar_12;
          float3 res_14;
          float3 x_15;
          x_15.x = dot(unity_SHAr, normal_13);
          x_15.y = dot(unity_SHAg, normal_13);
          x_15.z = dot(unity_SHAb, normal_13);
          float3 x1_16;
          float4 tmpvar_17;
          tmpvar_17 = (normal_13.xyzz * normal_13.yzzx);
          x1_16.x = dot(unity_SHBr, tmpvar_17);
          x1_16.y = dot(unity_SHBg, tmpvar_17);
          x1_16.z = dot(unity_SHBb, tmpvar_17);
          res_14 = (x_15 + (x1_16 + (unity_SHC.xyz * ((normal_13.x * normal_13.x) - (normal_13.y * normal_13.y)))));
          float3 tmpvar_18;
          float _tmp_dvx_68 = max(((1.055 * pow(max(res_14, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_18 = float3(_tmp_dvx_68, _tmp_dvx_68, _tmp_dvx_68);
          res_14 = tmpvar_18;
          tmpvar_4 = tmpvar_18;
          out_v.vertex = tmpvar_6;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD2 = tmpvar_2;
          out_v.xlv_TEXCOORD3 = o_8;
          out_v.xlv_TEXCOORD4 = tmpvar_3;
          out_v.xlv_TEXCOORD5 = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float4 c_2;
          float4 light_3;
          float4 tmpvar_4;
          tmpvar_4 = in_f.xlv_TEXCOORD2;
          float3 tmpvar_5;
          float3 tmpvar_6;
          float4 calc_7;
          float4 tmpvar_8;
          tmpvar_8 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * tmpvar_4);
          float x_9;
          x_9 = (tmpvar_8.w - _Cutoff);
          if((x_9<0))
          {
              discard;
          }
          calc_7.w = tmpvar_8.w;
          calc_7.xyz = (tmpvar_8.xyz * _Color.xyz);
          tmpvar_6 = ((tex2D(_GlowTex, in_f.xlv_TEXCOORD0).xyz * tmpvar_4.xyz) * _EmissionStrength);
          tmpvar_5 = (calc_7.xyz * _LightStrength);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_LightBuffer, in_f.xlv_TEXCOORD3);
          light_3 = tmpvar_10;
          light_3 = (-log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001))));
          light_3.xyz = (light_3.xyz + in_f.xlv_TEXCOORD5);
          float4 c_11;
          c_11.xyz = (tmpvar_5 * light_3.xyz);
          c_11.w = (tmpvar_8.w * _Color.w);
          c_2 = c_11;
          c_2.xyz = (c_2.xyz + tmpvar_6);
          tmpvar_1 = c_2;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: DEFERRED
    {
      Name "DEFERRED"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "LIGHTMODE" = "DEFERRED"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform float4 _Color;
      uniform sampler2D _MainTex;
      uniform sampler2D _GlowTex;
      uniform float _EmissionStrength;
      uniform float _LightStrength;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_TEXCOORD4 :TEXCOORD4;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
          float4 color1 :SV_Target1;
          float4 color2 :SV_Target2;
          float4 color3 :SV_Target3;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float3 worldNormal_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          float4 tmpvar_6;
          tmpvar_6.xyz = tmpvar_1.xyz;
          tmpvar_6.w = tmpvar_1.w;
          tmpvar_4 = tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_8;
          tmpvar_8[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_8[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_8[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_9;
          tmpvar_9 = normalize(mul(float3(0, 0, (-1)), tmpvar_8));
          worldNormal_2 = tmpvar_9;
          tmpvar_3 = worldNormal_2;
          tmpvar_5.zw = float2(0, 0);
          tmpvar_5.xy = float2(0, 0);
          float3 normal_10;
          normal_10 = worldNormal_2;
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(normal_10);
          float3 res_12;
          float3 x_13;
          x_13.x = dot(unity_SHAr, tmpvar_11);
          x_13.y = dot(unity_SHAg, tmpvar_11);
          x_13.z = dot(unity_SHAb, tmpvar_11);
          float3 x1_14;
          float4 tmpvar_15;
          tmpvar_15 = (normal_10.xyzz * normal_10.yzzx);
          x1_14.x = dot(unity_SHBr, tmpvar_15);
          x1_14.y = dot(unity_SHBg, tmpvar_15);
          x1_14.z = dot(unity_SHBb, tmpvar_15);
          res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * ((normal_10.x * normal_10.x) - (normal_10.y * normal_10.y)))));
          float3 tmpvar_16;
          float _tmp_dvx_69 = max(((1.055 * pow(max(res_12, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_16 = float3(_tmp_dvx_69, _tmp_dvx_69, _tmp_dvx_69);
          res_12 = tmpvar_16;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_7));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          out_v.xlv_TEXCOORD4 = tmpvar_5;
          out_v.xlv_TEXCOORD5 = max(float3(0, 0, 0), tmpvar_16);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 outEmission_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = in_f.xlv_TEXCOORD3;
          tmpvar_2 = in_f.xlv_TEXCOORD1;
          float3 tmpvar_4;
          float3 tmpvar_5;
          float4 calc_6;
          float4 tmpvar_7;
          tmpvar_7 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * tmpvar_3);
          float x_8;
          x_8 = (tmpvar_7.w - _Cutoff);
          if((x_8<0))
          {
              discard;
          }
          calc_6.w = tmpvar_7.w;
          calc_6.xyz = (tmpvar_7.xyz * _Color.xyz);
          tmpvar_5 = ((tex2D(_GlowTex, in_f.xlv_TEXCOORD0).xyz * tmpvar_3.xyz) * _EmissionStrength);
          tmpvar_4 = (calc_6.xyz * _LightStrength);
          float4 emission_9;
          float3 tmpvar_10;
          float3 tmpvar_11;
          tmpvar_10 = tmpvar_4;
          tmpvar_11 = tmpvar_2;
          float4 tmpvar_12;
          tmpvar_12.xyz = float3(tmpvar_10);
          tmpvar_12.w = 1;
          float4 tmpvar_13;
          tmpvar_13.xyz = float3(0, 0, 0);
          tmpvar_13.w = 0;
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          tmpvar_14.xyz = float3(((tmpvar_11 * 0.5) + 0.5));
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = float3(tmpvar_5);
          emission_9 = tmpvar_15;
          emission_9.xyz = (emission_9.xyz + (tmpvar_4 * in_f.xlv_TEXCOORD5));
          outEmission_1.w = emission_9.w;
          outEmission_1.xyz = exp2((-emission_9.xyz));
          out_f.color = tmpvar_12;
          out_f.color1 = tmpvar_13;
          out_f.color2 = tmpvar_14;
          out_f.color3 = outEmission_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 5, name: META
    {
      Name "META"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "LIGHTMODE" = "META"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      uniform float4 unity_MetaVertexControl;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform float4 _Color;
      uniform sampler2D _MainTex;
      uniform sampler2D _GlowTex;
      uniform float _EmissionStrength;
      uniform float _LightStrength;
      uniform float4 unity_MetaFragmentControl;
      uniform float unity_OneOverOutputBoost;
      uniform float unity_MaxOutputValue;
      uniform float unity_UseLinearSpace;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
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
          float4 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.xyz = tmpvar_1.xyz;
          tmpvar_3.w = tmpvar_1.w;
          tmpvar_2 = tmpvar_3;
          float4 vertex_4;
          vertex_4 = in_v.vertex;
          if(unity_MetaVertexControl.x)
          {
              vertex_4.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
              float tmpvar_5;
              if((in_v.vertex.z>0))
              {
                  tmpvar_5 = 0.0001;
              }
              else
              {
                  tmpvar_5 = 0;
              }
              vertex_4.z = tmpvar_5;
          }
          if(unity_MetaVertexControl.y)
          {
              vertex_4.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
              float tmpvar_6;
              if((vertex_4.z>0))
              {
                  tmpvar_6 = 0.0001;
              }
              else
              {
                  tmpvar_6 = 0;
              }
              vertex_4.z = tmpvar_6;
          }
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = vertex_4.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_7));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD2 = tmpvar_2;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          tmpvar_4 = in_f.xlv_TEXCOORD2;
          float3 tmpvar_5;
          float3 tmpvar_6;
          float4 calc_7;
          float4 tmpvar_8;
          tmpvar_8 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * tmpvar_4);
          float x_9;
          x_9 = (tmpvar_8.w - _Cutoff);
          if((x_9<0))
          {
              discard;
          }
          calc_7.w = tmpvar_8.w;
          calc_7.xyz = (tmpvar_8.xyz * _Color.xyz);
          tmpvar_6 = ((tex2D(_GlowTex, in_f.xlv_TEXCOORD0).xyz * tmpvar_4.xyz) * _EmissionStrength);
          tmpvar_5 = (calc_7.xyz * _LightStrength);
          tmpvar_2 = tmpvar_5;
          tmpvar_3 = tmpvar_6;
          float4 res_10;
          res_10 = float4(0, 0, 0, 0);
          if(unity_MetaFragmentControl.x)
          {
              float4 tmpvar_11;
              tmpvar_11.w = 1;
              tmpvar_11.xyz = float3(tmpvar_2);
              res_10.w = tmpvar_11.w;
              float3 tmpvar_12;
              float _tmp_dvx_70 = clamp(unity_OneOverOutputBoost, 0, 1);
              tmpvar_12 = clamp(pow(tmpvar_2, float3(_tmp_dvx_70, _tmp_dvx_70, _tmp_dvx_70)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
              res_10.xyz = float3(tmpvar_12);
          }
          if(unity_MetaFragmentControl.y)
          {
              float3 emission_13;
              if(int(unity_UseLinearSpace))
              {
                  emission_13 = tmpvar_3;
              }
              else
              {
                  emission_13 = (tmpvar_3 * ((tmpvar_3 * ((tmpvar_3 * 0.305306) + 0.6821711)) + 0.01252288));
              }
              float4 tmpvar_14;
              float alpha_15;
              float3 tmpvar_16;
              tmpvar_16 = (emission_13 * 0.01030928);
              alpha_15 = (ceil((max(max(tmpvar_16.x, tmpvar_16.y), max(tmpvar_16.z, 0.02)) * 255)) / 255);
              float tmpvar_17;
              tmpvar_17 = max(alpha_15, 0.02);
              alpha_15 = tmpvar_17;
              float4 tmpvar_18;
              tmpvar_18.xyz = float3((tmpvar_16 / tmpvar_17));
              tmpvar_18.w = tmpvar_17;
              tmpvar_14 = tmpvar_18;
              res_10 = tmpvar_14;
          }
          tmpvar_1 = res_10;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
