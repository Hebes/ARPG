Shader "PS4/displacement/additive"
{
  Properties
  {
    _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex ("Main Texture", 2D) = "white" {}
    _DispMap ("Displacement Map (RG)", 2D) = "white" {}
    _DissolveMap ("Dissolve Mask (R)", 2D) = "white" {}
    _DispScrollSpeedX ("Displacement Map Scroll Speed X", float) = 0
    _DispScrollSpeedY ("Displacement Map Scroll Speed Y", float) = 0
    _DispX ("Displacement Strength X", float) = 0
    _DispY ("Displacement Strength Y", float) = 0.2
    _DissolveGamma ("Dissolve Gamma", float) = 1
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
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
      ZWrite Off
      Cull Off
      Blend SrcAlpha One
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
      uniform sampler2D _DissolveMap;
      uniform sampler2D _DispMap;
      uniform float _DispScrollSpeedX;
      uniform float _DispScrollSpeedY;
      uniform float _DispX;
      uniform float _DispY;
      uniform float4 _TintColor;
      uniform float _DissolveGamma;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float4 xlv_COLOR :COLOR;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 xlv_COLOR :COLOR;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
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
          out_v.xlv_COLOR = in_v.color;
          out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
          out_v.xlv_TEXCOORD1 = in_v.texcoord1.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float phase_1;
          float grayscale_2;
          float2 uvoft_3;
          float4 dispColor_4;
          float2 mapoft_5;
          float2 tmpvar_6;
          tmpvar_6.x = (_Time.y * _DispScrollSpeedX);
          tmpvar_6.y = (_Time.y * _DispScrollSpeedY);
          mapoft_5 = tmpvar_6;
          float4 tmpvar_7;
          float2 P_8;
          P_8 = (in_f.xlv_TEXCOORD0 + mapoft_5);
          tmpvar_7 = tex2D(_DispMap, P_8);
          dispColor_4 = tmpvar_7;
          uvoft_3 = in_f.xlv_TEXCOORD0;
          uvoft_3.x = (uvoft_3.x + ((dispColor_4.x * _DispX) * in_f.xlv_TEXCOORD1.x));
          uvoft_3.y = (uvoft_3.y + ((dispColor_4.y * _DispY) * in_f.xlv_TEXCOORD1.x));
          float tmpvar_9;
          tmpvar_9 = in_f.xlv_TEXCOORD1.y;
          phase_1 = tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = min((max((tex2D(_DissolveMap, uvoft_3).x - phase_1), 0) / (1 - phase_1)), 1);
          float tmpvar_11;
          tmpvar_11 = pow(tmpvar_10, (1 / _DissolveGamma));
          grayscale_2 = tmpvar_11;
          float4 tmpvar_12;
          tmpvar_12 = ((2 * in_f.xlv_COLOR) * ((_TintColor * tex2D(_MainTex, in_f.xlv_TEXCOORD0)) * grayscale_2));
          out_f.color = tmpvar_12;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
