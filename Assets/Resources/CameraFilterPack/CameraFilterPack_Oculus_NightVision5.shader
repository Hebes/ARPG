Shader "CameraFilterPack/Oculus_NightVision5"
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
      uniform float _Red_R;
      uniform float _Red_G;
      uniform float _Red_B;
      uniform float _Green_R;
      uniform float _Green_G;
      uniform float _Green_B;
      uniform float _Blue_R;
      uniform float _Blue_G;
      uniform float _Blue_B;
      uniform float _Red_C;
      uniform float _Green_C;
      uniform float _Blue_C;
      uniform float _FadeFX;
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
          float3 rgb_1;
          float4 col_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_4;
          tmpvar_4 = tex2D(_MainTex, uv_3);
          col_2 = tmpvar_4;
          float3 tmpvar_5;
          tmpvar_5.x = _Red_R;
          tmpvar_5.y = (_Red_G - _Smooth);
          tmpvar_5.z = _Red_B;
          float3 tmpvar_6;
          tmpvar_6.x = _Green_R;
          tmpvar_6.y = (_Green_G - _Smooth);
          tmpvar_6.z = _Green_B;
          float3 tmpvar_7;
          tmpvar_7.x = _Blue_R;
          tmpvar_7.y = (_Blue_G - _Smooth);
          tmpvar_7.z = _Blue_B;
          float3 tmpvar_8;
          tmpvar_8.x = (dot(col_2.xyz, tmpvar_5) + _Red_C);
          tmpvar_8.y = (dot(col_2.xyz, tmpvar_6) + _Green_C);
          tmpvar_8.z = (dot(col_2.xyz, tmpvar_7) + _Blue_C);
          float xlat_varsample_9;
          float2 tmpvar_10;
          tmpvar_10.x = 1;
          tmpvar_10.y = (2 * cos(_TimeX));
          float2 P_11;
          P_11 = (((tmpvar_10 * _TimeX) * 8) + ((uv_3 * float2(0.5, 1)) + float2(1, 3)));
          float tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, P_11).x;
          xlat_varsample_9 = tmpvar_12;
          xlat_varsample_9 = (xlat_varsample_9 * xlat_varsample_9);
          float tmpvar_13;
          float x_14;
          x_14 = (((uv_3.y * 2) + (_TimeX / 4)) + sin((_TimeX + sin((_TimeX * 0.23)))));
          tmpvar_13 = (x_14 - floor(x_14));
          float tmpvar_15;
          tmpvar_15 = (float((tmpvar_13>=0.4)) - float((tmpvar_13>=0.6)));
          rgb_1 = (tmpvar_8 + ((((1 - (((tmpvar_13 - 0.4) / 0.2) * tmpvar_15)) * tmpvar_15) * xlat_varsample_9) / 8));
          float x_16;
          x_16 = ((uv_3.y * 30) + _TimeX);
          rgb_1 = (rgb_1 * ((12 + (x_16 - floor(x_16))) / 13));
          rgb_1 = (rgb_1 * (0.5 + ((((6 * uv_3.x) * uv_3.y) * (1.5 - uv_3.x)) * (1.5 - uv_3.y))));
          rgb_1 = (rgb_1 * (0.9 + (0.1 * sin(((10 * _TimeX) + (uv_3.y * 250))))));
          rgb_1 = (rgb_1 * (0.79 + (0.01 * sin((110 * _TimeX)))));
          uv_3.x = (uv_3.x / 0.72);
          float tmpvar_17;
          tmpvar_17 = (_Size - _Smooth);
          float2 tmpvar_18;
          tmpvar_18.y = 0.5;
          tmpvar_18.x = (0.694 + _Dist);
          float2 x_19;
          x_19 = (tmpvar_18 - uv_3);
          float tmpvar_20;
          tmpvar_20 = clamp(((sqrt(dot(x_19, x_19)) - _Size) / (tmpvar_17 - _Size)), 0, 1);
          float2 tmpvar_21;
          tmpvar_21.y = 0.5;
          tmpvar_21.x = (0.694 - _Dist);
          float2 x_22;
          x_22 = (tmpvar_21 - uv_3);
          float tmpvar_23;
          tmpvar_23 = clamp(((sqrt(dot(x_22, x_22)) - _Size) / (tmpvar_17 - _Size)), 0, 1);
          float4 tmpvar_24;
          tmpvar_24.w = 0;
          float _tmp_dvx_103 = ((1 - (tmpvar_20 * (tmpvar_20 * (3 - (2 * tmpvar_20))))) * (1 - (tmpvar_23 * (tmpvar_23 * (3 - (2 * tmpvar_23))))));
          tmpvar_24.xyz = float3((rgb_1 * (float3(1, 1, 1) - float3(_tmp_dvx_103, _tmp_dvx_103, _tmp_dvx_103))));
          float3 tmpvar_25;
          tmpvar_25 = lerp(col_2, tmpvar_24, float4(_FadeFX, _FadeFX, _FadeFX, _FadeFX)).xyz;
          rgb_1 = tmpvar_25;
          float4 tmpvar_26;
          tmpvar_26.w = 1;
          tmpvar_26.xyz = float3(tmpvar_25);
          out_f.color = tmpvar_26;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
