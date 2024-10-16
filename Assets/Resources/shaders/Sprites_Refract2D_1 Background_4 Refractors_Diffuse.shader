// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

Shader "Sprites/Refract2D/1 Background/4 Refractors_Diffuse"
{
  Properties
  {
    [PerRendererData] _MainTex ("MainTex", 2D) = "" {}
    _Color ("Tint", Color) = (1,1,1,1)
    _MainTexScrollX ("X Offset", float) = 0
    _MainTexScrollY ("Y Offset", float) = 0
    _MainTexScaleX ("X Scale", float) = 1
    _MainTexScaleY ("Y Scale", float) = 1
    _Refraction ("总扭曲强度", float) = 1
    _DistortionMap ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX1 ("X方向移动速度(贴图/s)", float) = 0
    _DispScrollSpeedY1 ("Y方向移动速度(贴图/s)", float) = -0.25
    _DistortionScaleX ("贴图拉伸比例X", float) = 1
    _DistortionScaleY ("贴图拉伸比例Y", float) = 0.5
    _DistortionPower ("扭曲强度", float) = 0.08
    _DistortionMap2 ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX2 ("X方向移动速度", float) = 0
    _DispScrollSpeedY2 ("Y方向移动速度", float) = -0.25
    _DistortionScaleX2 ("贴图拉伸比例X", float) = 0.5
    _DistortionScaleY2 ("贴图拉伸比例Y", float) = 1
    _DistortionPower2 ("扭曲强度", float) = 0.08
    _DistortionMap3 ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX3 ("X方向移动速度", float) = 0
    _DispScrollSpeedY3 ("Y方向移动速度", float) = -0.25
    _DistortionScaleX3 ("贴图拉伸比例X", float) = 1
    _DistortionScaleY3 ("贴图拉伸比例Y", float) = 0.5
    _DistortionPower3 ("扭曲强度", float) = 0.08
    _DistortionMap4 ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX4 ("X方向移动速度", float) = 0
    _DispScrollSpeedY4 ("Y方向移动速度", float) = -0.25
    _DistortionScaleX4 ("贴图拉伸比例X", float) = 1
    _DistortionScaleY4 ("贴图拉伸比例Y", float) = 0.5
    _DistortionPower4 ("扭曲强度", float) = 0.08
  }
  SubShader
  {
    Tags
    { 
      "CanUseSpriteAtlas" = "true"
      "IGNOREPROJECTOR" = "true"
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
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        "SHADOWSUPPORT" = "true"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      //uniform float4 _Time;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform sampler2D _DistortionMap3;
      uniform sampler2D _DistortionMap4;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _DispScrollSpeedX3;
      uniform float _DispScrollSpeedY3;
      uniform float _DistortionPower3;
      uniform float _DistortionScaleX3;
      uniform float _DistortionScaleY3;
      uniform float _DispScrollSpeedX4;
      uniform float _DispScrollSpeedY4;
      uniform float _DistortionPower4;
      uniform float _DistortionScaleX4;
      uniform float _DistortionScaleY4;
      uniform float _Refraction;
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
          tmpvar_4 = tmpvar_1;
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
          float3 normal_9;
          normal_9 = worldNormal_2;
          float4 tmpvar_10;
          tmpvar_10.w = 1;
          tmpvar_10.xyz = float3(normal_9);
          float3 res_11;
          float3 x_12;
          x_12.x = dot(unity_SHAr, tmpvar_10);
          x_12.y = dot(unity_SHAg, tmpvar_10);
          x_12.z = dot(unity_SHAb, tmpvar_10);
          float3 x1_13;
          float4 tmpvar_14;
          tmpvar_14 = (normal_9.xyzz * normal_9.yzzx);
          x1_13.x = dot(unity_SHBr, tmpvar_14);
          x1_13.y = dot(unity_SHBg, tmpvar_14);
          x1_13.z = dot(unity_SHBb, tmpvar_14);
          res_11 = (x_12 + (x1_13 + (unity_SHC.xyz * ((normal_9.x * normal_9.x) - (normal_9.y * normal_9.y)))));
          float3 tmpvar_15;
          float _tmp_dvx_172 = max(((1.055 * pow(max(res_11, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_15 = float3(_tmp_dvx_172, _tmp_dvx_172, _tmp_dvx_172);
          res_11 = tmpvar_15;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          out_v.xlv_TEXCOORD4 = max(float3(0, 0, 0), tmpvar_15);
          out_v.xlv_TEXCOORD5 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float3 tmpvar_4;
          float3 lightDir_5;
          float4 tmpvar_6;
          tmpvar_6 = in_f.xlv_TEXCOORD3;
          float3 tmpvar_7;
          tmpvar_7 = _WorldSpaceLightPos0.xyz;
          lightDir_5 = tmpvar_7;
          tmpvar_4 = in_f.xlv_TEXCOORD1;
          float2 distortion4_8;
          float2 distortion3_9;
          float2 distortion2_10;
          float2 distortion_11;
          float2 tmpvar_12;
          tmpvar_12.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_12.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_13;
          tmpvar_13.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_13.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_14;
          tmpvar_14.x = (_Time.y * _DispScrollSpeedX3);
          tmpvar_14.y = (_Time.y * _DispScrollSpeedY3);
          float2 tmpvar_15;
          tmpvar_15.x = (_Time.y * _DispScrollSpeedX4);
          tmpvar_15.y = (_Time.y * _DispScrollSpeedY4);
          float2 tmpvar_16;
          tmpvar_16.x = _DistortionScaleX;
          tmpvar_16.y = _DistortionScaleY;
          float4 tmpvar_17;
          float2 P_18;
          P_18 = ((tmpvar_16 * in_f.xlv_TEXCOORD0) + tmpvar_12);
          tmpvar_17 = tex2D(_DistortionMap, P_18);
          float2 tmpvar_19;
          tmpvar_19 = ((tmpvar_17 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_11 = tmpvar_19;
          float2 tmpvar_20;
          tmpvar_20.x = _DistortionScaleX2;
          tmpvar_20.y = _DistortionScaleY2;
          float4 tmpvar_21;
          float2 P_22;
          P_22 = ((tmpvar_20 * in_f.xlv_TEXCOORD0) + tmpvar_13);
          tmpvar_21 = tex2D(_DistortionMap2, P_22);
          float2 tmpvar_23;
          tmpvar_23 = ((tmpvar_21 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_10 = tmpvar_23;
          float2 tmpvar_24;
          tmpvar_24.x = _DistortionScaleX3;
          tmpvar_24.y = _DistortionScaleY3;
          float4 tmpvar_25;
          float2 P_26;
          P_26 = ((tmpvar_24 * in_f.xlv_TEXCOORD0) + tmpvar_14);
          tmpvar_25 = tex2D(_DistortionMap3, P_26);
          float2 tmpvar_27;
          tmpvar_27 = ((tmpvar_25 * _DistortionPower3) - (_DistortionPower3 * 0.5)).xy;
          distortion3_9 = tmpvar_27;
          float2 tmpvar_28;
          tmpvar_28.x = _DistortionScaleX4;
          tmpvar_28.y = _DistortionScaleY4;
          float4 tmpvar_29;
          float2 P_30;
          P_30 = ((tmpvar_28 * in_f.xlv_TEXCOORD0) + tmpvar_15);
          tmpvar_29 = tex2D(_DistortionMap4, P_30);
          float2 tmpvar_31;
          tmpvar_31 = ((tmpvar_29 * _DistortionPower4) - (_DistortionPower4 * 0.5)).xy;
          distortion4_8 = tmpvar_31;
          float2 tmpvar_32;
          tmpvar_32.x = _MainTexScaleX;
          tmpvar_32.y = _MainTexScaleY;
          float2 tmpvar_33;
          tmpvar_33.x = _MainTexScrollX;
          tmpvar_33.y = _MainTexScrollY;
          float2 P_34;
          P_34 = (((tmpvar_32 * in_f.xlv_TEXCOORD0) + (_Refraction * ((distortion_11 + distortion2_10) + (distortion3_9 + distortion4_8)))) + tmpvar_33);
          float4 tmpvar_35;
          tmpvar_35 = ((tex2D(_MainTex, P_34) * tmpvar_6) * _Color);
          tmpvar_3 = (tmpvar_35.xyz * tmpvar_35.w);
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_5;
          float4 c_36;
          float4 c_37;
          float diff_38;
          float tmpvar_39;
          tmpvar_39 = max(0, dot(tmpvar_4, tmpvar_2));
          diff_38 = tmpvar_39;
          c_37.xyz = float3(((tmpvar_3 * tmpvar_1) * diff_38));
          c_37.w = tmpvar_35.w;
          c_36.w = c_37.w;
          c_36.xyz = (c_37.xyz + (tmpvar_3 * in_f.xlv_TEXCOORD4));
          out_f.color = c_36;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDADD"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One One
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
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
      //uniform float4 _Time;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _LightTexture0;
      uniform float4x4 unity_WorldToLight;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform sampler2D _DistortionMap3;
      uniform sampler2D _DistortionMap4;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _DispScrollSpeedX3;
      uniform float _DispScrollSpeedY3;
      uniform float _DistortionPower3;
      uniform float _DistortionScaleX3;
      uniform float _DistortionScaleY3;
      uniform float _DispScrollSpeedX4;
      uniform float _DispScrollSpeedY4;
      uniform float _DistortionPower4;
      uniform float _DistortionScaleX4;
      uniform float _DistortionScaleY4;
      uniform float _Refraction;
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
          float2 xlv_TEXCOORD4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
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
          float2 tmpvar_5;
          tmpvar_4 = tmpvar_1;
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
          out_v.xlv_TEXCOORD4 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float3 lightCoord_3;
          float3 tmpvar_4;
          float3 lightDir_5;
          float4 tmpvar_6;
          tmpvar_6 = in_f.xlv_TEXCOORD3;
          float3 tmpvar_7;
          tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD2));
          lightDir_5 = tmpvar_7;
          tmpvar_4 = in_f.xlv_TEXCOORD1;
          float2 distortion4_8;
          float2 distortion3_9;
          float2 distortion2_10;
          float2 distortion_11;
          float2 tmpvar_12;
          tmpvar_12.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_12.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_13;
          tmpvar_13.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_13.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_14;
          tmpvar_14.x = (_Time.y * _DispScrollSpeedX3);
          tmpvar_14.y = (_Time.y * _DispScrollSpeedY3);
          float2 tmpvar_15;
          tmpvar_15.x = (_Time.y * _DispScrollSpeedX4);
          tmpvar_15.y = (_Time.y * _DispScrollSpeedY4);
          float2 tmpvar_16;
          tmpvar_16.x = _DistortionScaleX;
          tmpvar_16.y = _DistortionScaleY;
          float4 tmpvar_17;
          float2 P_18;
          P_18 = ((tmpvar_16 * in_f.xlv_TEXCOORD0) + tmpvar_12);
          tmpvar_17 = tex2D(_DistortionMap, P_18);
          float2 tmpvar_19;
          tmpvar_19 = ((tmpvar_17 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_11 = tmpvar_19;
          float2 tmpvar_20;
          tmpvar_20.x = _DistortionScaleX2;
          tmpvar_20.y = _DistortionScaleY2;
          float4 tmpvar_21;
          float2 P_22;
          P_22 = ((tmpvar_20 * in_f.xlv_TEXCOORD0) + tmpvar_13);
          tmpvar_21 = tex2D(_DistortionMap2, P_22);
          float2 tmpvar_23;
          tmpvar_23 = ((tmpvar_21 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_10 = tmpvar_23;
          float2 tmpvar_24;
          tmpvar_24.x = _DistortionScaleX3;
          tmpvar_24.y = _DistortionScaleY3;
          float4 tmpvar_25;
          float2 P_26;
          P_26 = ((tmpvar_24 * in_f.xlv_TEXCOORD0) + tmpvar_14);
          tmpvar_25 = tex2D(_DistortionMap3, P_26);
          float2 tmpvar_27;
          tmpvar_27 = ((tmpvar_25 * _DistortionPower3) - (_DistortionPower3 * 0.5)).xy;
          distortion3_9 = tmpvar_27;
          float2 tmpvar_28;
          tmpvar_28.x = _DistortionScaleX4;
          tmpvar_28.y = _DistortionScaleY4;
          float4 tmpvar_29;
          float2 P_30;
          P_30 = ((tmpvar_28 * in_f.xlv_TEXCOORD0) + tmpvar_15);
          tmpvar_29 = tex2D(_DistortionMap4, P_30);
          float2 tmpvar_31;
          tmpvar_31 = ((tmpvar_29 * _DistortionPower4) - (_DistortionPower4 * 0.5)).xy;
          distortion4_8 = tmpvar_31;
          float2 tmpvar_32;
          tmpvar_32.x = _MainTexScaleX;
          tmpvar_32.y = _MainTexScaleY;
          float2 tmpvar_33;
          tmpvar_33.x = _MainTexScrollX;
          tmpvar_33.y = _MainTexScrollY;
          float2 P_34;
          P_34 = (((tmpvar_32 * in_f.xlv_TEXCOORD0) + (_Refraction * ((distortion_11 + distortion2_10) + (distortion3_9 + distortion4_8)))) + tmpvar_33);
          float4 tmpvar_35;
          tmpvar_35 = ((tex2D(_MainTex, P_34) * tmpvar_6) * _Color);
          float4 tmpvar_36;
          tmpvar_36.w = 1;
          tmpvar_36.xyz = in_f.xlv_TEXCOORD2;
          lightCoord_3 = mul(unity_WorldToLight, tmpvar_36).xyz;
          float tmpvar_37;
          tmpvar_37 = dot(lightCoord_3, lightCoord_3);
          float tmpvar_38;
          tmpvar_38 = tex2D(_LightTexture0, float2(tmpvar_37, tmpvar_37)).w;
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_5;
          tmpvar_1 = (tmpvar_1 * tmpvar_38);
          float4 c_39;
          float4 c_40;
          float diff_41;
          float tmpvar_42;
          tmpvar_42 = max(0, dot(tmpvar_4, tmpvar_2));
          diff_41 = tmpvar_42;
          c_40.xyz = ((tmpvar_35.xyz * tmpvar_35.w) * (tmpvar_1 * diff_41));
          c_40.w = tmpvar_35.w;
          c_39.w = c_40.w;
          c_39.xyz = c_40.xyz;
          out_f.color = c_39;
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
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "PREPASSBASE"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
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
          tmpvar_4 = tmpvar_1;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_6;
          tmpvar_6[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_6[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_6[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_7;
          tmpvar_7 = normalize(mul(float3(0, 0, (-1)), tmpvar_6));
          worldNormal_2 = tmpvar_7;
          tmpvar_3 = worldNormal_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_TEXCOORD0 = tmpvar_3;
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD2 = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 res_1;
          float3 tmpvar_2;
          tmpvar_2 = in_f.xlv_TEXCOORD0;
          res_1.xyz = float3(((tmpvar_2 * 0.5) + 0.5));
          res_1.w = 0;
          out_f.color = res_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: PREPASS
    {
      Name "PREPASS"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "PREPASSFINAL"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform sampler2D _DistortionMap3;
      uniform sampler2D _DistortionMap4;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _DispScrollSpeedX3;
      uniform float _DispScrollSpeedY3;
      uniform float _DistortionPower3;
      uniform float _DistortionScaleX3;
      uniform float _DistortionScaleY3;
      uniform float _DispScrollSpeedX4;
      uniform float _DispScrollSpeedY4;
      uniform float _DistortionPower4;
      uniform float _DistortionScaleX4;
      uniform float _DistortionScaleY4;
      uniform float _Refraction;
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
          tmpvar_2 = tmpvar_1;
          float4 tmpvar_5;
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = in_v.vertex.xyz;
          tmpvar_5 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          float4 o_7;
          float4 tmpvar_8;
          tmpvar_8 = (tmpvar_5 * 0.5);
          float2 tmpvar_9;
          tmpvar_9.x = tmpvar_8.x;
          tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
          o_7.xy = (tmpvar_9 + tmpvar_8.w);
          o_7.zw = tmpvar_5.zw;
          tmpvar_3.zw = float2(0, 0);
          tmpvar_3.xy = float2(0, 0);
          float3x3 tmpvar_10;
          tmpvar_10[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_10[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_10[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(normalize(mul(float3(0, 0, (-1)), tmpvar_10)));
          float4 normal_12;
          normal_12 = tmpvar_11;
          float3 res_13;
          float3 x_14;
          x_14.x = dot(unity_SHAr, normal_12);
          x_14.y = dot(unity_SHAg, normal_12);
          x_14.z = dot(unity_SHAb, normal_12);
          float3 x1_15;
          float4 tmpvar_16;
          tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
          x1_15.x = dot(unity_SHBr, tmpvar_16);
          x1_15.y = dot(unity_SHBg, tmpvar_16);
          x1_15.z = dot(unity_SHBb, tmpvar_16);
          res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y)))));
          float3 tmpvar_17;
          float _tmp_dvx_173 = max(((1.055 * pow(max(res_13, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_17 = float3(_tmp_dvx_173, _tmp_dvx_173, _tmp_dvx_173);
          res_13 = tmpvar_17;
          tmpvar_4 = tmpvar_17;
          out_v.vertex = tmpvar_5;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD2 = tmpvar_2;
          out_v.xlv_TEXCOORD3 = o_7;
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
          float2 distortion4_5;
          float2 distortion3_6;
          float2 distortion2_7;
          float2 distortion_8;
          float2 tmpvar_9;
          tmpvar_9.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_9.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_10;
          tmpvar_10.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_10.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_11;
          tmpvar_11.x = (_Time.y * _DispScrollSpeedX3);
          tmpvar_11.y = (_Time.y * _DispScrollSpeedY3);
          float2 tmpvar_12;
          tmpvar_12.x = (_Time.y * _DispScrollSpeedX4);
          tmpvar_12.y = (_Time.y * _DispScrollSpeedY4);
          float2 tmpvar_13;
          tmpvar_13.x = _DistortionScaleX;
          tmpvar_13.y = _DistortionScaleY;
          float4 tmpvar_14;
          float2 P_15;
          P_15 = ((tmpvar_13 * in_f.xlv_TEXCOORD0) + tmpvar_9);
          tmpvar_14 = tex2D(_DistortionMap, P_15);
          float2 tmpvar_16;
          tmpvar_16 = ((tmpvar_14 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_8 = tmpvar_16;
          float2 tmpvar_17;
          tmpvar_17.x = _DistortionScaleX2;
          tmpvar_17.y = _DistortionScaleY2;
          float4 tmpvar_18;
          float2 P_19;
          P_19 = ((tmpvar_17 * in_f.xlv_TEXCOORD0) + tmpvar_10);
          tmpvar_18 = tex2D(_DistortionMap2, P_19);
          float2 tmpvar_20;
          tmpvar_20 = ((tmpvar_18 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_7 = tmpvar_20;
          float2 tmpvar_21;
          tmpvar_21.x = _DistortionScaleX3;
          tmpvar_21.y = _DistortionScaleY3;
          float4 tmpvar_22;
          float2 P_23;
          P_23 = ((tmpvar_21 * in_f.xlv_TEXCOORD0) + tmpvar_11);
          tmpvar_22 = tex2D(_DistortionMap3, P_23);
          float2 tmpvar_24;
          tmpvar_24 = ((tmpvar_22 * _DistortionPower3) - (_DistortionPower3 * 0.5)).xy;
          distortion3_6 = tmpvar_24;
          float2 tmpvar_25;
          tmpvar_25.x = _DistortionScaleX4;
          tmpvar_25.y = _DistortionScaleY4;
          float4 tmpvar_26;
          float2 P_27;
          P_27 = ((tmpvar_25 * in_f.xlv_TEXCOORD0) + tmpvar_12);
          tmpvar_26 = tex2D(_DistortionMap4, P_27);
          float2 tmpvar_28;
          tmpvar_28 = ((tmpvar_26 * _DistortionPower4) - (_DistortionPower4 * 0.5)).xy;
          distortion4_5 = tmpvar_28;
          float2 tmpvar_29;
          tmpvar_29.x = _MainTexScaleX;
          tmpvar_29.y = _MainTexScaleY;
          float2 tmpvar_30;
          tmpvar_30.x = _MainTexScrollX;
          tmpvar_30.y = _MainTexScrollY;
          float2 P_31;
          P_31 = (((tmpvar_29 * in_f.xlv_TEXCOORD0) + (_Refraction * ((distortion_8 + distortion2_7) + (distortion3_6 + distortion4_5)))) + tmpvar_30);
          float4 tmpvar_32;
          tmpvar_32 = ((tex2D(_MainTex, P_31) * tmpvar_4) * _Color);
          float4 tmpvar_33;
          tmpvar_33 = tex2D(_LightBuffer, in_f.xlv_TEXCOORD3);
          light_3 = tmpvar_33;
          light_3 = (-log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001))));
          light_3.xyz = (light_3.xyz + in_f.xlv_TEXCOORD5);
          float4 c_34;
          c_34.xyz = ((tmpvar_32.xyz * tmpvar_32.w) * light_3.xyz);
          c_34.w = tmpvar_32.w;
          c_2 = c_34;
          tmpvar_1 = c_2;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 5, name: DEFERRED
    {
      Name "DEFERRED"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "DEFERRED"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform sampler2D _DistortionMap3;
      uniform sampler2D _DistortionMap4;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _DispScrollSpeedX3;
      uniform float _DispScrollSpeedY3;
      uniform float _DistortionPower3;
      uniform float _DistortionScaleX3;
      uniform float _DistortionScaleY3;
      uniform float _DispScrollSpeedX4;
      uniform float _DispScrollSpeedY4;
      uniform float _DistortionPower4;
      uniform float _DistortionScaleX4;
      uniform float _DistortionScaleY4;
      uniform float _Refraction;
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
          tmpvar_4 = tmpvar_1;
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
          tmpvar_5.zw = float2(0, 0);
          tmpvar_5.xy = float2(0, 0);
          float3 normal_9;
          normal_9 = worldNormal_2;
          float4 tmpvar_10;
          tmpvar_10.w = 1;
          tmpvar_10.xyz = float3(normal_9);
          float3 res_11;
          float3 x_12;
          x_12.x = dot(unity_SHAr, tmpvar_10);
          x_12.y = dot(unity_SHAg, tmpvar_10);
          x_12.z = dot(unity_SHAb, tmpvar_10);
          float3 x1_13;
          float4 tmpvar_14;
          tmpvar_14 = (normal_9.xyzz * normal_9.yzzx);
          x1_13.x = dot(unity_SHBr, tmpvar_14);
          x1_13.y = dot(unity_SHBg, tmpvar_14);
          x1_13.z = dot(unity_SHBb, tmpvar_14);
          res_11 = (x_12 + (x1_13 + (unity_SHC.xyz * ((normal_9.x * normal_9.x) - (normal_9.y * normal_9.y)))));
          float3 tmpvar_15;
          float _tmp_dvx_174 = max(((1.055 * pow(max(res_11, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_15 = float3(_tmp_dvx_174, _tmp_dvx_174, _tmp_dvx_174);
          res_11 = tmpvar_15;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          out_v.xlv_TEXCOORD4 = tmpvar_5;
          out_v.xlv_TEXCOORD5 = max(float3(0, 0, 0), tmpvar_15);
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
          float2 distortion4_5;
          float2 distortion3_6;
          float2 distortion2_7;
          float2 distortion_8;
          float2 tmpvar_9;
          tmpvar_9.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_9.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_10;
          tmpvar_10.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_10.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_11;
          tmpvar_11.x = (_Time.y * _DispScrollSpeedX3);
          tmpvar_11.y = (_Time.y * _DispScrollSpeedY3);
          float2 tmpvar_12;
          tmpvar_12.x = (_Time.y * _DispScrollSpeedX4);
          tmpvar_12.y = (_Time.y * _DispScrollSpeedY4);
          float2 tmpvar_13;
          tmpvar_13.x = _DistortionScaleX;
          tmpvar_13.y = _DistortionScaleY;
          float4 tmpvar_14;
          float2 P_15;
          P_15 = ((tmpvar_13 * in_f.xlv_TEXCOORD0) + tmpvar_9);
          tmpvar_14 = tex2D(_DistortionMap, P_15);
          float2 tmpvar_16;
          tmpvar_16 = ((tmpvar_14 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_8 = tmpvar_16;
          float2 tmpvar_17;
          tmpvar_17.x = _DistortionScaleX2;
          tmpvar_17.y = _DistortionScaleY2;
          float4 tmpvar_18;
          float2 P_19;
          P_19 = ((tmpvar_17 * in_f.xlv_TEXCOORD0) + tmpvar_10);
          tmpvar_18 = tex2D(_DistortionMap2, P_19);
          float2 tmpvar_20;
          tmpvar_20 = ((tmpvar_18 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_7 = tmpvar_20;
          float2 tmpvar_21;
          tmpvar_21.x = _DistortionScaleX3;
          tmpvar_21.y = _DistortionScaleY3;
          float4 tmpvar_22;
          float2 P_23;
          P_23 = ((tmpvar_21 * in_f.xlv_TEXCOORD0) + tmpvar_11);
          tmpvar_22 = tex2D(_DistortionMap3, P_23);
          float2 tmpvar_24;
          tmpvar_24 = ((tmpvar_22 * _DistortionPower3) - (_DistortionPower3 * 0.5)).xy;
          distortion3_6 = tmpvar_24;
          float2 tmpvar_25;
          tmpvar_25.x = _DistortionScaleX4;
          tmpvar_25.y = _DistortionScaleY4;
          float4 tmpvar_26;
          float2 P_27;
          P_27 = ((tmpvar_25 * in_f.xlv_TEXCOORD0) + tmpvar_12);
          tmpvar_26 = tex2D(_DistortionMap4, P_27);
          float2 tmpvar_28;
          tmpvar_28 = ((tmpvar_26 * _DistortionPower4) - (_DistortionPower4 * 0.5)).xy;
          distortion4_5 = tmpvar_28;
          float2 tmpvar_29;
          tmpvar_29.x = _MainTexScaleX;
          tmpvar_29.y = _MainTexScaleY;
          float2 tmpvar_30;
          tmpvar_30.x = _MainTexScrollX;
          tmpvar_30.y = _MainTexScrollY;
          float2 P_31;
          P_31 = (((tmpvar_29 * in_f.xlv_TEXCOORD0) + (_Refraction * ((distortion_8 + distortion2_7) + (distortion3_6 + distortion4_5)))) + tmpvar_30);
          float4 tmpvar_32;
          tmpvar_32 = ((tex2D(_MainTex, P_31) * tmpvar_3) * _Color);
          tmpvar_4 = (tmpvar_32.xyz * tmpvar_32.w);
          float4 emission_33;
          float3 tmpvar_34;
          float3 tmpvar_35;
          tmpvar_34 = tmpvar_4;
          tmpvar_35 = tmpvar_2;
          float4 tmpvar_36;
          tmpvar_36.xyz = float3(tmpvar_34);
          tmpvar_36.w = 1;
          float4 tmpvar_37;
          tmpvar_37.xyz = float3(0, 0, 0);
          tmpvar_37.w = 0;
          float4 tmpvar_38;
          tmpvar_38.w = 1;
          tmpvar_38.xyz = float3(((tmpvar_35 * 0.5) + 0.5));
          float4 tmpvar_39;
          tmpvar_39.w = 1;
          tmpvar_39.xyz = float3(0, 0, 0);
          emission_33 = tmpvar_39;
          emission_33.xyz = (emission_33.xyz + (tmpvar_4 * in_f.xlv_TEXCOORD5));
          outEmission_1.w = emission_33.w;
          outEmission_1.xyz = exp2((-emission_33.xyz));
          out_f.color = tmpvar_36;
          out_f.color1 = tmpvar_37;
          out_f.color2 = tmpvar_38;
          out_f.color3 = outEmission_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 6, name: META
    {
      Name "META"
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "META"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform sampler2D _DistortionMap3;
      uniform sampler2D _DistortionMap4;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _DispScrollSpeedX3;
      uniform float _DispScrollSpeedY3;
      uniform float _DistortionPower3;
      uniform float _DistortionScaleX3;
      uniform float _DistortionScaleY3;
      uniform float _DispScrollSpeedX4;
      uniform float _DispScrollSpeedY4;
      uniform float _DistortionPower4;
      uniform float _DistortionScaleX4;
      uniform float _DistortionScaleY4;
      uniform float _Refraction;
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
          tmpvar_2 = tmpvar_1;
          float4 vertex_3;
          vertex_3 = in_v.vertex;
          if(unity_MetaVertexControl.x)
          {
              vertex_3.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
              float tmpvar_4;
              if((in_v.vertex.z>0))
              {
                  tmpvar_4 = 0.0001;
              }
              else
              {
                  tmpvar_4 = 0;
              }
              vertex_3.z = tmpvar_4;
          }
          if(unity_MetaVertexControl.y)
          {
              vertex_3.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
              float tmpvar_5;
              if((vertex_3.z>0))
              {
                  tmpvar_5 = 0.0001;
              }
              else
              {
                  tmpvar_5 = 0;
              }
              vertex_3.z = tmpvar_5;
          }
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = vertex_3.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
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
          float4 tmpvar_3;
          tmpvar_3 = in_f.xlv_TEXCOORD2;
          float3 tmpvar_4;
          float2 distortion4_5;
          float2 distortion3_6;
          float2 distortion2_7;
          float2 distortion_8;
          float2 tmpvar_9;
          tmpvar_9.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_9.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_10;
          tmpvar_10.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_10.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_11;
          tmpvar_11.x = (_Time.y * _DispScrollSpeedX3);
          tmpvar_11.y = (_Time.y * _DispScrollSpeedY3);
          float2 tmpvar_12;
          tmpvar_12.x = (_Time.y * _DispScrollSpeedX4);
          tmpvar_12.y = (_Time.y * _DispScrollSpeedY4);
          float2 tmpvar_13;
          tmpvar_13.x = _DistortionScaleX;
          tmpvar_13.y = _DistortionScaleY;
          float4 tmpvar_14;
          float2 P_15;
          P_15 = ((tmpvar_13 * in_f.xlv_TEXCOORD0) + tmpvar_9);
          tmpvar_14 = tex2D(_DistortionMap, P_15);
          float2 tmpvar_16;
          tmpvar_16 = ((tmpvar_14 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_8 = tmpvar_16;
          float2 tmpvar_17;
          tmpvar_17.x = _DistortionScaleX2;
          tmpvar_17.y = _DistortionScaleY2;
          float4 tmpvar_18;
          float2 P_19;
          P_19 = ((tmpvar_17 * in_f.xlv_TEXCOORD0) + tmpvar_10);
          tmpvar_18 = tex2D(_DistortionMap2, P_19);
          float2 tmpvar_20;
          tmpvar_20 = ((tmpvar_18 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_7 = tmpvar_20;
          float2 tmpvar_21;
          tmpvar_21.x = _DistortionScaleX3;
          tmpvar_21.y = _DistortionScaleY3;
          float4 tmpvar_22;
          float2 P_23;
          P_23 = ((tmpvar_21 * in_f.xlv_TEXCOORD0) + tmpvar_11);
          tmpvar_22 = tex2D(_DistortionMap3, P_23);
          float2 tmpvar_24;
          tmpvar_24 = ((tmpvar_22 * _DistortionPower3) - (_DistortionPower3 * 0.5)).xy;
          distortion3_6 = tmpvar_24;
          float2 tmpvar_25;
          tmpvar_25.x = _DistortionScaleX4;
          tmpvar_25.y = _DistortionScaleY4;
          float4 tmpvar_26;
          float2 P_27;
          P_27 = ((tmpvar_25 * in_f.xlv_TEXCOORD0) + tmpvar_12);
          tmpvar_26 = tex2D(_DistortionMap4, P_27);
          float2 tmpvar_28;
          tmpvar_28 = ((tmpvar_26 * _DistortionPower4) - (_DistortionPower4 * 0.5)).xy;
          distortion4_5 = tmpvar_28;
          float2 tmpvar_29;
          tmpvar_29.x = _MainTexScaleX;
          tmpvar_29.y = _MainTexScaleY;
          float2 tmpvar_30;
          tmpvar_30.x = _MainTexScrollX;
          tmpvar_30.y = _MainTexScrollY;
          float2 P_31;
          P_31 = (((tmpvar_29 * in_f.xlv_TEXCOORD0) + (_Refraction * ((distortion_8 + distortion2_7) + (distortion3_6 + distortion4_5)))) + tmpvar_30);
          float4 tmpvar_32;
          tmpvar_32 = ((tex2D(_MainTex, P_31) * tmpvar_3) * _Color);
          tmpvar_4 = (tmpvar_32.xyz * tmpvar_32.w);
          tmpvar_2 = tmpvar_4;
          float4 res_33;
          res_33 = float4(0, 0, 0, 0);
          if(unity_MetaFragmentControl.x)
          {
              float4 tmpvar_34;
              tmpvar_34.w = 1;
              tmpvar_34.xyz = float3(tmpvar_2);
              res_33.w = tmpvar_34.w;
              float3 tmpvar_35;
              float _tmp_dvx_175 = clamp(unity_OneOverOutputBoost, 0, 1);
              tmpvar_35 = clamp(pow(tmpvar_2, float3(_tmp_dvx_175, _tmp_dvx_175, _tmp_dvx_175)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
              res_33.xyz = float3(tmpvar_35);
          }
          if(unity_MetaFragmentControl.y)
          {
              float3 emission_36;
              if(int(unity_UseLinearSpace))
              {
                  emission_36 = float3(0, 0, 0);
              }
              else
              {
                  emission_36 = float3(0, 0, 0);
              }
              float4 tmpvar_37;
              float alpha_38;
              float3 tmpvar_39;
              tmpvar_39 = (emission_36 * 0.01030928);
              alpha_38 = (ceil((max(max(tmpvar_39.x, tmpvar_39.y), max(tmpvar_39.z, 0.02)) * 255)) / 255);
              float tmpvar_40;
              tmpvar_40 = max(alpha_38, 0.02);
              alpha_38 = tmpvar_40;
              float4 tmpvar_41;
              tmpvar_41.xyz = float3((tmpvar_39 / tmpvar_40));
              tmpvar_41.w = tmpvar_40;
              tmpvar_37 = tmpvar_41;
              res_33 = tmpvar_37;
          }
          tmpvar_1 = res_33;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
