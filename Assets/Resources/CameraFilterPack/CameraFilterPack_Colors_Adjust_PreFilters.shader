Shader "CameraFilterPack/Colors_Adjust_PreFilters"
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
          float4 col_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, uv_2);
          col_1 = tmpvar_3;
          float3 tmpvar_4;
          tmpvar_4.x = _Red_R;
          tmpvar_4.y = _Red_G;
          tmpvar_4.z = _Red_B;
          float3 tmpvar_5;
          tmpvar_5.x = _Green_R;
          tmpvar_5.y = _Green_G;
          tmpvar_5.z = _Green_B;
          float3 tmpvar_6;
          tmpvar_6.x = _Blue_R;
          tmpvar_6.y = _Blue_G;
          tmpvar_6.z = _Blue_B;
          float3 tmpvar_7;
          tmpvar_7.x = (dot(col_1.xyz, tmpvar_4) + _Red_C);
          tmpvar_7.y = (dot(col_1.xyz, tmpvar_5) + _Green_C);
          tmpvar_7.z = (dot(col_1.xyz, tmpvar_6) + _Blue_C);
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          tmpvar_8.xyz = lerp(col_1.xyz, tmpvar_7, float3(_FadeFX, _FadeFX, _FadeFX));
          out_f.color = tmpvar_8;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
