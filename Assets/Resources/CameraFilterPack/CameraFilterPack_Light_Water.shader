Shader "CameraFilterPack/Light_Water"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
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
      uniform float _Alpha;
      uniform float _Distance;
      uniform float _Size;
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
          float4 sum_1;
          float c_2;
          float2 w_3;
          float2 uv_4;
          uv_4 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_5;
          tmpvar_5 = (((uv_4 - 1.2) * _Distance) - float2(10, 10));
          float tmpvar_6;
          tmpvar_6 = (_TimeX * (-2));
          float2 tmpvar_7;
          tmpvar_7.x = (cos((tmpvar_6 - tmpvar_5.x)) + sin((tmpvar_6 + tmpvar_5.y)));
          tmpvar_7.y = (sin((tmpvar_6 - tmpvar_5.y)) + cos((tmpvar_6 + tmpvar_5.x)));
          w_3 = (tmpvar_5 + tmpvar_7);
          float2 tmpvar_8;
          tmpvar_8.x = (tmpvar_5.x / (sin((w_3.x + tmpvar_6)) / 0.01));
          tmpvar_8.y = (tmpvar_5.y / (cos((w_3.y + tmpvar_6)) / 0.01));
          c_2 = (0.2 + (1.2 / sqrt(dot(tmpvar_8, tmpvar_8))));
          c_2 = (c_2 / 1.5);
          c_2 = (1.5 - sqrt((c_2 * _Size)));
          float tmpvar_9;
          tmpvar_9 = (c_2 * c_2);
          float4 tmpvar_10;
          tmpvar_10.w = 999;
          tmpvar_10.x = tmpvar_9;
          tmpvar_10.y = tmpvar_9;
          tmpvar_10.z = tmpvar_9;
          float3 tmpvar_11;
          tmpvar_11 = ((tmpvar_10 + float4(0, 0.3, 0.5, 1)).xyz * _Alpha);
          float2 tmpvar_12;
          tmpvar_12.y = 0;
          tmpvar_12.x = (tmpvar_11.x / 3.5);
          float4 tmpvar_13;
          float2 P_14;
          P_14 = (uv_4 + tmpvar_12);
          tmpvar_13 = tex2D(_MainTex, P_14);
          sum_1 = tmpvar_13;
          sum_1.xyz = (sum_1.xyz + tmpvar_11);
          out_f.color = sum_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
