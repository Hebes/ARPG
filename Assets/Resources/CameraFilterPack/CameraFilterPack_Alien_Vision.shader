Shader "CameraFilterPack/Alien_Vision"
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
          float4 thermal_1;
          float3 col_2;
          float blur_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_5;
          float2 x_6;
          x_6 = (uv_4 - float2(0.5, 0.5));
          tmpvar_5 = sqrt(dot(x_6, x_6));
          blur_3 = ((1 + sin(((_TimeX * 6) * _Value2))) * 0.5);
          blur_3 = (blur_3 * (1 + (sin(((_TimeX * 16) * _Value2)) * 0.5)));
          blur_3 = (pow(blur_3, 3) * 0.05);
          blur_3 = (blur_3 * tmpvar_5);
          float2 tmpvar_7;
          tmpvar_7.x = (uv_4.x + blur_3);
          tmpvar_7.y = uv_4.y;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, tmpvar_7);
          col_2.x = tmpvar_8.x;
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, uv_4);
          col_2.y = tmpvar_9.y;
          float2 tmpvar_10;
          tmpvar_10.x = (uv_4.x - blur_3);
          tmpvar_10.y = uv_4.y;
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, tmpvar_10);
          col_2.z = tmpvar_11.z;
          col_2 = (col_2 - (sin((uv_4.y * 800)) * 0.04));
          col_2 = (col_2 * (1 - (tmpvar_5 * 0.5)));
          float4 tmpvar_12;
          tmpvar_12.xzw = float3(0, 0, 1);
          tmpvar_12.y = (0.5 + (sin(_TimeX) / 4.14));
          float4 tmpvar_13;
          tmpvar_13.xzw = float3(0.1, 0.1, 1);
          tmpvar_13.y = (1 + (cos((_TimeX * 2)) / 4.14));
          float4 tmpvar_14;
          tmpvar_14.xzw = float3(0.1, 0, 1);
          tmpvar_14.y = (0.5 + (sin((_TimeX * 4)) / 4.14));
          float tmpvar_15;
          tmpvar_15 = (((col_2.x + col_2.y) + col_2.z) / 3);
          if((tmpvar_15<_Value))
          {
              float _tmp_dvx_125 = (tmpvar_15 / _Value);
              thermal_1 = lerp(tmpvar_12, tmpvar_14, float4(_tmp_dvx_125, _tmp_dvx_125, _tmp_dvx_125, _tmp_dvx_125));
          }
          if((tmpvar_15>=_Value))
          {
              float _tmp_dvx_126 = ((tmpvar_15 - _Value) / _Value);
              thermal_1 = lerp(tmpvar_13, tmpvar_14, float4(_tmp_dvx_126, _tmp_dvx_126, _tmp_dvx_126, _tmp_dvx_126));
          }
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = thermal_1.xyz;
          out_f.color = tmpvar_16;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
