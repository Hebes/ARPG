Shader "CameraFilterPack/NightVisionFX"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
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
      uniform float _OnOff;
      uniform float _Vignette;
      uniform float _Vignette_Alpha;
      uniform float _Greenness;
      uniform float _Distortion;
      uniform float _Noise;
      uniform float _Intensity;
      uniform float _Light;
      uniform float _Light2;
      uniform float _Line;
      uniform float _Color_R;
      uniform float _Color_G;
      uniform float _Color_B;
      uniform float _Size;
      uniform float _Dist;
      uniform float _Smooth;
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
          float3 colm_1;
          float3 col_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float2 uv_4;
          uv_4 = (uv_3 - float2(0.5, 0.5));
          uv_4 = ((uv_4 * 1.2) * (0.8333333 + ((2 * uv_4.x) * ((uv_4.x * uv_4.y) * uv_4.y))));
          uv_4 = (uv_4 + float2(0.5, 0.5));
          float2 tmpvar_5;
          tmpvar_5 = lerp(uv_3, uv_4, float2(_Distortion, _Distortion));
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex, tmpvar_5);
          col_2 = tmpvar_6.xyz;
          col_2 = (col_2 + ((frac((sin(dot((tmpvar_5 * floor((_TimeX * 8))), float2(12.9898, 78.233))) * 43758.55)) / 2) * _Noise));
          float tmpvar_7;
          float2 tmpvar_8;
          tmpvar_8 = (float2(0.5, 0.5) - in_f.xlv_TEXCOORD0);
          tmpvar_7 = sqrt(dot(tmpvar_8, tmpvar_8));
          float edge0_9;
          edge0_9 = (0.15 + _Vignette);
          float tmpvar_10;
          tmpvar_10 = clamp(((tmpvar_7 - edge0_9) / ((0.5 + _Vignette) - edge0_9)), 0, 1);
          col_2 = (col_2 - ((tmpvar_10 * (tmpvar_10 * (3 - (2 * tmpvar_10)))) * _Vignette_Alpha));
          float tmpvar_11;
          tmpvar_11 = sqrt(dot(tmpvar_8, tmpvar_8));
          float edge0_12;
          edge0_12 = (0.25 + _Vignette);
          float tmpvar_13;
          tmpvar_13 = clamp(((tmpvar_11 - edge0_12) / (_Vignette - edge0_12)), 0, 1);
          col_2 = (col_2 + ((tmpvar_13 * (tmpvar_13 * (3 - (2 * tmpvar_13)))) * _Light2));
          col_2 = (col_2 + _Intensity);
          colm_1 = col_2;
          float3 tmpvar_14;
          tmpvar_14.xz = float2(0.55, 0.55);
          tmpvar_14.y = (1.55 + (_Greenness / 4));
          col_2 = (col_2 * tmpvar_14);
          col_2 = (col_2 * lerp(1, (0.8 + (0.1 * sin(((10 * _TimeX) + ((tmpvar_5.y * 300) * _Light))))), _Line));
          col_2.x = (col_2.x + _Color_R);
          col_2.y = (col_2.y + _Color_G);
          col_2.z = (col_2.z + _Color_B);
          uv_3 = in_f.xlv_TEXCOORD0;
          uv_3.x = (uv_3.x / 0.72);
          float tmpvar_15;
          tmpvar_15 = (_Size - _Smooth);
          float2 tmpvar_16;
          tmpvar_16.y = 0.5;
          tmpvar_16.x = (0.694 + _Dist);
          float2 x_17;
          x_17 = (tmpvar_16 - uv_3);
          float tmpvar_18;
          tmpvar_18 = clamp(((sqrt(dot(x_17, x_17)) - _Size) / (tmpvar_15 - _Size)), 0, 1);
          float2 tmpvar_19;
          tmpvar_19.y = 0.5;
          tmpvar_19.x = (0.694 - _Dist);
          float2 x_20;
          x_20 = (tmpvar_19 - uv_3);
          float tmpvar_21;
          tmpvar_21 = clamp(((sqrt(dot(x_20, x_20)) - _Size) / (tmpvar_15 - _Size)), 0, 1);
          float3 tmpvar_22;
          float _tmp_dvx_92 = ((1 - (tmpvar_18 * (tmpvar_18 * (3 - (2 * tmpvar_18))))) * (1 - (tmpvar_21 * (tmpvar_21 * (3 - (2 * tmpvar_21))))));
          tmpvar_22 = (lerp(col_2, colm_1, float3(_OnOff, _OnOff, _OnOff)) * (float3(1, 1, 1) - float3(_tmp_dvx_92, _tmp_dvx_92, _tmp_dvx_92)));
          col_2 = tmpvar_22;
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3(tmpvar_22);
          out_f.color = tmpvar_23;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
