Shader "PS4/airDistort/screen/displacement"
{
  Properties
  {
    _Color ("乘法叠加颜色", Color) = (1,1,1,1)
    _MainTex ("扭曲图", 2D) = "white" {}
    _DispScrollSpeedX ("X方向移动速度(图/s)", float) = 0
    _DispScrollSpeedY ("Y方向移动速度(图/s)", float) = 0
    _StrengthX ("X方向扭曲强度", float) = 0
    _StrengthY ("Y方向扭曲强度", float) = 1
    _Strength ("总方向扭曲强度", float) = 1
    [Toggle(BLEND_ON)] _BLEND ("使用叠加色的Alpha颜色?", float) = 1
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent+99"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      // m_ProgramMask = 0
      
    } // end phase
    Pass // ind: 2, name: BASE
    {
      Name "BASE"
      Tags
      { 
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Transparent+99"
        "RenderType" = "Transparent"
      }
      ZClip Off
      ZTest Always
      ZWrite Off
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
      uniform float4 _MainTex_ST;
      uniform float4 _Color;
      //uniform float4 _Time;
      uniform float _StrengthX;
      uniform float _StrengthY;
      uniform float _Strength;
      uniform sampler2D _MainTex;
      uniform float _DispScrollSpeedY;
      uniform float _DispScrollSpeedX;
      uniform sampler2D _GrabTexture;
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
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 xlv_COLOR :COLOR;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
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
          float4 tmpvar_2;
          float4 tmpvar_3;
          float4 tmpvar_4;
          tmpvar_4.w = 1;
          tmpvar_4.xyz = in_v.vertex.xyz;
          tmpvar_3 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_4));
          tmpvar_2.xy = ((tmpvar_3.xy + tmpvar_3.w) * 0.5);
          tmpvar_2.zw = tmpvar_3.zw;
          tmpvar_1.w = in_v.color.w;
          tmpvar_1.xyz = ((_Color.xyz * _Color.w) + (in_v.color.xyz * (1 - _Color.w)));
          out_v.vertex = tmpvar_3;
          out_v.xlv_COLOR = tmpvar_1;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = in_v.texcoord1.xy;
          out_v.xlv_TEXCOORD2 = tmpvar_2;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          tmpvar_1.zw = in_f.xlv_TEXCOORD2.zw;
          float4 col_2;
          float oftY_3;
          float oftX_4;
          float4 offsetColor_5;
          float2 mapoft_6;
          float2 tmpvar_7;
          tmpvar_7.x = (_Time.y * _DispScrollSpeedX);
          tmpvar_7.y = (_Time.y * _DispScrollSpeedY);
          mapoft_6 = tmpvar_7;
          float4 tmpvar_8;
          float2 P_9;
          P_9 = (in_f.xlv_TEXCOORD0 + mapoft_6);
          tmpvar_8 = tex2D(_MainTex, P_9);
          offsetColor_5 = tmpvar_8;
          offsetColor_5.xyz = (offsetColor_5.xyz * offsetColor_5.w);
          float tmpvar_10;
          float tmpvar_11;
          tmpvar_11 = (in_f.xlv_TEXCOORD1.x * _Strength);
          tmpvar_10 = ((offsetColor_5.x * _StrengthX) * tmpvar_11);
          oftX_4 = tmpvar_10;
          float tmpvar_12;
          tmpvar_12 = ((offsetColor_5.y * _StrengthY) * tmpvar_11);
          oftY_3 = tmpvar_12;
          tmpvar_1.x = (in_f.xlv_TEXCOORD2.x + oftX_4);
          tmpvar_1.y = (in_f.xlv_TEXCOORD2.y + oftY_3);
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_GrabTexture, tmpvar_1);
          col_2 = tmpvar_13;
          col_2.xyz = (col_2.xyz + (in_f.xlv_COLOR.xyz * in_f.xlv_COLOR.w));
          col_2.w = offsetColor_5.w;
          col_2.xyz = (col_2.xyz * offsetColor_5.w);
          out_f.color = col_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
