Shader "CameraFilterPack/Light_Rainbow2"
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
          float3 V_1;
          float2 uv_2;
          uv_2 = in_f.xlv_TEXCOORD0;
          uv_2 = (uv_2 - float2(0.5, 0.5));
          uv_2.y = (uv_2.y * _Value);
          uv_2 = (uv_2 + (sin((((uv_2.x * 10) * (uv_2.y * 1.11)) + _TimeX)) * 0.15));
          float3 tmpvar_3;
          tmpvar_3.yz = float2(1, 1);
          tmpvar_3.x = ((uv_2.x * 0.1) + (_TimeX * 0.25));
          float3 x_4;
          x_4 = ((tmpvar_3.x * 6) + float3(0, 4, 2));
          V_1 = (clamp((abs(((x_4 - (floor((x_4 * float3(0.1666667, 0.1666667, 0.1666667))) * float3(6, 6, 6))) - 3)) - 1), float3(0, 0, 0), float3(1, 1, 1)) * clamp(((0.7 - abs(uv_2.y)) * 3), 0, 1));
          V_1 = (V_1 * (1 - (sin(((uv_2.y * uv_2.y) * 30)) * 0.26)));
          float3 tmpvar_5;
          tmpvar_5 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) / 2).xyz;
          V_1 = (V_1 * tmpvar_5);
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = float3(V_1);
          out_f.color = tmpvar_6;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
