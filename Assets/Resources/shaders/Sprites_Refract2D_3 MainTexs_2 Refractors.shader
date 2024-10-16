Shader "Sprites/Refract2D/3 MainTexs/2 Refractors"
{
  Properties
  {
    [PerRendererData] _MainTex ("MainTex", 2D) = "" {}
    _Color ("Tint", Color) = (1,1,1,1)
    _MainTexScrollX ("X Offset", float) = 0
    _MainTexScrollY ("Y Offset", float) = 0
    _MainTexScaleX ("X Scale", float) = 1
    _MainTexScaleY ("Y Scale", float) = 1
    _Refraction ("Refraction", float) = 1
    _DistortionMap ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX1 ("Disp Scroll Speed X(pic/s)", float) = 0
    _DispScrollSpeedY1 ("Disp Scroll Speed Y(pic/s)", float) = -0.25
    _DistortionScrollX ("X Offset", float) = 0
    _DistortionScrollY ("Y Offset", float) = 0
    _DistortionScaleX ("X Scale", float) = 1
    _DistortionScaleY ("Y Scale", float) = 1
    _DistortionPower ("Refraction", float) = 0.08
    _DistortionMap2 ("Distortion Map", 2D) = "" {}
    _DispScrollSpeedX2 ("Disp Scroll Speed X(pic/s)", float) = 0
    _DispScrollSpeedY2 ("Disp Scroll Speed Y(pic/s)", float) = -0.25
    _DistortionScrollX2 ("X Offset", float) = 0
    _DistortionScrollY2 ("Y Offset", float) = 0
    _DistortionScaleX2 ("X Scale", float) = 1
    _DistortionScaleY2 ("Y Scale", float) = 1
    _DistortionPower2 ("Distortion Power", float) = 0.08
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
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      Cull Off
      Blend One OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Color;
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform sampler2D _DistortionMap;
      uniform sampler2D _DistortionMap2;
      uniform float _MainTexScrollX;
      uniform float _MainTexScrollY;
      uniform float _MainTexScaleX;
      uniform float _MainTexScaleY;
      uniform float _DispScrollSpeedX1;
      uniform float _DispScrollSpeedY1;
      uniform float _DistortionPower;
      uniform float _DistortionScaleX;
      uniform float _DistortionScaleY;
      uniform float _DispScrollSpeedX2;
      uniform float _DispScrollSpeedY2;
      uniform float _DistortionPower2;
      uniform float _DistortionScaleX2;
      uniform float _DistortionScaleY2;
      uniform float _Refraction;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 xlv_COLOR :COLOR;
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
          float4 tmpvar_1;
          tmpvar_1.w = 1;
          tmpvar_1.xyz = in_v.vertex.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_1));
          out_v.xlv_COLOR = (in_v.color * _Color);
          out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float2 distortion2_1;
          float2 distortion_2;
          float2 tmpvar_3;
          tmpvar_3.x = (_Time.y * _DispScrollSpeedX1);
          tmpvar_3.y = (_Time.y * _DispScrollSpeedY1);
          float2 tmpvar_4;
          tmpvar_4.x = (_Time.y * _DispScrollSpeedX2);
          tmpvar_4.y = (_Time.y * _DispScrollSpeedY2);
          float2 tmpvar_5;
          tmpvar_5.x = _DistortionScaleX;
          tmpvar_5.y = _DistortionScaleY;
          float4 tmpvar_6;
          float2 P_7;
          P_7 = ((tmpvar_5 * in_f.xlv_TEXCOORD0) + tmpvar_3);
          tmpvar_6 = tex2D(_DistortionMap, P_7);
          float2 tmpvar_8;
          tmpvar_8 = ((tmpvar_6 * _DistortionPower) - (_DistortionPower * 0.5)).xy;
          distortion_2 = tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9.x = _DistortionScaleX2;
          tmpvar_9.y = _DistortionScaleY2;
          float4 tmpvar_10;
          float2 P_11;
          P_11 = ((tmpvar_9 * in_f.xlv_TEXCOORD0) + tmpvar_4);
          tmpvar_10 = tex2D(_DistortionMap2, P_11);
          float2 tmpvar_12;
          tmpvar_12 = ((tmpvar_10 * _DistortionPower2) - (_DistortionPower2 * 0.5)).xy;
          distortion2_1 = tmpvar_12;
          float tmpvar_13;
          tmpvar_13 = (distortion_2 + distortion2_1).x;
          float2 tmpvar_14;
          tmpvar_14.x = _MainTexScaleX;
          tmpvar_14.y = _MainTexScaleY;
          float2 tmpvar_15;
          tmpvar_15.x = _MainTexScrollX;
          tmpvar_15.y = _MainTexScrollY;
          float4 tmpvar_16;
          float2 P_17;
          P_17 = (((tmpvar_14 * in_f.xlv_TEXCOORD0) + (_Refraction * tmpvar_13)) + tmpvar_15);
          tmpvar_16 = tex2D(_MainTex, P_17);
          float4 tmpvar_18;
          tmpvar_18.x = (tmpvar_16.x * tmpvar_16.w);
          tmpvar_18.y = (tmpvar_16.y * tmpvar_16.w);
          tmpvar_18.z = (tmpvar_16.z * tmpvar_16.w);
          tmpvar_18.w = tmpvar_16.w;
          out_f.color = tmpvar_18;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
