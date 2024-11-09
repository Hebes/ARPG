Shader "CameraFilterPack/FX_ZebraColor"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Value ("_Value", Range(1, 10)) = 10
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
      uniform float4 _ScreenResolution;
      uniform float _Value;
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
          float a_1;
          float3 col_2;
          float3 txt_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, uv_4).xyz;
          txt_3 = tmpvar_5;
          col_2 = (txt_3 / sqrt(dot(txt_3, txt_3)));
          float tmpvar_6;
          tmpvar_6 = (((txt_3.x + txt_3.y) + txt_3.z) / 3);
          float tmpvar_7;
          float tmpvar_8;
          tmpvar_8 = (min(abs((txt_3.x / txt_3.y)), 1) / max(abs((txt_3.x / txt_3.y)), 1));
          float tmpvar_9;
          tmpvar_9 = (tmpvar_8 * tmpvar_8);
          tmpvar_9 = (((((((((((-0.01213232 * tmpvar_9) + 0.05368138) * tmpvar_9) - 0.1173503) * tmpvar_9) + 0.1938925) * tmpvar_9) - 0.3326756) * tmpvar_9) + 0.9999793) * tmpvar_8);
          tmpvar_9 = (tmpvar_9 + (float((abs((txt_3.x / txt_3.y))>1)) * ((tmpvar_9 * (-2)) + 1.570796)));
          tmpvar_7 = (tmpvar_9 * sign((txt_3.x / txt_3.y)));
          if((abs(txt_3.y)>(1E-08 * abs(txt_3.x))))
          {
              if((txt_3.y<0))
              {
                  if((txt_3.x>=0))
                  {
                      tmpvar_7 = (tmpvar_7 + 3.141593);
                  }
                  else
                  {
                      tmpvar_7 = (tmpvar_7 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_7 = (sign(txt_3.x) * 1.570796);
          }
          a_1 = (2 * tmpvar_7);
          a_1 = (((floor(((_Value * a_1) / 3.141593)) * 3.141593) / _Value) + 1.570796);
          float2 tmpvar_10;
          tmpvar_10.x = cos(a_1);
          tmpvar_10.y = sin(a_1);
          a_1 = (6.283185 * dot(tmpvar_10, (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy)));
          float _tmp_dvx_16 = ((0.5 - (0.5 * cos(((0.5 * (floor((_Value * tmpvar_6)) / _Value)) * a_1)))) * (txt_3 / sqrt(dot(txt_3, txt_3))));
          col_2 = float3(_tmp_dvx_16, _tmp_dvx_16, _tmp_dvx_16);
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(col_2);
          out_f.color = tmpvar_11;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
