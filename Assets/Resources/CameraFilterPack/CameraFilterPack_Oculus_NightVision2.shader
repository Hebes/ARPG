Shader "CameraFilterPack/Oculus_NightVision2"
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
          tmpvar_5.y = _Red_G;
          tmpvar_5.z = _Red_B;
          float3 tmpvar_6;
          tmpvar_6.x = _Green_R;
          tmpvar_6.y = _Green_G;
          tmpvar_6.z = _Green_B;
          float3 tmpvar_7;
          tmpvar_7.x = _Blue_R;
          tmpvar_7.y = _Blue_G;
          tmpvar_7.z = _Blue_B;
          float3 tmpvar_8;
          tmpvar_8.x = (dot(col_2.xyz, tmpvar_5) + _Red_C);
          tmpvar_8.y = (dot(col_2.xyz, tmpvar_6) + _Green_C);
          tmpvar_8.z = (dot(col_2.xyz, tmpvar_7) + _Blue_C);
          float4 tmpvar_9;
          tmpvar_9.w = 0;
          float _tmp_dvx_79 = (1 - ((0.5 + (0.5 * frac(((sin(dot((uv_3 * float2(0.1, 1)), float2(12.9898, 78.233))) * 43758.55) + _TimeX)))) * 0.5));
          tmpvar_9.xyz = float3((tmpvar_8 * float3(_tmp_dvx_79, _tmp_dvx_79, _tmp_dvx_79)));
          float xlat_varsample_10;
          float2 tmpvar_11;
          tmpvar_11.x = 1;
          tmpvar_11.y = (2 * cos(_TimeX));
          float2 P_12;
          P_12 = (((tmpvar_11 * _TimeX) * 8) + ((uv_3 * float2(0.5, 1)) + float2(1, 3)));
          float tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, P_12).x;
          xlat_varsample_10 = tmpvar_13;
          xlat_varsample_10 = (xlat_varsample_10 * xlat_varsample_10);
          float tmpvar_14;
          float x_15;
          x_15 = (((uv_3.y * 2) + (_TimeX / 4)) + sin((_TimeX + sin((_TimeX * 0.23)))));
          tmpvar_14 = (x_15 - floor(x_15));
          float tmpvar_16;
          tmpvar_16 = (float((tmpvar_14>=0.4)) - float((tmpvar_14>=0.6)));
          rgb_1 = (lerp(col_2, tmpvar_9, float4(_FadeFX, _FadeFX, _FadeFX, _FadeFX)).xyz + (((1 - (((tmpvar_14 - 0.4) / 0.2) * tmpvar_16)) * tmpvar_16) * xlat_varsample_10));
          float x_17;
          x_17 = ((uv_3.y * 30) + _TimeX);
          rgb_1 = (rgb_1 * ((12 + (x_17 - floor(x_17))) / 13));
          rgb_1 = (rgb_1 * (0.5 + ((((12 * uv_3.y) * (1.5 - uv_3.x)) * (1.5 - uv_3.y)) * uv_3.x)));
          rgb_1 = (rgb_1 * (0.9 + (0.1 * sin(((10 * _TimeX) + (uv_3.y * 300))))));
          rgb_1 = (rgb_1 * (0.79 + (0.01 * sin((110 * _TimeX)))));
          float4 tmpvar_18;
          tmpvar_18.w = 1;
          tmpvar_18.xyz = float3(rgb_1);
          out_f.color = tmpvar_18;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
