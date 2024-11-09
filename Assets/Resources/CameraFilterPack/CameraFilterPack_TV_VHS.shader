Shader "CameraFilterPack/TV_VHS"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
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
          float2 uv_1;
          float4 text_2;
          uv_1 = in_f.xlv_TEXCOORD0;
          uv_1.y = (1 - uv_1.y);
          float x_3;
          x_3 = (uv_1.y + (_TimeX * _Value3));
          uv_1.y = (x_3 - floor(x_3));
          float2 tmpvar_4;
          tmpvar_4.x = _TimeX;
          tmpvar_4.y = in_f.xlv_TEXCOORD0.y;
          uv_1.x = (uv_1.x + ((frac((sin(dot(tmpvar_4, float2(11.9898, 75.133))) * 43528.15)) - 0.5) / _Value));
          uv_1.y = (uv_1.y + ((frac((sin(dot(float2(_TimeX, _TimeX), float2(11.9898, 75.133))) * 43528.15)) - 0.5) / _Value2));
          float2 tmpvar_5;
          tmpvar_5.x = in_f.xlv_TEXCOORD0.y;
          tmpvar_5.y = _TimeX;
          float2 tmpvar_6;
          tmpvar_6.x = in_f.xlv_TEXCOORD0.y;
          tmpvar_6.y = (_TimeX + 1);
          float2 tmpvar_7;
          tmpvar_7.x = in_f.xlv_TEXCOORD0.y;
          tmpvar_7.y = (_TimeX + 2);
          float4 tmpvar_8;
          tmpvar_8.w = 0;
          tmpvar_8.x = frac((sin(dot(tmpvar_5, float2(11.9898, 75.133))) * 43528.15));
          tmpvar_8.y = frac((sin(dot(tmpvar_6, float2(11.9898, 75.133))) * 43528.15));
          tmpvar_8.z = frac((sin(dot(tmpvar_7, float2(11.9898, 75.133))) * 43528.15));
          text_2 = ((float4(-0.5, (-0.5), (-0.5), (-0.5)) + tmpvar_8) * 0.1);
          float2 tmpvar_9;
          tmpvar_9.x = floor((uv_1.y * 80));
          tmpvar_9.y = floor((uv_1.x * 50));
          float2 tmpvar_10;
          tmpvar_10.y = 0;
          tmpvar_10.x = _TimeX;
          float tmpvar_11;
          tmpvar_11 = frac((sin(dot((tmpvar_9 + tmpvar_10), float2(11.9898, 75.133))) * 43528.15));
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, uv_1);
          text_2 = (text_2 + tmpvar_12);
          if(!((tmpvar_11>(11.5 - (30 * uv_1.y))) || (tmpvar_11<(1.5 - (5 * uv_1.y)))))
          {
              text_2 = (text_2 + float4(_Value4, _Value4, _Value4, _Value4));
          }
          out_f.color = text_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
